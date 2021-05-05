using DataStructures.Core;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DataStructures.Test.LinkedList
{
    public class DoublyLinkedListTest
    {
        private static readonly int LOOPS = 10000;
        private static readonly int TEST_SZ = 40;
        private static readonly int NUM_NULLS = TEST_SZ / 5;
        private static readonly int MAX_RAND_NUM = 250;

        // Instantiate random number generator.  
        private readonly Random _random = new Random();

        [Fact]
        public void TestEmptyList()
        {
            var list = new DoublyLinkedList<int>();

            Assert.True(list.isEmpty());
            Assert.Equal(0, list.size());
        }

        [Fact]
        public void TestRemoveFirstOperationIfEmptyList()
        {
            var list = new DoublyLinkedList<int>();

            var ex = Assert.Throws<InvalidOperationException>(() => list.removeFirst());
            Assert.Equal("Empty list", ex.Message);
        }

        [Fact]
        public void TestRemoveLastOperationIfEmptyList()
        {
            var list = new DoublyLinkedList<int>();

            var ex = Assert.Throws<InvalidOperationException>(() => list.removeLast());
            Assert.Equal("Empty list", ex.Message);
        }

        [Fact]
        public void TestPeekFirstOperationIfEmptyList()
        {
            var list = new DoublyLinkedList<int>();

            var ex = Assert.Throws<InvalidOperationException>(() => list.peekFirst());
            Assert.Equal("Empty list", ex.Message);
        }

        [Fact]
        public void TestPeekLastOperationIfEmptyList()
        {
            var list = new DoublyLinkedList<int>();

            var ex = Assert.Throws<InvalidOperationException>(() => list.peekLast());
            Assert.Equal("Empty list", ex.Message);
        }

        [Fact]
        public void TestAddFirstOperation()
        {
            var list = new DoublyLinkedList<int>();

            list.addFirst(3);
            Assert.Equal(1, list.size());
            Assert.Equal(3, list.get(0));

            list.addFirst(5);
            Assert.Equal(2, list.size());
            Assert.Equal(5, list.get(0));
            Assert.Equal(3, list.get(1));
        }

        [Fact]
        public void TestAddLastOperation()
        {
            var list = new DoublyLinkedList<int>();

            list.addLast(3);
            Assert.Equal(1, list.size());
            Assert.Equal(3, list.get(0));

            list.addLast(5);
            Assert.Equal(2, list.size());
            Assert.Equal(3, list.get(0));
            Assert.Equal(5, list.get(1));
        }

        [Fact]
        public void TestRemoveFirstOperation()
        {
            var list = new DoublyLinkedList<int>();

            list.addFirst(3);
            list.addFirst(5);

            Assert.Equal(5, list.removeFirst());
            Assert.Equal(3, list.get(0));

            Assert.Equal(3, list.removeFirst());
            Assert.True(list.isEmpty());
        }

        [Fact]
        public void TestRemoveLastOperation()
        {
            var list = new DoublyLinkedList<int>();

            list.addFirst(3);
            list.addFirst(5);

            Assert.Equal(3, list.removeLast());
            Assert.Equal(5, list.get(0));

            Assert.Equal(5, list.removeLast());
            Assert.True(list.isEmpty());
        }

        [Fact]
        public void TestPeekFirstOperation()
        {
            var list = new DoublyLinkedList<int>();

            list.addFirst(4);
            Assert.Equal(4, list.peekFirst());
            Assert.Equal(1, list.size());
        }

        [Fact]
        public void TestPeekLastOperation()
        {
            var list = new DoublyLinkedList<int>();

            list.addFirst(4);
            Assert.Equal(4, list.peekLast());
            Assert.Equal(1, list.size());
        }

        [Fact]
        public void TestPeeking_SuccessiveInserts()
        {
            var list = new DoublyLinkedList<int>();

            // 5
            list.addFirst(5);
            Assert.Equal(5, list.peekFirst());
            Assert.Equal(5, list.peekLast());

            // 6 - 5
            list.addFirst(6);
            Assert.Equal(6, list.peekFirst());
            Assert.Equal(5, list.peekLast());

            // 7 - 6 - 5
            list.addFirst(7);
            Assert.Equal(7, list.peekFirst());
            Assert.Equal(5, list.peekLast());

            // 7 - 6 - 5 - 8
            list.addLast(8);
            Assert.Equal(7, list.peekFirst());
            Assert.Equal(8, list.peekLast());

            // 7 - 6 - 5
            list.removeLast();
            Assert.Equal(7, list.peekFirst());
            Assert.Equal(5, list.peekLast());

            // 7 - 6
            list.removeLast();
            Assert.Equal(7, list.peekFirst());
            Assert.Equal(6, list.peekLast());

            // 6
            list.removeFirst();
            Assert.Equal(6, list.peekFirst());
            Assert.Equal(6, list.peekLast());
        }

        [Fact]
        public void TestRemoveAtOperation()
        {
            var list = new DoublyLinkedList<int>();

            list.add(1);
            list.add(2);
            list.add(3);
            list.add(4);

            list.removeAt(0);
            list.removeAt(2);
            Assert.Equal(2, list.peekFirst());
            Assert.Equal(3, list.peekLast());

            list.removeAt(1);
            list.removeAt(0);

            Assert.Equal(0, list.size());
        }

        [Fact]
        public void TestClearOperation()
        {
            var list = new DoublyLinkedList<int>();

            list.add(22);
            list.add(33);
            list.add(44);
            Assert.Equal(3, list.size());

            list.clear();
            Assert.Equal(0, list.size());

            list.add(22);
            list.add(33);
            list.add(44);
            Assert.Equal(3, list.size());

            list.clear();
            Assert.Equal(0, list.size());
        }

        [Fact]
        public void TestAddAtOperation()
        {
            var list = new DoublyLinkedList<int>();

            // 1
            list.addAt(0, 1);
            Assert.Equal(1, list.size());
            Assert.Equal(1, list.get(0));

            // 1 - 2
            list.addAt(1, 2);
            Assert.Equal(2, list.size());
            Assert.Equal(1, list.get(0));
            Assert.Equal(2, list.get(1));

            // 1 - 3 - 2
            list.addAt(1, 3);
            Assert.Equal(3, list.size());
            Assert.Equal(1, list.get(0));
            Assert.Equal(3, list.get(1));
            Assert.Equal(2, list.get(2));

            // 1 - 4 - 3 - 2
            list.addAt(1, 4);
            Assert.Equal(4, list.size());
            Assert.Equal(1, list.get(0));
            Assert.Equal(4, list.get(1));
            Assert.Equal(3, list.get(2));
            Assert.Equal(2, list.get(3));

        }

        [Fact]
        public void TestRemoveObjectOperation()
        {
            var strs = new DoublyLinkedList<string>();

            strs.add("a");
            strs.add("b");
            strs.add("c");
            strs.add("d");
            strs.add("e");
            strs.add("f");
            strs.remove("b");
            strs.remove("a");
            strs.remove("d");
            strs.remove("e");
            strs.remove("c");
            strs.remove("f");

            Assert.True(strs.isEmpty());
        }

        [Fact]
        public void TestRandomizedRemoveOperation()
        {
            var list = new DoublyLinkedList<int?>();
            var LIST = new List<int?>();

            for (int loops = 0; loops < LOOPS; loops++)
            {
                list.clear();
                LIST.Clear();

                List<int?> randNums = genRandList(TEST_SZ);
                foreach (var value in randNums)
                {
                    list.add(value);
                    LIST.Add(value);
                }

                // Collections.Shuffle
                randNums = randNums.OrderBy(l => _random.Next()).ToList();

                for (int i = 0; i < randNums.Count; i++)
                {
                    var rm_val = randNums[i];
                    Assert.Equal(LIST.Remove(rm_val), list.remove(rm_val));
                    Assert.Equal(LIST.Count, list.size());

                    var iter1 = LIST.GetEnumerator();
                    var iter2 = list.GetEnumerator();

                    while (iter1.MoveNext())
                    {
                        iter2.MoveNext();
                        Assert.Equal(iter1.Current, iter2.Current);
                    }
                }
            }
        }

        [Fact]
        public void TestRandomizedRemoveAtOperation()
        {
            var list = new DoublyLinkedList<int?>();
            var LIST = new List<int?>();

            for (int loops = 0; loops < LOOPS; loops++)
            {
                list.clear();
                LIST.Clear();

                List<int?> randNums = genRandList(TEST_SZ);
                foreach (var value in randNums)
                {
                    list.add(value);
                    LIST.Add(value);
                }

                for (int i = 0; i < randNums.Count; i++)
                {
                    int rm_index = (int)(list.size() * _random.NextDouble());

                    var num1 = LIST[rm_index];
                    LIST.RemoveAt(rm_index);
                    var num2 = list.removeAt(rm_index);
                    Assert.Equal(num1, num2);
                    Assert.Equal(LIST.Count, list.size());

                    var iter1 = LIST.GetEnumerator();
                    var iter2 = list.GetEnumerator();

                    while (iter1.MoveNext())
                    {
                        iter2.MoveNext();
                        Assert.Equal(iter1.Current, iter2.Current);
                    }
                }
            }
        }

        [Fact]
        public void TestRandomizedIndexOfOperation()
        {
            var list = new DoublyLinkedList<int?>();
            var LIST = new List<int?>();

            for (int loops = 0; loops < LOOPS; loops++)
            {
                list.clear();
                LIST.Clear();

                List<int?> randNums = genRandList(TEST_SZ);
                foreach (var value in randNums)
                {
                    list.add(value);
                    LIST.Add(value);
                }

                // Collections.Shuffle
                randNums = randNums.OrderBy(l => _random.Next()).ToList();

                for (int i = 0; i < randNums.Count; i++)
                {
                    var elem = randNums[i];

                    var num1 = LIST.IndexOf(elem);
                    var num2 = list.indexOf(elem);
                    Assert.Equal(num1, num2);
                    Assert.Equal(LIST.Count, list.size());

                    var iter1 = LIST.GetEnumerator();
                    var iter2 = list.GetEnumerator();

                    while (iter1.MoveNext())
                    {
                        iter2.MoveNext();
                        Assert.Equal(iter1.Current, iter2.Current);
                    }
                }
            }
        }

        [Fact]
        public void TestToStringOperation()
        {
            var strs = new DoublyLinkedList<string>();

            Assert.Equal("[  ]", strs.ToString());

            strs.add("a");
            Assert.Equal("[ a ]", strs.ToString());

            strs.add("b");
            Assert.Equal("[ a, b ]", strs.ToString());

            strs.add("c");
            strs.add("d");
            strs.add("e");
            strs.add("f");
            Assert.Equal("[ a, b, c, d, e, f ]", strs.ToString());
        }

        [Fact]
        public void TestIEnumerableImplementation()
        {
            var list = new DoublyLinkedList<String>();
            var strs = new string[] { "a", "b", "c", "d", "e", null, "g", "h" };
            for (int i = 0; i < strs.Length; i++)
            {
                list.add(strs[i]);
            }

            int ind = 0;
            foreach (var elem in list)
            {
                Assert.Equal(strs[ind++], elem);
            }
        }


        #region Private helper functions
        private List<int?> genRandList(int sz)
        {
            var lst = new List<int?>();

            for (int i = 0; i < sz; i++)
            {
                lst.Add((int)(_random.NextDouble() * MAX_RAND_NUM));
            }
            for (int i = 0; i < NUM_NULLS; i++) 
            {
                lst.Add(null);
            }

            // Collections.Shuffle
            lst = lst.OrderBy(l => _random.Next()).ToList();

            return lst;
        }

        private List<int?> genUniqueRandList(int sz)
        {
            var lst = new List<int?>();

            for (int i = 0; i < sz; i++)
            {
                lst.Add(i);
            }
            for (int i = 0; i < NUM_NULLS; i++)
            {
                lst.Add(null);
            }

            // Collections.Shuffle
            lst = lst.OrderBy(l => _random.Next()).ToList();

            return lst;
        }
        #endregion
    }
}
