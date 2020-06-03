using System.Collections.Generic;

namespace Alghoritms.Solutions.Solutions
{
    public class ListWrapper<T> : List<T>, ISimpleList<T>
    {
        public ListWrapper() : base() { }
    }
}
