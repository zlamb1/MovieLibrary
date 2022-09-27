using MovieLibrary.models;
using System;
using System.Collections.Generic;

namespace MovieLibrary
{
    internal class Movie : Media
    {
        public List<string> Genres { get; set; }
        public Movie(string input)
        {
            string[] parts = ParseLine(input);
            Id = int.Parse(parts[0]);
            Title = parts[1];
            Genres = new List<string>();
            foreach (string genre in parts[2].Split('|'))
                Genres.Add(genre);
        }
        public override void Display()
        {
            string display = Id + "," + Title + ",";
            foreach (string genre in Genres)
                display += genre + "|";
            display = display.Substring(0, display.Length - 1);
            Console.WriteLine(display);
        }
    }
}
