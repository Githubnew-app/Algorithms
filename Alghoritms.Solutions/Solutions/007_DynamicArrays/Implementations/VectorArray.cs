using System;

namespace Alghoritms.Solutions.Solutions
{
    public class VectorArray<T> : ISimpleList<T>
    {
        private T[] data;
        private readonly int vector;

        public int Vector => vector;

        public VectorArray(int vector = 100)
        {
            this.vector = vector;
            data = new T[this.vector];
            Count = 0;
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public T this[int index]
        {
            get
            {
                if (index >= Count)
                    throw new IndexOutOfRangeException();
                return data[index];
            }

            set
            {
                if (index >= Count)
                    throw new IndexOutOfRangeException();
                data[index] = value;
            }
        }

        private void EnshureCapacity(int maxExpectedIndex)
        {
            if (maxExpectedIndex >= data.Length)
            {
                T[] newData = new T[data.Length + vector];
                Array.Copy(data, newData, data.Length);
                data = newData;
            }
        }

        public void Add(T item)
        {
            EnshureCapacity(Count + 1);
            data[Count] = item;
            Count++;
        }

        public void Insert(int index, T item)
        {
            EnshureCapacity(Count + 1);
            Array.Copy(data, index, data, index + 1, Count - index);
            data[index] = item;
            Count++;
        }

        public void RemoveAt(int index)
        {
            Array.Copy(data, index + 1, data, index, Count - index - 1);
            Count--;
        }

        public void Clear()
        {
            data = new T[0];
            Count = 0;
        }

        //public bool Contains(T item)
        //{
        //    throw new NotImplementedException();
        //}
        //
        //public void CopyTo(T[] array, int arrayIndex)
        //{
        //    throw new NotImplementedException();
        //}
        //
        //public IEnumerator<T> GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}
        //
        //public int IndexOf(T item)
        //{
        //    throw new NotImplementedException();
        //}
        //
        //public bool Remove(T item)
        //{
        //    throw new NotImplementedException();
        //}
        //
        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
