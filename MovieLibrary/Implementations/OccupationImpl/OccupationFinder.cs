using MovieLibrary.Interfaces;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieLibrary.Implementations.OccupationImpl
{
    internal class OccupationFinder : IFinder<Occupation>
    {
        public Occupation First(string id)
        {
            using (var ctx = new MovieContext())
            {
                return ctx.Occupations.FirstOrDefault(x => x.Name.StartsWith(id));
            }
        }
        public List<Occupation> Find(string id)
        {
            using (var ctx = new MovieContext())
            {
                return ctx.Occupations.Where(x => x.Name.StartsWith(id)).ToList();
            }
        }
    }
}
