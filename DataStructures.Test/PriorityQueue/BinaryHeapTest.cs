using DataStructures.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DataStructures.Test
{
    public class BinaryHeapTest
    {
        private static readonly int LOOPS = 100;
        private static readonly int MAX_SZ = 100;

        // Instantiate random number generator.  
        private readonly Random _random = new Random();

        [Fact]
        public void TestEmptyHeap()
        {
            BinaryHeap<Int32> q = new BinaryHeap<Int32>();

            Assert.Equal(0, q.size());
            Assert.True(q.isEmpty());
            Assert.Equal(default(Int32), q.poll());
            Assert.Equal(default(Int32), q.peek());
        }

        [Fact]
        public void TestHeapProperty()
        {
            BinaryHeap<int> q = new BinaryHeap<int>();
            var nums = new int[] { 3, 2, 5, 6, 7, 9, 4, 8, 1 };

            // Try manually creating heap
            foreach(var num in nums)
            {
                q.add(num);
            }

            for(int i = 1; i <= 9; i++)
            {
                Assert.Equal(i, q.poll());
            }

            q.clear();

            q = new BinaryHeap<int>(nums);
            for (int i = 1; i <= 9; i++)
            {
                Assert.Equal(i, q.poll());
            }
        }

        [Fact]
        public void TestHeapifyOperation()
        {
            for (int i = 1; i < LOOPS; i++)
            {
                var lst = GenRandArray(i);
                var pq1 = new BinaryHeap<int>(lst);

                // Note: .NET Core does not have a native implementation
                // of Priority Queues. Code from a Visual Studio Magazine
                // article is used instead.
                var pq2 = new PriorityQueue<int>();
                foreach(var elem in lst)
                {
                    pq2.Enqueue(elem);
                }

                Assert.True(pq1.isMinHeap(0));
                while (!pq2.IsEmpty())
                {
                    Assert.Equal(pq1.poll(), pq2.Dequeue());
                }
            }
        }

        #region Private functions

        // Generate an array of random numbers
        private int[] GenRandArray(int size)
        {
            var lst = new int[size];

            for(int i = 0; i < size; i++)
            {
                lst[i] = (int)(_random.NextDouble() * MAX_SZ);
            }

            return lst;
        }

        // Generate a list of random numbers
        private List<int> GenRandList(int size)
        {
            var lst = new List<int>();

            for (int i = 0; i < size; i++)
            {
                lst.Add((int)(_random.NextDouble() * MAX_SZ));
            }

            return lst;
        }

        // Generate a list of unique random numbers
        private List<int> GenUniqueRandList(int size)
        {
            var lst = new List<int>();

            for (int i = 0; i < size; i++)
            {
                lst.Add(i);
            }

            // Collections.Shuffle
            lst = lst.OrderBy(l => _random.Next()).ToList();

            return lst;
        }

        #endregion
    }
}
