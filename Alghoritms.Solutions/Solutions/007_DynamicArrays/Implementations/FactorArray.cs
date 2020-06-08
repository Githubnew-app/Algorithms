using System;

namespace Alghoritms.Solutions.Solutions
{
    public class FactorArray<T> : ISimpleList<T>
    {
        private T[] data;
        private readonly float factor;

        public FactorArray(int defaultCatacity = 100, float factor = 2f)
        {
            if (factor < 1f)
                throw new Exception("Factor should be grater than 1");
            this.factor = factor > 1 ? factor : 1f;
            data = new T[defaultCatacity];
            Count = 0;
        }

        public int Count { get; private set; }

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
                T[] newData = new T[(int)((data.Length > 0 ? data.Length : 1) * factor)];
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
            if (Count > 0)
            {
                Array.Copy(data, index, data, index + 1, Count - index);
            }
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
    }
}
