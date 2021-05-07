using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Core
{
    public class IntArray : IDynamicArray<int>, IEnumerable
    {
        private static readonly int DEFAULT_CAP = 1 << 3;

        public int[] arr;
        public int len = 0;
        private int capacity = 0;

        #region Constructors
        public IntArray() : this(DEFAULT_CAP) { }

        public IntArray(int capacity)
        {
            if (capacity < 0) throw new ArgumentOutOfRangeException($"Illegal Capacity: {capacity}");
            this.capacity = capacity;
            arr = new int[capacity];
        }

        public IntArray(int[] array)
        {
            if (array == null) throw new ArgumentNullException("Array cannot be null");

            arr = new int[array.Length];
            Array.Copy(array, arr, array.Length);
            capacity = len = array.Length;
        }
        #endregion

        #region Public methods

        public int size()
        {
            return len;
        }

        public bool isEmpty()
        {
            return (len == 0);
        }

        public int get(int index)
        {
            return arr[index];
        }

        public void set(int index, int elem)
        {
           arr[index] = elem;
        }

        public void add(int elem)
        {
            // Resize
            if (len + 1 >= capacity)
            {
                capacity = (capacity == 0) ? 1 : 2 * capacity;
                var newarr = new int[capacity];
                Array.Copy(arr, newarr, arr.Length);
                arr = newarr;
            }

            arr[len++] = elem;
        }

        public void clear()
        {
            for (int i = 0; i < len; i++)
            {
                arr[i] = 0;
            }

            len = 0;
        }

        public int removeAt(int index)
        {
            if (index >= len || index < 0)
                throw new IndexOutOfRangeException($"Index is out-of-range: {index}");

            #region Original implementation - does not pass unit test
            /*int data = arr[index];
            Array.Copy(arr, index + 1, arr, index, len - index - 1);

            len--;
            capacity--; */
            #endregion

            var data = arr[index];
            var newarr = new int[len - 1];

            for (int i = 0, j = 0; i < len; i++, j++)
            {
                if (i == index) j--; // Skip over rm_index by fixing j temporarily
                else newarr[j] = arr[i];
            }

            arr = newarr;
            capacity = --len;

            return data;
        }

        public bool remove(int obj)
        {
            int index = indexOf(obj);
            if (index == -1)
                return false;

            removeAt(index);
            return true;
        }

        public int indexOf(int obj)
        {
            for (int i = 0; i < len; i++)
            {
                if (obj == arr[i]) 
                    return i;
            }
            return -1;
        }

        #endregion

        public override String ToString()
        {
            if (len == 0) return "[]";
            else
            {
                StringBuilder sb = new StringBuilder(len).Append("[");
                for (int i = 0; i < len - 1; i++) sb.Append(arr[i] + ", ");
                return sb.Append(arr[len - 1] + "]").ToString();
            }
        }

        #region Private enumerator class
        private class IntArrayEnumerator : IEnumerator
        {
            int[] arrlist;
            int position = -1;
            int length;

            // Constructor
            public IntArrayEnumerator(IntArray array)
            {
                arrlist = array.arr;
                length = array.len;
            }

            public object Current
            {
                get
                {
                    if (position >= length || position < 0) throw new InvalidOperationException("Index is out-of-range");
                    return arrlist[position];
                }
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                position++;
                return position < length;
            }

            public void Reset()
            {
                position = -1;
            }
        }
        #endregion

        public IEnumerator GetEnumerator()
        {
            return new IntArrayEnumerator(this);
        }

    }
}
