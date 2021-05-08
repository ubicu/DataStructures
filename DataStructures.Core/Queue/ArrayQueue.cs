using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DataStructures.Core.Queue
{
    public class ArrayQueue<T> : IQueue<T>, IEnumerable<T>
    {
        private T[] data;
        private int front;
        private int rear;

        #region Constructors
        public ArrayQueue(int capacity)
        {

        }
        #endregion

        #region Public methods
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool isEmpty()
        {
            throw new NotImplementedException();
        }

        public void offer(T elem)
        {
            throw new NotImplementedException();
        }

        public T peek()
        {
            throw new NotImplementedException();
        }

        public T poll()
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
    }
}
