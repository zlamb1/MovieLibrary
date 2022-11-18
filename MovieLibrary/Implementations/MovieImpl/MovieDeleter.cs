using Microsoft.Extensions.Logging;
using MovieLibrary.Interfaces;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;
using System;
using System.Linq;

namespace MovieLibrary.Implementations.MovieImpl
{
    internal class MovieDeleter : IDeleter<Movie>
    {

        private readonly ILogger<IDeleter<Movie>> logger;
        public MovieDeleter(ILogger<IDeleter<Movie>> _logger)
        {
            logger = _logger;
        }

        public void Delete(string name)
        {
            using (var ctx = new MovieContext())
            {
                var aMovie = ctx.Movies.FirstOrDefault(x => x.Title.Equals(name));

                if (aMovie is null)
                {
                    logger.LogWarning("Could not find a movie with the title => " + name);
                    throw new ArgumentException(
                        "Could not find a movie with the title => " + name);
                }
                else
                {
                    logger.LogInformation("Deleting Movie => " + name);
                    ctx.Movies.Remove(aMovie);
                }

                ctx.SaveChanges();
            }
        }
    }
}
