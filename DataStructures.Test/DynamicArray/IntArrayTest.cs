using DataStructures.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DataStructures.Test.DynamicArray
{
    public class IntArrayTest
    {
        [Fact]
        public void TestConstructor_InitializedBy_NullArray()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new IntArray(null));
            Assert.Equal("Value cannot be null. (Parameter 'Array cannot be null')", ex.Message);
        }

        [Fact]
        public void TestConstructor_InitializedBy_NonEmptyArray()
        {
            var input = new int[] { 1, 2, 3, 4 };

            var intArray = new IntArray(input);
            for(int i = 0; i < input.Length; i++)
            {
                Assert.Equal(input[i], intArray.arr[i]);
            }
        }

        [Fact]
        public void TestEmptyList()
        {
            var intArray = new IntArray();

            Assert.True(intArray.isEmpty());
        }

        [Fact]
        public void TestRemovingIfEmpty()
        {
            var intArray = new IntArray();

            int index = 0;
            var ex = Assert.Throws<IndexOutOfRangeException>(() => intArray.removeAt(index));
            Assert.Equal($"Index is out-of-range: {index}", ex.Message);
        }

        [Fact]
        public void TestIndexOutOfBounds_IndexIsGreaterThanLength()
        {
            var intArray = new IntArray();

            intArray.add(-56);
            intArray.add(-53);
            intArray.add(-55);

            int index = 3;
            var ex = Assert.Throws<IndexOutOfRangeException>(() => intArray.removeAt(index));
            Assert.Equal($"Index is out-of-range: {index}", ex.Message);
        }

        [Fact]
        public void TestIndexOutOfBounds_LargeIntArray_IndexIsGreaterThanLength()
        {
            var intArray = new IntArray();

            for (int i = 0; i < 1000; i++)
            {
                intArray.add(789);
            }

            int index = 1000;
            var ex = Assert.Throws<IndexOutOfRangeException>(() => intArray.removeAt(index));
            Assert.Equal($"Index is out-of-range: {index}", ex.Message);
        }

        [Fact]
        public void TestIndexOutOfBounds_IndexIsLessThanZero()
        {
            var intArray = new IntArray();

            for (int i = 0; i < 1000; i++)
            {
                intArray.add(789);
            }

            int index = -1;
            var ex = Assert.Throws<IndexOutOfRangeException>(() => intArray.removeAt(index));
            Assert.Equal($"Index is out-of-range: {index}", ex.Message);
        }

        [Fact]
        public void TestIndexOutOfBounds_IndexIsLargeNegativeNumber()
        {
            var intArray = new IntArray();

            for (int i = 0; i < 15; i++)
            {
                intArray.add(123);
            }

            int index = -66;
            var ex = Assert.Throws<IndexOutOfRangeException>(() => intArray.removeAt(index));
            Assert.Equal($"Index is out-of-range: {index}", ex.Message);
        }

        [Fact]
        public void TestRemoveOperation()
        {
            var intArray = new IntArray();
            var elems = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };

            for (int i = 0; i < elems.Length; i++)
            {
                intArray.add(elems[i]);
            }

            bool ret = intArray.remove(1);
            Assert.True(ret);

            ret = intArray.remove(1);
            Assert.False(ret);

            ret = intArray.remove(2);
            Assert.True(ret);

            ret = intArray.remove(3);
            Assert.True(ret);

            ret = intArray.remove(4);
            Assert.True(ret);

            ret = intArray.remove(4);
            Assert.False(ret);

            ret = intArray.remove(3);
            Assert.False(ret);
        }

        [Fact]
        public void TestRemoveOperation_RemoveElementsTwice()
        {
            var intArray = new IntArray();
            var elems = new int[] { 1, 2, 3, 4};
            foreach (var elem in elems)
            {
                intArray.add(elem);
            }

            Assert.True(intArray.remove(1));
            Assert.True(intArray.remove(2));
            Assert.True(intArray.remove(3));
            Assert.True(intArray.remove(4));

            Assert.False(intArray.remove(1));
            Assert.False(intArray.remove(2));
            Assert.False(intArray.remove(3));
            Assert.False(intArray.remove(4));

            Assert.True(intArray.isEmpty());
        }

        [Fact]
        public void TestAddElementsOperation()
        {
            var intArray = new IntArray();

            var elems = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            foreach (var elem in elems)
            {
                intArray.add(elem);
            }

            for (int i = 0; i < elems.Length; i++)
            {
                Assert.Equal(elems[i], intArray.get(i));
            }
        }

        [Fact]
        public void TestAddAndRemoveOperation()
        {
            var intArray = new IntArray();

            for (int i = 0; i < 55; i++) intArray.add(44);
            for (int i = 0; i < 55; i++) intArray.remove(44);
            Assert.True(intArray.isEmpty());

            for (int i = 0; i < 55; i++) intArray.add(44);
            for (int i = 0; i < 55; i++) intArray.removeAt(0);
            Assert.True(intArray.isEmpty());

            for (int i = 0; i < 155; i++) intArray.add(44);
            for (int i = 0; i < 155; i++) intArray.remove(44);
            Assert.True(intArray.isEmpty());

            for (int i = 0; i < 155; i++) intArray.add(44);
            for (int i = 0; i < 155; i++) intArray.removeAt(0);
            Assert.True(intArray.isEmpty());
        }

        [Fact]
        public void TestAddSetAndRemove()
        {
            var intArray = new IntArray();

            for (int i = 0; i < 55; i++) intArray.add(44);
            for (int i = 0; i < 55; i++) intArray.set(i, 33);
            for (int i = 0; i < 55; i++) intArray.remove(33);
            Assert.True(intArray.isEmpty());

            for (int i = 0; i < 55; i++) intArray.add(44);
            for (int i = 0; i < 55; i++) intArray.set(i, 33);
            for (int i = 0; i < 55; i++) intArray.removeAt(0);
            Assert.True(intArray.isEmpty());

            for (int i = 0; i < 155; i++) intArray.add(44);
            for (int i = 0; i < 155; i++) intArray.set(i, 33);
            for (int i = 0; i < 155; i++) intArray.remove(33);
            Assert.True(intArray.isEmpty());

            for (int i = 0; i < 155; i++) intArray.add(44);
            for (int i = 0; i < 155; i++) intArray.removeAt(0);
            Assert.True(intArray.isEmpty());
        }

        [Fact]
        public void TestSize()
        {
            var intArray = new IntArray();
            var elems = new int[] { -76, 45, 66, 3, 54, 33 };
            for (int i = 0, sz = 1; i < elems.Length; i++, sz++)
            {
                intArray.add(elems[i]);
                Assert.Equal(intArray.size(), sz);
            }
        }

        [Fact]
        public void TestIEnumerableImplementation()
        {
            var intArray = new IntArray();
            var ints = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            for (int i = 0; i < ints.Length; i++)
            {
                intArray.add(ints[i]);
            }

            int ind = 0;
            foreach (var elem in intArray)
            {
                Assert.Equal(ints[ind++], elem);
            }
        }

        [Fact]
        public void TestIEnumerableImplementation_EmptyArray()
        {
            var intArray = new IntArray();

            int ind = 0;
            foreach (var elem in intArray)
            {
                ind++;
            }

            Assert.Equal(0, ind);
        }

    }
}
