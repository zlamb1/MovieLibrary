using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary
{

    // not really necessary but I felt like creating an interface for my menus

    internal interface IMenu
    {
        public int GetResults();
        public void Start();
    }
}
