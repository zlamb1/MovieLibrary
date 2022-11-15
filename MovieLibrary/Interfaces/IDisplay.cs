using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Interfaces
{
    internal interface IDisplay<T>
    {
        public void Display(T obj);
    }
}
