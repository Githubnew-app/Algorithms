using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Alghoritms.Solutions.Solutions._007_DynamicArrays
{
    public class FactorArray<T> : IList<T>
    {
        private T[] data;
        private float factor;
        private int length;
        public FactorArray(int defaultCatacity = 100, float factor = 2f)
        {
            this.factor = factor > 1 ? factor : 1f;
            data = new T[defaultCatacity];
            length = 0;
        }

        public int Count => length;

        public bool IsReadOnly => false;

        public T this[int index] { get => data[index]; set => data[index] = value; }

        private void EnshureCapacity(int maxExpectedIndex)
        {
            if (maxExpectedIndex >= data.Length)
            {
                T[] newData = new T[(int)(data.Length * factor)];
                Array.Copy(data, newData, data.Length);
                data = newData;
            }
        }

        public void Add(T item)
        {
            EnshureCapacity(length);
            data[length] = item;
            length++;
        }

        public void Insert(int index, T item)
        {
            EnshureCapacity(length);
            Array.Copy(data, index, data, index + 1, length - index);
            data[index] = item;
            length++;
        }

        public void RemoveAt(int index)
        {
            Array.Copy(data, index + 1, data, index, length - index - 1);
            length--;
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
