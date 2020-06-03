using System;

namespace Alghoritms.Solutions.Solutions
{
    public class MatrixArray<T> : ISimpleList<T>
    {
        private VectorArray<VectorArray<T>> data;
        private readonly int vector;
        private int capacity;

        public MatrixArray(int vector = 100)
        {
            this.vector = vector;
            data = new VectorArray<VectorArray<T>>();
            Count = 0;
            capacity = 0;
        }

        public int Count { get; private set; }

        private (int hi, int lo) Indexes(int index)
        {
            if (index == 0) return (0, 0);
            int position = 0;
            for (int i = 0; i < data.Count; i++)
            {
                int countOnTheLayer = data[i].Count;
                position += countOnTheLayer;
                if (position >= index)
                {
                    return (i, countOnTheLayer - (position - index));
                }
            }
            throw new IndexOutOfRangeException();
        }

        public T this[int index]
        {
            get
            {
                if (index >= Count)
                    throw new IndexOutOfRangeException();
                var (hi, lo) = Indexes(index);
                return data[hi][lo];
            }
            set
            {
                if (index >= Count)
                    throw new IndexOutOfRangeException();
                var (hi, lo) = Indexes(index);
                data[hi][lo] = value;
            }
        }

        private void EnshureCapacity(int maxExpectedIndex)
        {
            if (maxExpectedIndex >= capacity)
            {
                data.Add(new VectorArray<T>(vector));
                capacity += vector;
            }
        }

        public void Add(T item)
        {
            EnshureCapacity(Count + 1);
            var hi = Indexes(Count).hi;
            data[hi].Add(item);
            Count++;
        }

        public void Insert(int index, T item)
        {
            EnshureCapacity(Count + 1);
            var (hi, lo) = Indexes(index);
            data[hi].Insert(lo, item);
            Count++;
        }

        public void RemoveAt(int index)
        {
            var (hi, lo) = Indexes(index);
            data[hi].RemoveAt(lo);
            Count--;
        }

        public void Clear()
        {
            data = new VectorArray<VectorArray<T>>();
            Count = capacity = 0;
        }
    }
}
