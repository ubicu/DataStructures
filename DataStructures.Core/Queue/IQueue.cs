using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Core
{
    public interface IQueue<T>
    {
        void offer(T elem);

        T poll();

        T peek();

        int size();

        bool isEmpty();
    }
}
