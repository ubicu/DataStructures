﻿namespace DataStructures.Core
{
    public interface IDynamicArray<T>
    {
        void add(T elem);
        void clear();
        T get(int index);
        int indexOf(object obj);
        bool isEmpty();
        bool remove(object obj);
        T removeAt(int index);
        void set(int index, T elem);
        int size();
    }
}