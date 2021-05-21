using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Text;

namespace DataStructures.Core
{
    public class BinaryHeap<T> : IComparable<T>
    {
        // A dynamic list to track the elements inside the heap
        private List<T> heap = null;

        #region Constructors

        // Construct and initially empty priority queue
        public BinaryHeap() : this(1) { }

        // Construct a priority queue with an initial capacity
        public BinaryHeap(int size)
        {
            heap = new List<T>(size);
        }

        // Construct a priority queue using heapify in O(n) time, a great explanation can be found at:
        // http://www.cs.umd.edu/~meesh/351/mount/lectures/lect14-heapsort-analysis-part.pdf
        public BinaryHeap(T[] elems)
        {
            throw new NotImplementedException();
        }

        // Priority queue construction, O(n)
        public BinaryHeap(ICollection<T> elems)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Public methods

        // Returns true/false depending on if the priority queue is empty
        public bool isEmpty()
        {
            return size() == 0;
        }

        // Clears everything inside the heap, O(n)
        public void clear()
        {
            heap.Clear();
        }

        // Return the size of the heap
        public int size()
        {
            return heap.Count;
        }

        // Returns the value of the element with the lowest
        // priority in this priority queue. If the priority
        // queue is empty, generic default is returned
        public T peek()
        {
            if (isEmpty())
                return default(T);

            return heap[0];
        }

        // Removes the root of the heap, O(log(n))
        public T poll()
        {
            if (isEmpty())
                return default(T);

            return removeAt(0);
        }

        #endregion

        #region Private methods
        private T removeAt(int index)
        {
            if (isEmpty())
                return default(T);

            int indexOfLastElem = size() - 1;
            T removed_data = heap[indexOfLastElem];
            swap(index, indexOfLastElem);

            // Obliterate the value
            heap.RemoveAt(indexOfLastElem);

            // Check if the last element was removed
            if (index == indexOfLastElem) 
                return removed_data;

            T elem = heap[index];

            // Try sinking element
            sink(index);

            // If sinking did not work try swimming
            if (heap[index].Equals(elem)) 
                swim(index);

            return removed_data;
        }

        private void swim(int index)
        {
            throw new NotImplementedException();
        }

        // Top down node sink, O(log(n))
        private void sink(object i)
        {
            throw new NotImplementedException();
        }

        // Swap two nodes. Assumes i & j are valid, O(1)
        private void swap(int index_i, int index_j)
        {
            T elem_i = heap[index_i];
            T elem_j = heap[index_j];

            heap[index_i] = elem_j;
            heap[index_j] = elem_i;
        }
        #endregion

        public int CompareTo([AllowNull] T other)
        {
            throw new NotImplementedException();
        }
    }
}
