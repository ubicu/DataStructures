using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Text;

namespace DataStructures.Core
{
    public class SinglyLinkedList<T> : ILinkedList<T>, IEnumerable<T>
    {
        private int _size = 0;
        private Node<T> head = null;
        private Node<T> tail = null;

        #region Internal node class
        private class Node<T>
        {
            public T data;
            public Node<T> next;

            public Node() : this(default(T), null){}

            public Node(T data) : this(data, null) { }

            public Node(T data, Node<T> next)
            {
                this.data = data;
                this.next = next;
            }

            public override string ToString()
            {
                return data.ToString();
            }
        }
        #endregion

        #region Public methods
        public T get(int index)
        {
            // Make sure list is not empty
            if (isEmpty())
            {
                throw new InvalidOperationException("Empty list");
            }

            // Make sure the index provided is valid
            if (index < 0 || index >= _size)
            {
                throw new ArgumentOutOfRangeException($"Index is out-of-range: {index}");
            }

            Node<T> trav = head;
            for (int i = 0; i < index; i++)
            {
                trav = trav.next;
            }

            return trav.data;
        }


        // Empty linked list, O(n) - time
        public void clear()
        {
            Node<T> trav = head;
            while(trav != null)
            {
                Node<T> next = trav.next;
                trav.data = default(T);
                trav.next = null;
                trav = next;
            }

            _size = 0;
            head = null;
            tail = null;
            trav = null;
        }

        public int size()
        {
            return _size;
        }

        public bool isEmpty()
        {
            return (size() == 0);
        }

        public void add(T elem)
        {
            addLast(elem);
        }

        public void addLast(T elem)
        {
            if (isEmpty())
            {
                head = tail = new Node<T>(elem, null);
            }
            else
            {
                tail.next = new Node<T>(elem, null);
                tail = tail.next;
            }
            _size++;
        }

        public void addFirst(T elem)
        {
            if (isEmpty())
            {
                head = tail = new Node<T>(elem, null);
            }
            else
            {
                head = new Node<T>(elem, head);
            }
            _size++;
        }

        public T peekFirst()
        {
            if (isEmpty())
            {
                throw new InvalidOperationException("Empty list");
            }
            else
                return head.data;
        }

        public T peekLast()
        {
            if (isEmpty())
            {
                throw new InvalidOperationException("Empty list");
            }
            else
                return tail.data;
        }

        public T removeFirst()
        {
            if (isEmpty())
            {
                throw new InvalidOperationException("Empty list");
            }

            T data = head.data;
            head = head.next;
            --_size;

            if (isEmpty())
            {
                tail = null;
            }

            return data;
        }

        public T removeLast()
        {
            if (isEmpty())
            {
                throw new InvalidOperationException("Empty list");
            }

            T data = tail.data;

            if (size() == 1)
            {
                head = tail = null;
            }
            else
            {
                // Traverse linked list until just before tail
                Node<T> trav = head;
                while (trav.next != tail)
                {
                    trav = trav.next;
                }
                tail = trav;
                trav.next = null;
            }
            --_size;

            return data;
        }

        public T removeAt(int index)
        {
            // Make sure the index provided is valid
            if (index < 0 || index >= _size)
            {
                throw new ArgumentOutOfRangeException($"Index is out-of-range: {index}");
            }

            if (index == 0)
                return removeFirst();

            if (index == _size - 1)
                return removeLast();

            // Traverse linked list around index to be removed
            Node<T> rear = head;
            Node<T> front = head.next;
            for (int i = 0; i < index-1; i++)
            {
                rear = rear.next;
                front = front.next;
            }

            T data = front.data;

            Node<T> tmp = front; // Store location for later removal
            front = front.next;
            rear.next = front;

            // Clear removed node
            tmp.next = null;
            tmp.data = default(T);

            --_size;

            return data;
        }

        public void addAt(int index, T data)
        {
            // Operation is only allowed for indices: 0, 1,.., currentsize
            if (index < 0 || index > _size)
                throw new ArgumentOutOfRangeException($"Illegal Index: {index}");

            if (index == 0)
            {
                addFirst(data);
                return;
            }

            if (index == _size)
            {
                addLast(data);
                return;
            }

            // Iterate to position just before index
            Node<T> trav = head;
            for (int i = 0; i < index - 1; i++)
            {
                trav = trav.next;
            }
            trav.next = new Node<T>(data, trav.next);

            _size++;
        }


        public bool remove(object obj)
        {
            Node<T> rear = head;
            Node<T> front;

            if (isEmpty())
                return false;

            if ((obj == null && head.data == null) || (obj != null && obj.Equals(head.data)))
            {
                removeFirst();
                return true;
            }

            for (rear = head, front = head.next; front != null; rear = rear.next, front = front.next)
            {
                if ((obj == null && front.data == null) || (obj != null && obj.Equals(front.data)))
                {
                    rear.next = front.next;
                    --_size;

                    return true;
                }
            }

            return false;
        }

        public bool contains(object obj)
        {
            return (indexOf(obj) != -1);
        }

        public int indexOf(object obj)
        {
            int index = 0;
            Node<T> trav = new Node<T>();
            for (trav = head; trav != null; trav = trav.next)
            {
                if ((obj == null && trav.data == null) || (obj != null && obj.Equals(trav.data)))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[ ");
            Node<T> trav = head;
            while (trav != null)
            {
                sb.Append(trav.data);
                if (trav.next != null)
                {
                    sb.Append(", ");
                }
                trav = trav.next;
            }
            sb.Append(" ]");

            return sb.ToString();
        }

        #endregion

        #region Private enumerator class
        private class SinglyLinkedListEnumerator : IEnumerator<T>
        {
            private Node<T> trav = new Node<T>();
            private Node<T> _head;

            public SinglyLinkedListEnumerator(SinglyLinkedList<T> list)
            {
                trav.next = list.head;
                _head = list.head;
            }

            public object Current => trav.data;

            T IEnumerator<T>.Current => trav.data;

            public void Dispose(){}

            public bool MoveNext()
            {
                trav = trav.next;
                return (trav != null);
            }

            public void Reset()
            {
                trav.next = _head;
            }
        }

        #endregion

        public IEnumerator GetEnumerator()
        {
            return new SinglyLinkedListEnumerator(this);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new SinglyLinkedListEnumerator(this);
        }
    }
}
