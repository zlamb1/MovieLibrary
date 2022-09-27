using System;
using System.Collections.Generic;

namespace MovieLibrary.models
{
    internal class Video : Media
    {
        public List<string> Formats { get; set; }
        public int Length { get; set; }
        public List<int> Regions { get; set; }
        public Video(string input)
        {
            string[] parts = ParseLine(input);
            Id = int.Parse(parts[0]);
            Title = parts[1];
            Formats = new List<string>();
            string[] fParts = parts[2].Split('|');
            foreach (string format in fParts)
                Formats.Add(format);
            Length = int.Parse(parts[3]);
            Regions = new List<int>();
            string[] rParts = parts[4].Split('|');
            foreach (string region in rParts)
                Regions.Add(int.Parse(region));
        }
        public override void Display()
        {
            string display = Id + "," + Title + ",";
            foreach (string format in Formats)
                display += format + "|";
            display = display.Substring(0, display.Length - 1) + "," + Length + ",";
            foreach (int region in Regions)
                display += region + "|";
            display = display.Substring(0, display.Length - 1);
            Console.WriteLine(display);
        }
    }
}
