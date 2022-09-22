using System.Collections.Generic;

namespace MovieLibrary.models
{
    internal class Show : Media
    {
        public int Seasons { get; set; }
        public int Episodes { get; set; }
        public List<string> Writers { get; set; }
    }
}
