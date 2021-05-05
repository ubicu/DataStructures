using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace DataStructures.Core
{
    public class ListStack<T> : IStack<T>, IEnumerable<T>
    {
        private List<T> list = new List<T>();

        #region Constructors
        public ListStack() { }

        public ListStack(T firstElem)
        {
            push(firstElem);
        }
        #endregion

        #region Public methods
        public int size()
        {
            return list.Count;
        }

        public bool isEmpty()
        {
            return (size() == 0);
        }

        public void push(T elem)
        {
            list.Add(elem);
        }

        public T pop()
        {
            if (isEmpty())
                throw new InvalidOperationException("Stack is empty");

            int lastIndex = size() - 1;
            T data = list[lastIndex];
            list.RemoveAt(lastIndex);

            return data;
        }

        public T peek()
        {
            if (isEmpty())
                throw new InvalidOperationException("Stack is empty");

            int lastIndex = size() - 1;
            return list[lastIndex];
        }
        #endregion

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
