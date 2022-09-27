using System.Collections.Generic;
using System.Linq;

namespace MovieLibrary.models
{
    internal abstract class Media
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public abstract void Display();
        protected string[] ParseLine(string line)
        {
            List<string> parts = line.Split(",").ToList();
            if (line.Contains('"'))
            {
                int firstOccurence = line.IndexOf('"');
                string quotation = line.Substring(firstOccurence, line.LastIndexOf('"') - firstOccurence + 1);
                string removed = line.Replace("," + quotation, "");
                parts = removed.Split(',').ToList();
                parts.Insert(1, quotation.Replace("\"", ""));
            }
            return parts.ToArray();
        }
    }
}
