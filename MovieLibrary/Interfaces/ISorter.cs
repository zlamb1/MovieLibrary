using System.Collections.Generic;

namespace MovieLibrary.Interfaces
{
    internal interface ISorter<T>
    {
        List<object> Sort(params object[] args);
    }
}
