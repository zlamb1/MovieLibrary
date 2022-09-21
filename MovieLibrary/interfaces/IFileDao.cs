using System.Collections.Generic;

namespace MovieLibrary.interfaces
{
    internal interface IFileDao
    {
        // Input & Result are designated as "object" lists to be as broad as possible for
        // potential implementations.
        public List<object> Input { get; set; }
        public List<object> Result { get; set; }
        public bool ExceptionOccured { get; set; }
        public bool IgnoreFirstLine { get; set; }
        public string File { get; set; }
        public void Read();
        public void Write();
        public void Reset();
    }
}
