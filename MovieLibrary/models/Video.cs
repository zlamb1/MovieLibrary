using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.models
{
    internal class Video : Media
    {
        public string Format { get; set; }
        public int Length { get; set; }
        public int[] Regions { get; set; }
        public override string Display()
        {
            throw new System.NotImplementedException();
        }
    }
}
