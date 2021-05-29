using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Core.PriorityQueue
{
    public class BinaryHeapQuickRemovals<T> : IBinaryHeap<T> where T : IComparable<T>
    {
        // A dynamic list to track the elements inside the heap
        private List<T> heap = null;

        #region Constructors

        #endregion

        #region Public methods

        public void add(T elem)
        {
            throw new NotImplementedException();
        }

        public void clear()
        {
            throw new NotImplementedException();
        }

        public bool contains(T elem)
        {
            throw new NotImplementedException();
        }

        public bool isEmpty()
        {
            throw new NotImplementedException();
        }

        public bool isMinHeap(int k)
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

        public bool remove(T element)
        {
            throw new NotImplementedException();
        }

        public int size()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
