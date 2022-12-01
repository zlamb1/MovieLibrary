using System.Collections.Generic;

namespace MovieLibrary.Interfaces
{
    internal interface IFinder<T>
    {

        T First(string id);
        List<T> Find(string id);

    }
}
