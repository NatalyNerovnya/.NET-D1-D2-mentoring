using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GenericCollection
{
    public class CustomQueue<T> : IEnumerable<T> where T: IComparable<T>
    {
        private T[] queue;
        private int capacity, head, tail;
        private static readonly int defaultCapacity = 8;


        public CustomQueue() : this(defaultCapacity) { }

        public CustomQueue(int n)
        {
            if (n > 0)
            {
                queue = new T[n];
                capacity = n;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
            head = 0;
            tail = 0;
        }

        public CustomQueue(IEnumerable<T> arr): this(arr.Count())
        {
            Array.Copy(arr.ToArray(), queue, capacity);
            tail = capacity - 1;
        }


        public void Enqueue(T element)
        {
            if ((tail + 1) % capacity == head % capacity)
            {
                Extend();
                queue[capacity] = element;
                tail = capacity;
                head = 0;
                capacity *= 2;
            }
            else
            {
                queue[++tail % capacity] = element;
                tail = tail % capacity;
            }

        }

        public T Dequeue()
        {
            if (IsEmpty())
                throw new ArgumentException();

            T returnValue = Peek();
            DeleteHead();
            return returnValue;
        }

        public T Peek()
        {
            if (IsEmpty())
                throw new ArgumentException();
            return queue[head];
        }

        public void Clear()
        {
            for (int i = head, j = 0; j < capacity; j++)
            {
                i = ++i % capacity;
            }
            head = tail = 0;
        }

        public bool Contain(T str)
        {
            foreach (var variable in queue)
            {
                if (variable.Equals(str))
                    return true;
            }
            return false;
        }

        public bool EqualToObject(object obj)
        {
            if (!(obj is T)) return false;

            return queue.Any(element => element.CompareTo((T) obj) == 0);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new QueueIterator<T>(queue, tail, head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        private void Extend()
        {
            T[] wideQueue = new T[capacity * 2];

            for (int i = head, j = 0; j < capacity; j++)
            {
                wideQueue[j] = queue[i];
                i = ++i % capacity;
            }
            queue = wideQueue;
        }

        private void DeleteHead()
        {
            head = ++head % capacity;
        }

        private bool IsEmpty()
        {
            return tail % capacity == head % capacity;
        }
    }
}