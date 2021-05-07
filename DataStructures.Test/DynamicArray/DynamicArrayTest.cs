using DataStructures.Core;
using System;
using System.Linq;
using Xunit;

namespace DataStructures.Test.DynamicArray
{
    public class DynamicArrayTest
    {
        [Fact]
        public void TestEmptyList()
        {
            var dynamicArray = new DynamicArray<int>();

            Assert.True(dynamicArray.isEmpty());
        }

        [Fact]
        public void TestRemovingIfEmpty()
        {
            var dynamicArray = new DynamicArray<int>();

            int index = 0;
            var ex = Assert.Throws<IndexOutOfRangeException>(() => dynamicArray.removeAt(index));
            Assert.Equal($"Index is out-of-range: {index}", ex.Message);
        }

        [Fact]
        public void TestIndexOutOfBounds_IndexIsGreaterThanLength()
        {
            var dynamicArray = new DynamicArray<int>();

            dynamicArray.add(-56);
            dynamicArray.add(-53);
            dynamicArray.add(-55);

            int index = 3;
            var ex = Assert.Throws<IndexOutOfRangeException>(() => dynamicArray.removeAt(index));
            Assert.Equal($"Index is out-of-range: {index}", ex.Message);
        }

        [Fact]
        public void TestIndexOutOfBounds_LargeDynamicArray_IndexIsGreaterThanLength()
        {
            var dynamicArray = new DynamicArray<int>();

            for (int i = 0; i < 1000; i++)
            {
                dynamicArray.add(789);
            }

            int index = 1000;
            var ex = Assert.Throws<IndexOutOfRangeException>(() => dynamicArray.removeAt(index));
            Assert.Equal($"Index is out-of-range: {index}", ex.Message);
        }

        [Fact]
        public void TestIndexOutOfBounds_IndexIsLessThanZero()
        {
            var dynamicArray = new DynamicArray<int>();

            for (int i = 0; i < 1000; i++)
            {
                dynamicArray.add(789);
            }

            int index = -1;
            var ex = Assert.Throws<IndexOutOfRangeException>(() => dynamicArray.removeAt(index));
            Assert.Equal($"Index is out-of-range: {index}", ex.Message);
        }

        [Fact]
        public void TestIndexOutOfBounds_IndexIsLargeNegativeNumber()
        {
            var dynamicArray = new DynamicArray<int>();

            for (int i = 0; i < 15; i++)
            {
                dynamicArray.add(123);
            }

            int index = -66;
            var ex = Assert.Throws<IndexOutOfRangeException>(() => dynamicArray.removeAt(index));
            Assert.Equal($"Index is out-of-range: {index}", ex.Message);
        }

        [Fact]
        public void TestRemoveOperation_ArrayWithNullElements()
        {
            var dynamicArray = new DynamicArray<String>();
            var strs = new string[] { "a", "b", "c", "d", "e", null, "g", "h" };

            for (int i = 0; i < strs.Length; i++)
            {
                dynamicArray.add(strs[i]);
            }

            bool ret = dynamicArray.remove("c");
            Assert.True(ret);

            ret = dynamicArray.remove("c");
            Assert.False(ret);

            ret = dynamicArray.remove("h");
            Assert.True(ret);

            ret = dynamicArray.remove(null);
            Assert.True(ret);

            ret = dynamicArray.remove("a");
            Assert.True(ret);

            ret = dynamicArray.remove("a");
            Assert.False(ret);

            ret = dynamicArray.remove("h");
            Assert.False(ret);

            ret = dynamicArray.remove(null);
            Assert.False(ret);
        }

        [Fact]
        public void TestRemoveOperation_RemoveElementsTwice()
        {
            var dynamicArray = new DynamicArray<String>();
            var strs = new string[] { "a", "b", "c", "d" };
            foreach (var str in strs)
            {
                dynamicArray.add(str);
            }

            Assert.True(dynamicArray.remove("a"));
            Assert.True(dynamicArray.remove("b"));
            Assert.True(dynamicArray.remove("c"));
            Assert.True(dynamicArray.remove("d"));

            Assert.False(dynamicArray.remove("a"));
            Assert.False(dynamicArray.remove("b"));
            Assert.False(dynamicArray.remove("c"));
            Assert.False(dynamicArray.remove("d"));
        }

