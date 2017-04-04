using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomIntStack
{
    public class CustomStack : IEnumerable<int>, IEquatable<CustomStack>
    {
        private int size;
        private int capacity;
        private static readonly int defaultCapacity = 8;
        private int[] stack;

        public int Count
        {
            get
            {
                return size;
            }
        }

        public CustomStack() : this(defaultCapacity) { }

        public CustomStack(int initialCapacity)
        {
            capacity = initialCapacity;
            size = 0;
            stack = new int[capacity];
        }

        public CustomStack(params int[] collection)
            : this(collection.Length)
        {
            stack = collection;
            size = collection.Length;
        }

        public void Push(int element)
        {
            if (size + 1 > capacity)
                ExtendCapacity();
            stack[size++] = element;
        }

        public int Pop()
        {
            if (Count == 0)
                throw new InvalidOperationException();
            var result = stack[size - 1];
            stack[--size] = 0;
            return result;
        }

        public int Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException();
            return stack[size - 1];

        }

        public bool Contains(int element)
        {
            for (int i = 0; i < size; i++)
            {
                if (stack[i] == element)
                    return true;
            }
            return false;
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = size - 1; i >= 0; i--)
            {
                if (stack[i] < 0)
                    yield break;
                yield return stack[i];
            }
        }

        //very bad!! Don't use foreach loop because it will stop after first negative integer
        public bool Equals(CustomStack other)
        {
            if (other == null)
                return false;
            if (other.Count != this.Count)
                return false;

            int i = this.size - 1;
            foreach (var element in other)
            {
                if (i < 0)
                    break;
                if (element != this.stack[i--])
                    return false;
            }
            return true;
        }

        private void ExtendCapacity()
        {
            capacity *= 2;
            var newStack = new int[capacity];
            for (int i = 0; i < size; i++)
            {
                newStack[i] = stack[i];
            }

            stack = newStack;
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
