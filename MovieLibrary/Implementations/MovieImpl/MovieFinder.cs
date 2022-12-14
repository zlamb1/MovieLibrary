using Microsoft.EntityFrameworkCore;
using MovieLibrary.Interfaces;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;
using System.Collections.Generic;
using System.Linq;

namespace MovieLibrary.Implementations.MovieImpl
{
    internal class MovieFinder : IFinder<Movie>
    {
        public Movie First(string name)
        {
            using (var ctx = new MovieContext())
            {
                return ctx.Movies
                    .Include("MovieGenres.Genre")
                    .FirstOrDefault(x => x.Title.StartsWith(name));
            }
        }

        public List<Movie> Find(string name)
        {
            using (var ctx = new MovieContext())
            {
                return ctx.Movies
                    .Where(x => x.Title.StartsWith(name))
                    .Include("MovieGenres.Genre")
                    .ToList();
            }
        }
    }
}
