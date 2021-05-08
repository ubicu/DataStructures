using DataStructures.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DataStructures.Test.Queue
{
    public class ArrayQueueTest : IDisposable
    {
        private ArrayQueue<int> queue;

        public ArrayQueueTest()
        {
            queue = new ArrayQueue<int>(4);
        }

        [Fact]
        public void TestEmptyQueue()
        {
            Assert.True(queue.isEmpty());
            Assert.Equal(0, queue.size());
        }

        [Fact]
        public void TestPollOperationOnEmptyQueue()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => queue.poll());
            Assert.Equal($"Queue is empty", ex.Message);
        }

        [Fact]
        public void TestOfferOperationOnFullQueue()
        {
            queue.offer(1);
            queue.offer(2);
            queue.offer(3);
            queue.offer(4);

            var ex = Assert.Throws<InvalidOperationException>(() => queue.offer(5));
            Assert.Equal($"Queue is full", ex.Message);
        }

        [Fact]
        public void TestPeekOperationOnEmptyQueue()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => queue.peek());
            Assert.Equal($"Queue is empty", ex.Message);
        }

        [Fact]
        public void TestOfferAndPeekOperation()
        {
            queue.offer(2);
            Assert.Equal(1, queue.size());
            Assert.Equal(2, queue.peek());

            queue.offer(3);
            Assert.Equal(2, queue.size());
            Assert.Equal(2, queue.peek());
        }

        [Fact]
        public void TestPollOperation()
        {
            queue.offer(2);
            queue.offer(3);
            queue.offer(4);

            Assert.Equal(3, queue.size());
            Assert.Equal(2, queue.poll());
            Assert.Equal(2, queue.size());

            Assert.Equal(3, queue.poll());
            Assert.Equal(1, queue.size());

            Assert.Equal(4, queue.poll());
            Assert.True(queue.isEmpty());
        }

        [Fact]
        public void TestQueueOperationsExhaustively()
        {
            // 1
            queue.offer(1);
            Assert.Equal(1, queue.size());
            Assert.Equal(1, queue.peek());

            int index = 0;
            var expectedQueue = new int[] { 1 };
            foreach(var elem in queue)
            {
                Assert.Equal(expectedQueue[index++], elem);
            }

            // 1 - 2
            queue.offer(2);
            Assert.Equal(2, queue.size());
            Assert.Equal(1, queue.peek());

            index = 0;
            expectedQueue = new int[] { 1, 2 };
            foreach (var elem in queue)
            {
                Assert.Equal(expectedQueue[index++], elem);
            }

            // 2
            Assert.Equal(1, queue.poll());
            Assert.Equal(2, queue.peek());

            index = 0;
            expectedQueue = new int[] { 2 };
            foreach (var elem in queue)
            {
                Assert.Equal(expectedQueue[index++], elem);
            }

            // 2 - 3
            queue.offer(3);
            Assert.Equal(2, queue.size());
            Assert.Equal(2, queue.peek());

            index = 0;
            expectedQueue = new int[] { 2, 3 };
            foreach (var elem in queue)
            {
                Assert.Equal(expectedQueue[index++], elem);
            }

            // 2 - 3 - 4
            queue.offer(4);
            Assert.Equal(3, queue.size());
            Assert.Equal(2, queue.peek());

            index = 0;
            expectedQueue = new int[] { 2, 3, 4 };
            foreach (var elem in queue)
            {
                Assert.Equal(expectedQueue[index++], elem);
            }

            // 3 - 4
            Assert.Equal(2, queue.poll());
            Assert.Equal(2, queue.size());
            Assert.Equal(3, queue.peek());

            index = 0;
            expectedQueue = new int[] { 3, 4 };
            foreach (var elem in queue)
            {
                Assert.Equal(expectedQueue[index++], elem);
            }

            // 3 - 4 - 5
            queue.offer(5);
            Assert.Equal(3, queue.size());
            Assert.Equal(3, queue.peek());

            index = 0;
            expectedQueue = new int[] { 3, 4, 5 };
            foreach (var elem in queue)
            {
                Assert.Equal(expectedQueue[index++], elem);
            }

            // 3 - 4 - 5 - 6
            queue.offer(6);
            Assert.Equal(4, queue.size());
            Assert.Equal(3, queue.peek());
            Assert.True(queue.isFull());

            index = 0;
            expectedQueue = new int[] { 3, 4, 5, 6 };
            foreach (var elem in queue)
            {
                Assert.Equal(expectedQueue[index++], elem);
            }

            // 4 - 5 - 6
            Assert.Equal(3, queue.poll());
            Assert.Equal(3, queue.size());
            Assert.Equal(4, queue.peek());

            index = 0;
            expectedQueue = new int[] { 4, 5, 6 };
            foreach (var elem in queue)
            {
                Assert.Equal(expectedQueue[index++], elem);
            }

            // 5 - 6
            Assert.Equal(4, queue.poll());
            Assert.Equal(2, queue.size());
            Assert.Equal(5, queue.peek());

            index = 0;
            expectedQueue = new int[] { 5, 6 };
            foreach (var elem in queue)
            {
                Assert.Equal(expectedQueue[index++], elem);
            }

            // 6
            Assert.Equal(5, queue.poll());
            Assert.Equal(1, queue.size());
            Assert.Equal(6, queue.peek());

            index = 0;
            expectedQueue = new int[] { 6 };
            foreach (var elem in queue)
            {
                Assert.Equal(expectedQueue[index++], elem);
            }

            // []
            Assert.Equal(6, queue.poll());
            Assert.Equal(0, queue.size());
            Assert.True(queue.isEmpty());

            index = 0;
            expectedQueue = new int[] {  };
            foreach (var elem in queue)
            {
                Assert.Equal(expectedQueue[index++], elem);
            }
        }

        public void Dispose()
        {
        }
    }
}
