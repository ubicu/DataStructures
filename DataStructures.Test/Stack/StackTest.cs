using DataStructures.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DataStructures.Test.Stack
{
    public class StackTest : IDisposable
    {
        private List<IStack<int>> stacks = new List<IStack<int>>();

        public StackTest()
        {
            stacks.Add(new ListStack<int>());
            stacks.Add(new ArrayStack<int>());
            stacks.Add(new IntStack(2));
        }

        [Fact]
        public void TestEmptyStackOperation()
        {
            foreach (var stack in stacks)
            {
                Assert.True(stack.isEmpty());
                Assert.Equal(0, stack.size());
            }
        }

        [Fact]
        public void TestPopOperation_EmptyStacks()
        {
            foreach (var stack in stacks)
            {
                var ex = Assert.Throws<InvalidOperationException>(() => stack.pop());
                Assert.Equal($"Stack is empty", ex.Message);
            }
        }

        [Fact]
        public void TestPeekOperation_EmptyStacks()
        {
            foreach (var stack in stacks)
            {
                var ex = Assert.Throws<InvalidOperationException>(() => stack.peek());
                Assert.Equal($"Stack is empty", ex.Message);
            }
        }

        [Fact]
        public void TestPushAndPeekOperation()
        {
            foreach (var stack in stacks)
            {
                stack.push(2);
                Assert.Equal(1, stack.size());
                Assert.Equal(2, stack.peek());
            }
        }

        [Fact]
        public void TestPopOperation()
        {
            foreach (var stack in stacks)
            {
                stack.push(2);
                Assert.Equal(2, stack.pop());
                Assert.True(stack.isEmpty());
            }
        }

        [Fact]
        public void TestMultipleOperationsExhaustively()
        {
            foreach (var stack in stacks)
            {
                Assert.True(stack.isEmpty());
                stack.push(1);
                Assert.False(stack.isEmpty());
                stack.push(2);
                Assert.Equal(2, stack.size());
                Assert.Equal(2, stack.peek());
                Assert.Equal(2, stack.size());
                Assert.Equal(2, stack.pop());
                Assert.Equal(1, stack.size());
                Assert.Equal(1, stack.peek());
                Assert.Equal(1, stack.size());
                Assert.Equal(1, stack.pop());
                Assert.Equal(0, stack.size());
                Assert.True(stack.isEmpty());
            }
        }

        public void Dispose()
        {
            stacks.Clear();
        }
    }
}
