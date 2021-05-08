using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures.Core
{
    public class LinkedQueue<T> : IQueue<T>, IEnumerable<T>
    {
        private List<T> list = new List<T>();

        #region Constructors
        public LinkedQueue() { }

        public LinkedQueue(T firstElem)
        {
            offer(firstElem);
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

        // Add an element to the back of the queue
        public void offer(T elem)
        {
            list.Add(elem);
        }

        // Peek the element at the front of the queue
        // The method throws an error is the queue is empty
        public T peek()
        {
            if (isEmpty())
                throw new InvalidOperationException("Queue is empty");

            return list.First();
        }

        // Poll an element from the front of the queue
        // The method throws an error is the queue is empty
        public T poll()
        {
            if (isEmpty())
                throw new InvalidOperationException("Queue is empty");

            int firstIndex = 0;
            T data = list[firstIndex];
            list.RemoveAt(firstIndex);

            return data;
        }
        #endregion

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