        [Fact]
        public void TestIndexOfNullElement()
        {
            var dynamicArray = new DynamicArray<String>();
            var strs = new string[] { "a", "b", null, "d" };

            for (int i = 0; i < strs.Length; i++)
            {
                dynamicArray.add(strs[i]);
            }

            Assert.Equal(2, dynamicArray.indexOf(null));
        }

        [Fact]
        public void TestAddElementsOperation()
        {
            var dynamicArray = new DynamicArray<int>();

            var elems = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            foreach (var elem in elems)
            {
                dynamicArray.add(elem);
            }

            for (int i = 0; i < elems.Length; i++)
            {
                Assert.Equal(elems[i], dynamicArray.get(i));
            }
        }

        [Fact]
        public void TestAddAndRemoveOperation()
        {
            var dynamicArray = new DynamicArray<long>();

            for (int i = 0; i < 55; i++) dynamicArray.add(44L);
            for (int i = 0; i < 55; i++) dynamicArray.remove(44L);
            Assert.True(dynamicArray.isEmpty());

            for (int i = 0; i < 55; i++) dynamicArray.add(44L);
            for (int i = 0; i < 55; i++) dynamicArray.removeAt(0);
            Assert.True(dynamicArray.isEmpty());

            for (int i = 0; i < 155; i++) dynamicArray.add(44L);
            for (int i = 0; i < 155; i++) dynamicArray.remove(44L);
            Assert.True(dynamicArray.isEmpty());

            for (int i = 0; i < 155; i++) dynamicArray.add(44L);
            for (int i = 0; i < 155; i++) dynamicArray.removeAt(0);
            Assert.True(dynamicArray.isEmpty());
        }

        [Fact]
        public void TestAddSetAndRemove()
        {
            var dynamicArray = new DynamicArray<long>();

            for (int i = 0; i < 55; i++) dynamicArray.add(44L);
            for (int i = 0; i < 55; i++) dynamicArray.set(i, 33L);
            for (int i = 0; i < 55; i++) dynamicArray.remove(33L);
            Assert.True(dynamicArray.isEmpty());

            for (int i = 0; i < 55; i++) dynamicArray.add(44L);
            for (int i = 0; i < 55; i++) dynamicArray.set(i, 33L);
            for (int i = 0; i < 55; i++) dynamicArray.removeAt(0);
            Assert.True(dynamicArray.isEmpty());

            for (int i = 0; i < 155; i++) dynamicArray.add(44L);
            for (int i = 0; i < 155; i++) dynamicArray.set(i, 33L);
            for (int i = 0; i < 155; i++) dynamicArray.remove(33L);
            Assert.True(dynamicArray.isEmpty());

            for (int i = 0; i < 155; i++) dynamicArray.add(44L);
            for (int i = 0; i < 155; i++) dynamicArray.removeAt(0);
            Assert.True(dynamicArray.isEmpty());
        }

        [Fact]
        public void TestSize()
        {
            var dynamicArray = new DynamicArray<int?>();
            var elems = new int?[] { -76, 45, 66, 3, null, 54, 33 };
            for (int i = 0, sz = 1; i < elems.Length; i++, sz++)
            {
                dynamicArray.add(elems[i]);
                Assert.Equal(dynamicArray.size(), sz);
            }
        }

        [Fact]
        public void TestIEnumerableImplementation()
        {
            var dynamicArray = new DynamicArray<string>();
            var strs = new string[] { "a", "b", "c", "d", "e", null, "g", "h" };
            for (int i = 0; i < strs.Length; i++)
            {
                dynamicArray.add(strs[i]);
            }

            int ind = 0;
            foreach (var elem in dynamicArray)
            {
                Assert.Equal(strs[ind++], elem);
            }
        }

        [Fact]
        public void TestIEnumerableImplementation_EmptyArray()
        {
            var dynamicArray = new DynamicArray<String>();

            int ind = 0;
            foreach (var elem in dynamicArray)
            {
                ind++;
            }

            Assert.Equal(0, ind);
        }

    }
}
