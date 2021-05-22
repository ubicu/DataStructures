using DataStructures.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DataStructures.Test
{
    public class BinaryHeapTest
    {
        private static readonly int LOOPS = 100;
        private static readonly int MAX_SZ = 100;

        [Fact]
        public void TestEmptyHeap()
        {
            BinaryHeap<Int32> q = new BinaryHeap<Int32>();

            Assert.Equal(0, q.size());
            Assert.True(q.isEmpty());
            Assert.Equal(default(Int32), q.poll());
            Assert.Equal(default(Int32), q.peek());
        }
    }
}
