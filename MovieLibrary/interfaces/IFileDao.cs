using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.interfaces
{
    internal interface IFileDao
    {
        public bool IgnoreFirstLine { get; set; }
        public string File { get; set; }
        public object[] Read();
    }
}
