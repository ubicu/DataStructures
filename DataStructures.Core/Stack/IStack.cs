using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Core
{
    public interface IStack<T>
    {
        int size();
        bool isEmpty();
        void push(T elem);
        T pop();
        T peek();
    }
}
