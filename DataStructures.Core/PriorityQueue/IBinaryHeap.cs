using System;

namespace DataStructures.Core
{
    public interface IBinaryHeap<T> where T : IComparable<T>
    {
        void add(T elem);
        void clear();
        bool contains(T elem);
        bool isEmpty();
        bool isMinHeap(int k);
        T peek();
        T poll();
        bool remove(T element);
        int size();
    }
}