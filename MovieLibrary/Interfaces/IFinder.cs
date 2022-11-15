using System.Collections.Generic;

namespace MovieLibrary.Interfaces
{
    internal interface IFinder<T>
    {

        List<T> Find(string name);

    }
}
