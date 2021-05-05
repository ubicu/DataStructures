using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Core
{
    public class ArrayStack<T> : IStack<T>, IEnumerable<T>
    {


        #region Public methods
        public bool isEmpty()
        {
            throw new NotImplementedException();
        }

        public T peek()
        {
            throw new NotImplementedException();
        }

        public T pop()
        {
            throw new NotImplementedException();
        }

        public void push(T elem)
        {
            throw new NotImplementedException();
        }

        public int size()
        {
            throw new NotImplementedException();
        }
        #endregion

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
