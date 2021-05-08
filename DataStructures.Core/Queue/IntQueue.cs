using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Core.Queue
{
    public class IntQueue : IQueue<int>, IEnumerable
    {
        private int[] data;
        private int front, rear;
        private int _size;

        #region Constructors
        public IntQueue(int capacity)
        {
            data = new int[capacity];
            front = rear = -1;
            _size = 0;
        }
        #endregion

        #region Public methods
        public bool isEmpty()
        {
            return (_size == 0);
        }

        public void offer(int elem)
        {
            if (isFull())
                throw new InvalidOperationException("Queue is full");

            if (isEmpty())
            {
                front = rear = 0;
                data[front] = elem;
                _size++;
                return;
            }

            rear = (rear + 1) % data.Length;
            data[rear] = elem;
            _size++;
        }

        public int peek()
        {
            if (isEmpty())
                throw new InvalidOperationException("Queue is empty");

            return data[front];
        }

        public int poll()
        {
            if (isEmpty())
                throw new InvalidOperationException("Queue is empty");

            var elem = data[front];

            if (_size == 1)
            {
                // reset to initial state
                front = rear = -1;
                _size = 0;
            }
            else
            {
                front = (front + 1) % data.Length;
                --_size;
            }

            return elem;
        }

        public int size()
        {
            return _size;
        }

        public bool isFull()
        {
            return (_size == data.Length);
        }
        #endregion

        #region Private enumerator class
        private class IntQueueEnumerator : IEnumerator
        {
            private int[] data;
            private int front, rear;
            private int _size;
            private int position = -1;

            // Constructor
            public IntQueueEnumerator(IntQueue queue)
            {
                data = queue.data;
                front = queue.front;
                rear = queue.rear;
                _size = queue._size;
            }

            public int Current
            {
                get
                {
                    return data[(front + position) % data.Length];
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return data[(front + position) % data.Length];
                }
            }

            public bool MoveNext()
            {
                position++;
                return position < _size;
            }

            public void Reset()
            {
                position = -1;
            }
        }
        #endregion

        public IEnumerator GetEnumerator()
        {
            return new IntQueueEnumerator(this);
        }

    }
}
