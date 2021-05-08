using DataStructures.Core;
using DataStructures.Core.Queue;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DataStructures.Test.Queue
{
    public class QueueTest : IDisposable
    {
        private List<IQueue<int>> queues = new List<IQueue<int>>();

        public QueueTest()
        {
            queues.Add(new ArrayQueue<int>(2));
            queues.Add(new LinkedQueue<int>());
            queues.Add(new IntQueue(2));
        }

        [Fact]
        public void TestEmptyQueue()
        {
            foreach(var queue in queues)
            {
                Assert.True(queue.isEmpty());
                Assert.Equal(0, queue.size());
            }
        }

        [Fact]
        public void TestPollOperationOnEmptyQueues()
        {
            foreach (var queue in queues)
            {
                var ex = Assert.Throws<InvalidOperationException>(() => queue.poll());
                Assert.Equal($"Queue is empty", ex.Message);
            }
        }

        [Fact]
        public void TestPeekOperationOnEmptyQueues()
        {
            foreach (var queue in queues)
            {
                var ex = Assert.Throws<InvalidOperationException>(() => queue.peek());
                Assert.Equal($"Queue is empty", ex.Message);
            }
        }

        [Fact]
        public void TestOfferAndPeekOperation()
        {
            foreach (var queue in queues)
            {
                queue.offer(2);
                Assert.Equal(1, queue.size());
                Assert.Equal(2, queue.peek());
            }
        }

        [Fact]
        public void TestPollOperation()
        {
            foreach (var queue in queues)
            {
                queue.offer(2);
                Assert.Equal(1, queue.size());
                Assert.Equal(2, queue.peek());

                Assert.Equal(2, queue.poll());
                Assert.Equal(0, queue.size());
                Assert.True(queue.isEmpty());
            }
        }

        [Fact]
        public void TestQueueOperationsExhaustively()
        {
            foreach (var queue in queues)
            {
                Assert.True(queue.isEmpty());
                queue.offer(1);
                Assert.False(queue.isEmpty());

                queue.offer(2);
                Assert.Equal(2, queue.size());
                Assert.Equal(1, queue.peek());
                Assert.Equal(2, queue.size());

                Assert.Equal(1, queue.poll());
                Assert.Equal(1, queue.size());
                Assert.Equal(2, queue.peek());
                Assert.Equal(1, queue.size());

                Assert.Equal(2, queue.poll());
                Assert.Equal(0, queue.size());
                Assert.True(queue.isEmpty());
            }
        }

        public void Dispose()
        {
            queues.Clear();
        }
    }
}
