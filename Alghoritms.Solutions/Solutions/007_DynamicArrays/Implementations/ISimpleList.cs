using System;
using System.Collections.Generic;
using System.Text;

namespace Alghoritms.Solutions.Solutions
{
    public interface ISimpleList<T>
    {
        public int Count { get; }

        public T this[int index] { get ; set ; }

        public void Add(T item);

        public void Insert(int index, T item);

        public void RemoveAt(int index);

        public void Clear();
    }
}
