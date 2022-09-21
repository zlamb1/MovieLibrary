using System.Collections.Generic;

namespace MovieLibrary.interfaces
{
    internal interface IFilter
    {
        public bool ExceptionOccured { get; set; }
        public List<object> Input { get; set; }
        public List<object> Output { get; }
        public void Filter(params string[] args);
    }
}
