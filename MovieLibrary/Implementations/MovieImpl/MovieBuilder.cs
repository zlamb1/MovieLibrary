using MovieLibrary.Interfaces;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;
using System;
using System.Linq;

namespace MovieLibrary.Implementations.MovieImpl
{
    internal class MovieBuilder : IBuilder<Movie>
    {
        public Movie Build(params object[] args)
        {
            var movie = new Movie();

            if (args.Length < 3)
            {
                throw new ArgumentNullException("The movie builder expects three arguments!");
            }

            if (string.IsNullOrEmpty((string)args[0]))
            {
                throw new ArgumentNullException("The movie title cannot be null/blank!");
            }

            movie.Title = args[0].ToString();

            if (string.IsNullOrEmpty((string)args[2]))
                movie.ReleaseDate = DateTime.Now;
            else
                movie.ReleaseDate = DateTime.Parse(args[2].ToString());

            using (var ctx = new MovieContext())
            {
                ctx.Movies.Add(movie);
                ctx.SaveChanges();
            }

            // this code is used as well in the MovieUpdater
            // consider moving to a MovieGenreBuilder class?
            if (!string.IsNullOrEmpty((string)args[1]))
            {
                string[] genres = args[1]
                    .ToString()
                    .Replace(" ", "")
                    .Split("|");
                using (var ctx = new MovieContext())
                {
                    movie = ctx.Movies
                        .FirstOrDefault(x =>
                        x.Id == movie.Id);

                    foreach (string genre in genres)
                    {
                        var foundGenre = ctx.Genres
                            .FirstOrDefault(x => x.Name.StartsWith(genre));

                        if (foundGenre is null)
                        {
                            throw new ArgumentException(
                                $"Could not find the genre: {genre}");
                        }

                        MovieGenre movieGenre = new MovieGenre();
                        movieGenre.Movie = movie;
                        movieGenre.Genre = foundGenre;
                        ctx.MovieGenres.Add(movieGenre);
                    }

                    ctx.SaveChanges();
                }
            }

            return movie;
        }
    }
}
