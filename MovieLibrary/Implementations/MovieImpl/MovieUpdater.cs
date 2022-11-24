using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieLibrary.Interfaces;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;
using System;
using System.Linq;

namespace MovieLibrary.Implementations.MovieImpl
{
    internal class MovieUpdater : IUpdater<Movie>
    {

        private ILogger<IUpdater<Movie>> logger;

        public MovieUpdater(ILogger<IUpdater<Movie>> _logger)
        {
            logger = _logger;
        }

        public void Update(Movie movie, int fieldNum, string val)
        {
            switch (fieldNum)
            {
                case 1:
                    UpdateTitle(movie, val);
                    break;
                case 2:
                    UpdateGenres(movie, val);
                    break;
                case 3:
                    UpdateReleaseDate(movie, val);
                    break;
                default:
                    throw new InvalidOperationException("Invalid field number!");
            }
        }
        private void UpdateTitle(Movie movie, string val)
        {
            if (string.IsNullOrEmpty(val))
            {
                throw new InvalidOperationException("The movie title cannot be null!");
            }

            using (var ctx = new MovieContext())
            {
                var aMovie = ctx.Movies
                    .FirstOrDefault(x => x.Id == movie.Id);

                if (aMovie is null)
                {
                    throw new InvalidOperationException($"Could not find the movie => {val}!");
                }

                logger.LogInformation($"Changing Title {aMovie.Title} => {val}");
                aMovie.Title = val;

                ctx.SaveChanges();
            }

            movie.Title = val;
        }
        private void UpdateGenres(Movie movie, string val)
        {
            if (string.IsNullOrEmpty(val))
            {
                throw new InvalidOperationException("The movie genres cannot be null!");
            }
            string[] strGenres = val.Replace(" ", "").Split("|");
            using (var ctx = new MovieContext())
            {
                var aMovie = ctx.Movies
                    .Include("MovieGenres.Genre")
                    .FirstOrDefault(x => x.Id == movie.Id);
                if (aMovie is null)
                {
                    throw new InvalidOperationException($"Could not find the movie => {val}!");
                }

                foreach (var movieGenre in aMovie.MovieGenres)
                {
                    logger.LogInformation(
                        $"Removing Genre => {movieGenre.Genre.Name}");
                    ctx.MovieGenres.Remove(movieGenre);
                }

                foreach (string genre in strGenres)
                {
                    Genre found = ctx.Genres
                        .FirstOrDefault(x => x.Name.Equals(genre));
                    if (found is null)
                    {
                        throw new InvalidOperationException(
                            $"The movie genre {genre} is not a valid genre!");
                    }

                    MovieGenre movieGenre = new MovieGenre();
                    movieGenre.Movie = aMovie;
                    movieGenre.Genre = found;

                    logger.LogInformation(
                        $"Adding Genre => {found.Name}");
                    ctx.MovieGenres.Add(movieGenre);
                }

                ctx.SaveChanges();
            }
        }
        private void UpdateReleaseDate(Movie movie, string val)
        {
            var date = DateTime.Now;
            if (!string.IsNullOrEmpty(val))
            {
                date = DateTime.Parse(val);
            }

            using (var ctx = new MovieContext())
            {
                var aMovie = ctx.Movies
                    .FirstOrDefault(x => x.Id == movie.Id);
                if (aMovie is null)
                {
                    throw new InvalidOperationException($"Could not find the movie => {val}!");
                }

                logger.LogInformation($"Changing Release Date {aMovie.ReleaseDate} => {date}");
                aMovie.ReleaseDate = date;

                ctx.SaveChanges();
            }
        }
    }
}
