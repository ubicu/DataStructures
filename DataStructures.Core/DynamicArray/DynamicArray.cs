using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Core
{
    public class DynamicArray<T> : IDynamicArray<T>, IEnumerable<T>
    {
        private T[] arr;
        private int len = 0;        // Length user thinks array is
        private int capacity = 0;   // Actual array size

        #region Constructor
        public DynamicArray() : this(16)
        {
        }

        public DynamicArray(int capacity)
        {
            if (capacity < 0) throw new ArgumentOutOfRangeException($"Illegal capacity: {capacity}");
            this.capacity = capacity;
            arr = new T[capacity];
        }
        #endregion 

        #region Public methods
        public int size()
        {
            return len;
        }

        public bool isEmpty()
        {
            return (size() == 0);
        }

        // O(1) - time, O(1) - space
        public T get(int index)
        {
            return arr[index];
        }

        // O(1) - time, O(1) - space
        public void set(int index, T elem)
        {
            arr[index] = elem;
        }

        // O(n) - time, O(1) - space
        public void clear()
        {
            for (int i = 0; i < len; i++)
            {
                arr[i] = default(T);
            }

            len = 0;
        }

        // With no resize: O(1) -  time, O(1) - space
        public void add(T elem)
        {
            // Resize
            if (len + 1 >= capacity)
            {
                capacity = (capacity == 0) ? 1 : 2 * capacity;
                T[] newarr = new T[capacity];
                for (int i = 0; i < len; i++)
                {
                    newarr[i] = arr[i];
                }
                arr = newarr;
            }

            arr[len++] = elem;
        }

        // O(n) - time, O(n) - space
        public T removeAt(int index)
        {
            if (index >= len || index < 0)
                throw new IndexOutOfRangeException($"Index is out-of-range: {index}");

            T data = arr[index];
            T[] newarr = new T[len - 1];

            for (int i = 0, j = 0; i < len; i++, j++)
            {
                if (i == index) j--; // Skip over rm_index by fixing j temporarily
                else newarr[j] = arr[i];
            }

            arr = newarr;
            capacity = --len;

            return data;
        }

        // O(n) - time, O(1) - space
        public int indexOf(T obj)
        {
            for (int i = 0; i < len; i++)
            {
                if (obj == null)
                {
                    if (arr[i] == null) return i;
                }
                else
                {
                    if (obj.Equals(arr[i])) return i;
                }
            }
            return -1;
        }

        // O(n) - time, O(n) - space
        public bool remove(T obj)
        {
            int index = indexOf(obj);
            if (index == -1)
                return false;

            removeAt(index);
            return true;
        }

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
        #endregion

        #region Private enumerator class
        private class DynamicArrayEnumerator : IEnumerator<T>
        {
            T[] arrlist;
            int position = -1;
            int length;

            // Constructor
            public DynamicArrayEnumerator(DynamicArray<T> array)
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

            T IEnumerator<T>.Current
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
            return new DynamicArrayEnumerator(this);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new DynamicArrayEnumerator(this);
        }
    }
}
