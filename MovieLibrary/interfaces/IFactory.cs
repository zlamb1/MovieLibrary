using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.interfaces
{
    internal interface IFactory<T>
    {
        public T Create(params object[] args);
    }
}
