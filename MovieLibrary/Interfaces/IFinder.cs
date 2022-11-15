using System.Collections.Generic;

namespace MovieLibrary.Interfaces
{
    internal interface IFinder<T>
    {

        T First(string name);
        List<T> Find(string name);

    }
}
