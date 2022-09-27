using System;
using System.Collections.Generic;

namespace MovieLibrary.models
{
    internal class Show : Media
    {
        public int Seasons { get; set; }
        public int Episodes { get; set; }
        public List<string> Writers { get; set; }
        public Show(string input)
        {
            string[] parts = ParseLine(input);
            Id = int.Parse(parts?[0]);
            Title = parts?[1];
            Seasons = int.Parse(parts?[2]);
            Episodes = int.Parse(parts?[3]);
            Writers = new List<string>();
            foreach (string writer in parts[4]?.Split('|'))
                Writers.Add(writer);
        }
        public override void Display()
        {
            string display = Id + "," + Title + "," + Seasons + "," + Episodes + ",";
            foreach (string writer in Writers)
                display += writer + "|";
            display = display.Substring(0, display.Length - 1);
            Console.WriteLine(display);
        }
    }
}
