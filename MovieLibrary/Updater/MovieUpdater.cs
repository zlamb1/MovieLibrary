using MovieLibrary.Interfaces;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;
using System;
using System.Linq;

namespace MovieLibrary.Updater
{
    internal class MovieUpdater : IUpdater<Movie>
    {
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
                foreach (var movieGenre in movie.MovieGenres)
                {
                    ctx.MovieGenres.Remove(movieGenre);
                }

                foreach (string genre in strGenres)
                {
                    Genre found = ctx.Genres
                        .FirstOrDefault(x => x.Name.Equals(genre));

                    if (found is null)
                        throw new InvalidOperationException("The movie genre " + genre + " is not a valid genre!");

                    MovieGenre movieGenre = new MovieGenre();
                    movieGenre.Movie = ctx.Movies
                        .FirstOrDefault(x => x.Id == movie.Id);
                    movieGenre.Genre = found;
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
            movie.ReleaseDate = date;
        }
    }
}
