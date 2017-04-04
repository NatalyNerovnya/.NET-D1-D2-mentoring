using System.Collections;
using System.Collections.Generic;

namespace GenericCollection
{

    class QueueIterator<T> : IEnumerator<T>
    {

        private T[] array;
        private int position;
        private readonly int tailPosition, headPosition;

        public QueueIterator(T[] list, int tail, int head)
        {
            array = list;
            position = (head - 1) % list.Length;
            tailPosition = tail;
            headPosition = head;
        }

        public bool MoveNext()
        {
            if (position == tailPosition)
            {
                Reset();
                return false;
            }
            position = ++position % array.Length;
            return true;
        }

        public void Reset()
        {
            position = (headPosition - 1) % array.Length;
        }

        object IEnumerator.Current
        {
            get { return array[position]; }
        }

        public void Dispose()
        {}

        public T Current
        {
            get { return array[position]; }
        }
    }
}