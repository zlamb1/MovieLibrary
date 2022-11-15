using Microsoft.Extensions.Logging;
using MovieLibrary.Interfaces;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;
using System;
using System.Linq;

namespace MovieLibrary.Implementations
{
    internal class MovieDeleter : IDeleter<Movie>
    {

        private readonly ILogger<IDeleter<Movie>> logger;
        public MovieDeleter(ILogger<IDeleter<Movie>> _logger) {
            logger = _logger;
        }

        public void Delete(string name)
        {
            using (var ctx = new MovieContext())
            {
                var movie = ctx.Movies.FirstOrDefault(x => x.Title.Equals(name));

                if (movie is null)
                {
                    throw new ArgumentException(
                        "Could not find a movie with the title => " + name);
                } else
                {
                    logger.LogInformation("Deleting movie => " + name);
                    ctx.Movies.Remove(movie);
                }

                ctx.SaveChanges();
            }
        }
    }
}
