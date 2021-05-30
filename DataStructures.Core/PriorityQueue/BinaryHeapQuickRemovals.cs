using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Core.PriorityQueue
{
    public class BinaryHeapQuickRemovals<T> : IBinaryHeap<T> where T : IComparable<T>
    {
        // A dynamic list to track the elements inside the heap
        private List<T> heap = null;

        // This map keeps track of the possible indices a particular
        // node value is found in the heap. Having this mapping lets
        // us have O(log(n)) removals and O(1) element containment check
        // at the cost of some additional space and minor overhead
        private Dictionary<T, HashSet<int>> map = new Dictionary<T, HashSet<int>>();

        #region Constructors

        // Construct and initially empty priority queue
        public BinaryHeapQuickRemovals() : this(1) { }

        // Construct a priority queue with an initial capacity
        public BinaryHeapQuickRemovals(int size)
        {
            heap = new List<T>(size);
        }

        #endregion

        #region Public methods

        public void add(T elem)
        {
            throw new NotImplementedException();
        }

        public void clear()
        {
            heap.Clear();
            map.Clear();
        }

        public bool contains(T elem)
        {
            throw new NotImplementedException();
        }

        public bool isEmpty()
        {
            return size() == 0;
        }

        public bool isMinHeap(int k)
        {
            throw new NotImplementedException();
        }

        public T peek()
        {
            if (isEmpty())
                return default(T);

            return heap[0];
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
            return heap.Count;
        }

        #endregion
    }
}
