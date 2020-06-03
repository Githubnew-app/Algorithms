using System;

namespace Alghoritms.Solutions.Solutions
{
    public class SingleArray<T> : ISimpleList<T>
    {
        private T[] data;
        public SingleArray()
        {
            data = new T[0];
        }

        public int Count => data.Length;

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

        public void Add(T item)
        {
            int currentLength = data.Length;
            T[] newData = new T[data.Length + 1];
            Array.Copy(data, newData, data.Length);
            data = newData;
            data[currentLength] = item;
        }
        public void Insert(int index, T item)
        {
            T[] newData = new T[data.Length + 1];
            Array.Copy(data, 0, newData, 0, index);
            newData[index] = item;
            Array.Copy(data, index, newData, index + 1, data.Length - index);
            data = newData;
        }

        public void RemoveAt(int index)
        {
            T[] newData = new T[data.Length - 1];
            Array.Copy(data, 0, newData, 0, index);
            Array.Copy(data, index + 1, newData, index, data.Length - index - 1);
            data = newData;
        }

        public void Clear()
        {
            data = new T[0];
        }
    }
}
