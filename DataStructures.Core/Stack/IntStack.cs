using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Core
{
    public class IntStack : IStack<int>, IEnumerable
    {
        private int[] arr;
        private int pos = 0;

        #region Constructors
        public IntStack(int maxSize)
        {
            arr = new int[maxSize];
        }
        #endregion

        #region Public methods
        public int size()
        {
            return pos;
        }

        public bool isEmpty()
        {
            return (pos == 0);
        }

        public int peek()
        {
            if (isEmpty())
                throw new InvalidOperationException("Stack is empty");

            return arr[pos - 1];
        }

        public int pop()
        {
            if (isEmpty())
                throw new InvalidOperationException("Stack is empty");

            return arr[--pos];
        }

        public void push(int elem)
        {
            arr[pos++] = elem;
        }
        #endregion

        #region Private enumerator class
        private class IntStackEnumerator : IEnumerator
        {
            int[] arr;
            int position = -1;
            int pos;

            // Constructor
            public IntStackEnumerator(IntStack stack)
            {
                arr = stack.arr;
                pos = stack.pos;
            }

            public object Current
            {
                get
                {
                    if (position >= pos || position < 0) throw new InvalidOperationException("Index is out-of-range");
                    return arr[position];
                }
            }

            public bool MoveNext()
            {
                position++;
                return position < pos;
            }

            public void Reset()
            {
                position = -1;
            }
        }
        #endregion

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new IntStackEnumerator(this);
        }

    }
}
