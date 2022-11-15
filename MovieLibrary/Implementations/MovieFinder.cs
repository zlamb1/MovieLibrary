using Microsoft.EntityFrameworkCore;
using MovieLibrary.Interfaces;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;
using System.Collections.Generic;
using System.Linq;

namespace MovieLibrary.Implementations
{
    internal class MovieFinder : IFinder<Movie>
    {

        public List<Movie> Find(string name)
        {
            List<Movie> movies = null;
            using (var ctx = new MovieContext())
            {
                movies = ctx.Movies
                    .Where(x => x.Title.StartsWith(name))
                    .Include("MovieGenres.Genre")
                    .Include("UserMovies.User").ToList();
            }
            return movies;
        }
    }
}
