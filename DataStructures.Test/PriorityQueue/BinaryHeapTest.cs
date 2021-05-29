using DataStructures.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DataStructures.Test.PriorityQueue
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

                #region Note about .NET PriorityQueue implementation
                /* 
                Note: .NET Core does not have a native implementation
                of Priority Queues. Code from a Visual Studio Magazine
                article is used instead.
                */
                #endregion

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

        [Fact]
        public void TestClearOperation()
        {
            var strs = new string[] { "aa", "bb", "cc", "dd", "ee" };
            var q = new BinaryHeap<string>(strs);

            q.clear();

            Assert.Equal(0, q.size());
            Assert.True(q.isEmpty());
        }

        [Fact]
        public void TestContainment()
        {
            var strs = new string[] { "aa", "bb", "cc", "dd", "ee" };
            var q = new BinaryHeap<string>(strs);

            foreach(var str in strs)
            {
                Assert.True(q.contains(str));
                q.remove(str);
                Assert.False(q.contains(str));
            }

            Assert.True(q.isEmpty());
        }

        [Fact]
        public void TestContainmentRandomized()
        {
            #region Note about .NET PriorityQueue implementation
            /*
            Note: .NET Core does not have a native implementation
            of Priority Queues. Code from a Visual Studio Magazine
            article is used instead.
            However, the priority queue(from VS Magazine) does not
            have support for a remove operation.
            */
            #endregion

            for (int i = 0; i < LOOPS; i++)
            {
                var randNums = GenUniqueRandList(100);
                var pq = new BinaryHeap<int>();

                foreach (var num in randNums)
                {
                    pq.add(num);
                }

                foreach (var randVal in randNums)
                {
                    Assert.True(pq.contains(randVal));
                    pq.remove(randVal);
                    Assert.False(pq.contains(randVal));
                    Assert.True(pq.isMinHeap(0));
                }
            }
        }

        [Fact]
        public void TestRemovingOperation()
        {
            var arr = new int[]{ 1, 2, 3, 4, 5, 6, 7};
            var removeOrder = new int[]{ 1, 3, 6, 4, 5, 7, 2 };
            SequentialRemoving(arr, removeOrder);

            arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            removeOrder = new int[] { 7, 4, 6, 10, 2, 5, 11, 3, 1, 8, 9 };
            SequentialRemoving(arr, removeOrder);

            arr = new int[] { 8, 1, 3, 3, 5, 3 };
            removeOrder = new int[] { 3, 3, 5, 8, 1, 3 };
            SequentialRemoving(arr, removeOrder);

            arr = new int[] { 7, 7, 3, 1, 1, 2 };
            removeOrder = new int[] { 2, 7, 1, 3, 7, 1 };
            SequentialRemoving(arr, removeOrder);

            arr = new int[] { 32, 66, 93, 42, 41, 91, 54, 64, 9, 35 };
            removeOrder = new int[] { 64, 93, 54, 41, 35, 9, 66, 42, 32, 91 };
            SequentialRemoving(arr, removeOrder);
        }

        [Fact]
        public void TestRemovingDuplicates()
        {
            var array = new int[] { 2, 7, 2, 11, 7, 13, 2 };
            var pq = new BinaryHeap<int>(array);

            Assert.Equal(2, pq.peek());

            pq.add(3);

            Assert.Equal(2, pq.poll());
            Assert.Equal(2, pq.poll());
            Assert.Equal(2, pq.poll());

            Assert.Equal(3, pq.poll());

            Assert.Equal(7, pq.poll());
            Assert.Equal(7, pq.poll());

            Assert.Equal(11, pq.poll());
            Assert.Equal(13, pq.poll());
        }

        [Fact]
        public void TestRandomizedPolling()
        {
            #region Note about .NET PriorityQueue implementation
            /* 
            Note: .NET Core does not have a native implementation
            of Priority Queues. Code from a Visual Studio Magazine
            article is used instead.
            */
            #endregion

            for (int i = 0; i < LOOPS; i++)
            {
                var randNums = GenRandList(i);

                var pq1 = new PriorityQueue<int>();
                var pq2 = new BinaryHeap<int>();

                foreach(var value in randNums)
                {
                    pq1.Enqueue(value);
                    pq2.add(value);
                }

                while (!pq1.IsEmpty())
                {
                    Assert.True(pq2.isMinHeap(0));
                    Assert.Equal(pq1.size(), pq2.size());
                    Assert.Equal(pq1.Peek(), pq2.peek());
                    Assert.Equal(pq1.contains(pq1.Peek()), pq2.contains(pq2.peek()));

                    var v1 = pq1.Dequeue();
                    var v2 = pq2.poll();

                    Assert.Equal(v1, v2);
                    Assert.Equal(pq1.Peek(), pq2.peek());
                    Assert.Equal(pq1.size(), pq2.size());

                    Assert.True(pq2.isMinHeap(0));
                }

            }

        }

        [Fact]
        public void TestRandomizedRemoving()
        {
            #region Note about .NET PriorityQueue implementation
            /*
            Note: .NET Core does not have a native implementation
            of Priority Queues. Code from a Visual Studio Magazine
            article is used instead.
            However, the priority queue(from VS Magazine) does not
            have support for a remove operation.
            */
            #endregion

            for (int i = 0; i < LOOPS; i++)
            {
                var randNums = GenRandList(i);
                var pq = new BinaryHeap<int>();

                foreach (var value in randNums)
                {
                    pq.add(value);
                }

                //Collections.shuffle(randNums);
                randNums = randNums.OrderBy(l => _random.Next()).ToList();

                int index = 0;
                while (!pq.isEmpty())
                {
                    var removeNum = randNums[index++];

                    Assert.True(pq.isMinHeap(0));
                    pq.remove(removeNum);
                    Assert.True(pq.isMinHeap(0));
                }
            }
        }

        [Fact]
        public void TestPriorityQueueReusability()
        {
            var SZs = GenUniqueRandList(LOOPS);
            var pq = new BinaryHeap<int>();

            foreach (var sz in SZs)
            {
                pq.clear();

                var nums = GenRandList(sz);
                foreach (var num in nums)
                {
                    pq.add(num);
                }

                //Collections.shuffle(randNums);
                nums = nums.OrderBy(l => _random.Next()).ToList();

                for (int i = 0; i < sz / 2; i++)
                {
                    // Sometimes add a new number into the BinaryHeap
                    if (0.25 < _random.NextDouble())
                    {
                        int randNum = (int)(_random.NextDouble() * 10000);
                        pq.add(randNum);
                    }

                    int removeNum = nums[i];

                    Assert.True(pq.isMinHeap(0));
                    pq.remove(removeNum);
                    Assert.True(pq.isMinHeap(0));
                }
            }
        }

        #region Private functions

        private void SequentialRemoving(int[] array, int[] removeOrder)
        {
            #region Note about .NET PriorityQueue implementation
            /*
            Note: .NET Core does not have a native implementation
            of Priority Queues. Code from a Visual Studio Magazine
            article is used instead.
            However, the priority queue(from VS Magazine) does not
            have support for a remove operation.
            */
            #endregion

            Assert.Equal(array.Length, removeOrder.Length);

            var pq = new BinaryHeap<int>(array);
            Assert.True(pq.isMinHeap(0));

            foreach(var elem in removeOrder)
            {
                pq.remove(elem);
                Assert.True(pq.isMinHeap(0));
            }

            Assert.True(pq.isEmpty());
        }

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
