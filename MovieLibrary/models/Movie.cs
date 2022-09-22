using MovieLibrary.models;
using System.Collections.Generic;

namespace MovieLibrary
{
    internal class Movie : Media
    {
        public string IdString { get { return Id.ToString(); } }
        public List<string> Genres { get; set; }
        public Movie(int _Id, string _Title, string _Genres)
        {
            Id = _Id;
            Title = _Title;
            Genres = new List<string>();
            // Do a null check to ensure that .Split is not being called on a null object.
            if (_Genres != null)
            {
                string[] gParts = _Genres.Split('|');
                foreach (string genre in gParts)
                    Genres.Add(genre);
            }
        }

        public override string ToString()
        {
            string str = Id + "," + Title + ( Genres.Count > 0 ? "," : "" );
            foreach (string genre in Genres)
                str += genre + "|";
            return str.Remove(str.Length-1);
        }
    }
}
