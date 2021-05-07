using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DataStructures.Core
{
    public class ArrayStack<T> : IStack<T>, IEnumerable<T>
    {
        private int _size;
        private int capacity;
        private T[] data;

        #region Constructors
        public ArrayStack()
        {
            capacity = 16;
            data = new T[capacity];
        }
        #endregion

        #region Public methods

        public int size()
        {
            return _size;
        }

        public bool isEmpty()
        {
            return (_size == 0);
        }

        public void push(T elem)
        {
            if (_size == capacity)
                increaseCapacity();

            data[_size++] = elem;
        }

        public T peek()
        {
            if (isEmpty())
                throw new InvalidOperationException("Stack is empty");

            return data[_size - 1];
        }

        public T pop()
        {
            if(isEmpty())
                throw new InvalidOperationException("Stack is empty");

            var elem = data[--_size];
            data[_size] = default(T);

            return elem;
        }

        #endregion

        private void increaseCapacity()
        {
            capacity *= 2;
            var newarr = new T[capacity];
            Array.Copy(data, newarr, data.Length);
            data = newarr;
        }

        #region Private enumerator class
        private class ArrayStackEnumerator : IEnumerator<T>
        {
            T[] data;
            int position = -1;
            int _size;

            // Constructor
            public ArrayStackEnumerator(ArrayStack<T> stack)
            {
                data = stack.data;
                _size = stack._size;
            }

            public T Current
            {
                get
                {
                    if (position >= _size || position < 0) throw new InvalidOperationException("Index is out-of-range");
                    return data[position];
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    if (position >= _size || position < 0) throw new InvalidOperationException("Index is out-of-range");
                    return data[position];
                }
            }

            public void Dispose(){}

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
            return new ArrayStackEnumerator(this);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ArrayStackEnumerator(this);
        }
    }
}
