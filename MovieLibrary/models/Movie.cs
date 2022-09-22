using MovieLibrary.models;
using System.Collections.Generic;

namespace MovieLibrary
{
    internal class Movie : Media
    {
        public string IdString { get { return Id.ToString(); } }
        public List<string> Genres { get; set; }
        public override string Display()
        {
            throw new System.NotImplementedException();
        }
    }
}
