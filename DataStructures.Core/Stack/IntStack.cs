using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Core.Stack
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


            public object Current => throw new NotImplementedException();

            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

    }
}
