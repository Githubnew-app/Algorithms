using System;
using System.Collections;
using System.Collections.Generic;

namespace Alghoritms.Solutions.Solutions._007_DynamicArrays
{
    public class SingleArray<T> : IList<T>
    {
        private T[] data;
        public SingleArray()
        {
            data = new T[0];
        }

        public int Count => data.Length;

        public bool IsReadOnly => false;

        public T this[int index] { get => data[index]; set => data[index] = value; }

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

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
