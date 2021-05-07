using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Core
{
    public interface ILinkedList<T>
    {
        T get(int index);
        void clear();
        int size();
        bool isEmpty();
        void add(T elem);
        void addLast(T elem);
        void addFirst(T elem);
        void addAt(int index, T data);
        T peekFirst();
        T peekLast();
        T removeFirst();
        T removeLast();
        bool remove(T obj);
        T removeAt(int index);
        int indexOf(T obj);
        bool contains(T obj);

    }
}
