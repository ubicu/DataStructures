using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Core
{
    public class DoublyLinkedList<T> : ILinkedList<T>, IEnumerable<T>
    {
        private int _size = 0;
        private Node<T> head = null;
        private Node<T> tail = null;

        #region Internal node class
        private class Node<T>
        {
            public T data;
            public Node<T> next, prev;

            public Node() : this(default(T), null, null) { }

            public Node(T data, Node<T> prev, Node<T> next)
            {
                this.data = data;
                this.next = next;
                this.prev = prev;
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
            while (trav != null)
            {
                Node<T> next = trav.next;
                trav.data = default(T);
                trav.next = null;
                trav.prev = null;
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
                head = tail = new Node<T>(elem, null, null);
            }
            else
            {
                tail.next = new Node<T>(elem, tail, null);
                tail = tail.next;
            }
            _size++;
        }

        public void addFirst(T elem)
        {
            if (isEmpty())
            {
                head = tail = new Node<T>(elem, null, null);
            }
            else
            {
                head.prev = new Node<T>(elem, null, head);
                head = head.prev;
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
            else
            {
                head.prev = null;
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
            tail = tail.prev;
            --_size;

            if (isEmpty())
            {
                head = null;
            }
            else
            {
                tail.next = null;
            }

            return data;
        }

        public T removeAt(int index)
        {
            // Make sure the index provided is valid
            if (index < 0 || index >= _size)
            {
                throw new ArgumentOutOfRangeException($"Index is out-of-range: {index}");
            }

            int i;
            Node<T> trav;

            // Search from the front of the list
            if (index < _size / 2)
            {
                trav = head;
                for(i = 0; i < index; i++)
                {
                    trav = trav.next;
                }
            }
            // Search from the end of the list
            else
            {
                trav = tail;
                for(i = _size-1; i > index; i--)
                {
                    trav = trav.prev;
                }
            }

            return remove(trav);
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
            var newNode = new Node<T>(data, trav, trav.next);
            trav.next.prev = newNode;
            trav.next = newNode;

            _size++;
        }


        public bool remove(T obj)
        {
            Node<T> trav = head;

            for (trav = head; trav != null; trav = trav.next)
            {
                // Support searching for null/non-null object
                if ((obj == null && trav.data == null) || (obj != null && obj.Equals(trav.data)))
                {
                    remove(trav);
                    return true;
                }
            }

            return false;
        }

        public bool contains(T obj)
        {
            return (indexOf(obj) != -1);
        }

        public int indexOf(T obj)
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

        private T remove(Node<T> node)
        {
            // If node is head
            if (node.prev == null)
                return removeFirst();

            // If node is tail
            if (node.next == null)
                return removeLast();

            // Make the pointers of adjacent nodes skip over 'node'
            node.next.prev = node.prev;
            node.prev.next = node.next;

            // Store data to return
            T data = node.data;

            // Memory cleanup
            node.data = default(T);
            node = node.prev = node.next = null;

            --_size;

            return data;
        }

        #region Private enumerator class
        private class DoublyLinkedListEnumerator : IEnumerator<T>
        {
            private Node<T> trav = new Node<T>();
            private Node<T> _head;

            public DoublyLinkedListEnumerator(DoublyLinkedList<T> list)
            {
                trav.next = list.head;
                _head = list.head;
            }

            public object Current => trav.data;

            T IEnumerator<T>.Current => trav.data;

            public void Dispose(){ }

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
            return new DoublyLinkedListEnumerator(this);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new DoublyLinkedListEnumerator(this);
        }
    }
}
