using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DataStructures.Core
{
    public class ArrayQueue<T> : IQueue<T>, IEnumerable<T>
    {
        private T[] data;
        private int front, rear;
        private int _size;

        #region Constructors
        public ArrayQueue(int capacity)
        {
            data = new T[capacity];
            front = rear = -1;
            _size = 0;
        }
        #endregion

        #region Public methods
        
        public bool isEmpty()
        {
            return (_size == 0);
        }

        public void offer(T elem)
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

        public T peek()
        {
            if (isEmpty())
                throw new InvalidOperationException("Queue is empty");

            return data[front];
        }

        public T poll()
        {
            if (isEmpty())
                throw new InvalidOperationException("Queue is empty");

            T elem = data[front];

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
        private class ArrayQueueEnumerator : IEnumerator<T>
        {
            private T[] data;
            private int front, rear;
            private int _size;
            private int position = -1;

            // Constructor
            public ArrayQueueEnumerator(ArrayQueue<T> queue)
            {
                data  = queue.data;
                front = queue.front;
                rear  = queue.rear;
                _size = queue._size;
            }

            public T Current
            {
                get {
                    return data[(front + position) % data.Length]; 
                }
            }

            object IEnumerator.Current
            {
                get { 
                    return data[(front + position) % data.Length]; 
                }
            }

            public void Dispose() { }

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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ArrayQueueEnumerator(this);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ArrayQueueEnumerator(this);
        }

    }
}
