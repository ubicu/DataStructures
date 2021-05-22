using System;
using System.Collections.Generic;

namespace DataStructures.Core
{
    public class BinaryHeap<T> where T : IComparable<T>
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
            int heapSize = elems.Length;
            heap = new List<T>(heapSize);

            // Place all element in heap
            for (int i = 0; i < heapSize; i++) 
                heap.Add(elems[i]);

            // Heapify process, O(n)
            for (int i = Math.Max(0, (heapSize / 2) - 1); i >= 0; i--) 
                sink(i);
        }

        // Priority queue construction, O(n)
        public BinaryHeap(ICollection<T> elems)
        {
            int heapSize = elems.Count;

            // Add all elements of the given collection to the heap
            heap = new List<T>(elems);

            // Heapify process, O(n)
            for (int i = Math.Max(0, (heapSize / 2) - 1); i >= 0; i--)
                sink(i);

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

        public bool contains(T elem)
        {
            foreach(var element in heap)
            {
                if (element.Equals(elem))
                    return true;
            }

            return false;
        }

        // Adds an element to the priority queue, the
        // element must not be null, O(log(n))
        public void add(T elem)
        {
            if (elem == null) 
                throw new ArgumentNullException();

            heap.Add(elem);

            int indexOfLastElem = size() - 1;
            swim(indexOfLastElem);

        }

        // Recursively checks if this heap is a min heap
        // This method is just for testing purposes to make
        // sure the heap invariant is still being maintained
        // Called this method with k=0 to start at the root
        public bool isMinHeap(int k)
        {
            // If we are outside the bounds of the heap return true
            int heapSize = size();
            if (k >= heapSize) 
                return true;

            int left  = 2 * k + 1;
            int right = 2 * k + 2;

            // Make sure that the current node k is less than
            // both of its children left, and right if they exist
            // return false otherwise to indicate an invalid heap
            if (left < heapSize && !less(k, left)) return false;
            if (right < heapSize && !less(k, right)) return false;

            // Recurse on both children to make sure they're also valid heaps
            return isMinHeap(left) && isMinHeap(right);
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

        private void swim(int k)
        {
            // Grab the index of the next parent node WRT to k
            int parent = (k - 1) / 2;

            // Keep swimming while we have not reached the
            // root and while we're less than our parent.
            while (k > 0 && less(k, parent))
            {
                // Exchange k with the parent
                swap(parent, k);
                k = parent;

                // Grab the index of the next parent node WRT to k
                parent = (k - 1) / 2;
            }
        }

        // Top down node sink, O(log(n))
        private void sink(int k)
        {
            int heapSize = size();
            while (true)
            {
                int left = 2 * k + 1; // Left  node
                int right = 2 * k + 2; // Right node
                int smallest = left; // Assume left is the smallest node of the two children

                // Find which is smaller left or right
                // If right is smaller set smallest to be right
                if (right < heapSize && less(right, left)) smallest = right;

                // Stop if we're outside the bounds of the tree
                // or stop early if we cannot sink k anymore
                if (left >= heapSize || less(k, smallest)) 
                    break;

                // Move down the tree following the smallest node
                swap(smallest, k);
                k = smallest;
            }
        }

        // Tests if the value of node i <= node j
        // This method assumes i & j are valid indices, O(1)
        private bool less(int i, int j)
        {
            T node1 = heap[i];
            T node2 = heap[j];

            return node1.CompareTo(node2) <= 0;
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

            
        public override string ToString()
        {
            return heap.ToString();
        }

    }
}
