using DataStructures.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Test.Stack
{
    public class StackTest : IDisposable
    {
        private List<IStack<int>> stacks = new List<IStack<int>>();

        public StackTest()
        {

        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
