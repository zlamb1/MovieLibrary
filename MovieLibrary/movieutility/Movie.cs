using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary
{
    internal class Movie
    {
        public int MovieID { get; set; }
        public string MovieIDString { get { return MovieID.ToString(); } }
        public string Title { get; set; }
        public List<string> Genres { get; set; }
        public Movie(int _MovieID, string _Title, string _Genres)
        {
            MovieID = _MovieID;
            Title = _Title;
            Genres = new List<string>();
            string[] gParts = _Genres.Split('|');
            foreach (string genre in gParts) 
                Genres.Add(genre);
        }

        public override string ToString()
        {
            string str = MovieID + "," + Title + ( Genres.Count > 0 ? "," : "" );
            foreach (string genre in Genres)
                str += genre + "|";
            return str.Remove(str.Length-1);
        }
    }
}
