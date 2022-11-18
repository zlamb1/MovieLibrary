using MovieLibrary.Interfaces;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;
using System;
using System.Linq;

namespace MovieLibrary.Implementations.MovieImpl
{
    internal class MovieDisplay : IDisplay<Movie>
    {
        private static string TAB = "  ";

        public bool DisplayGenres { get; set; }

        public bool DisplayReviews { get; set; }

        public MovieDisplay()
        {
            DisplayGenres = true;
            DisplayReviews = false;
        }

        public void Display(Movie movie)
        {
            Console.WriteLine($"{movie.Title} =>");

            Console.WriteLine($"{TAB}Id: => {movie.Id}");

            var movieGenres = movie.MovieGenres;
            if (movie.MovieGenres is null)
            {
                // load genre information
                using (var ctx = new MovieContext())
                {
                    movieGenres = ctx.MovieGenres
                        .Where(x => x.Movie.Id == movie.Id)
                        .ToList();
                }
            }

            string genresStr = $"{TAB}Genres => ";
            foreach (var genre in movieGenres)
            {
                if (genre != movie.MovieGenres.First())
                    genresStr += " | ";
                genresStr += genre.Genre.Name;
            }

            if (DisplayGenres && movie.MovieGenres.Count() > 0)
                Console.WriteLine(genresStr);

            Console.WriteLine($"{TAB}Release Date => {movie.ReleaseDate}");

            if (DisplayReviews && movie.UserMovies.Count() > 0)
            {

                if (movie.UserMovies is null)
                {
                    throw new ArgumentException(
                        $"Cannot access user ratings while displaying: " +
                        $"{movie.Title}");
                }

                Console.WriteLine($"{TAB}Reviews => ");
                foreach (var userMovie in movie.UserMovies)
                {
                    Console.WriteLine(
                        $"{TAB}{TAB}Review ID => {userMovie.Id}");
                    Console.WriteLine(
                        $"{TAB}{TAB}{TAB}User ID => {userMovie.User.Id}");
                    Console.WriteLine(
                        $"{TAB}{TAB}{TAB}Rating => {userMovie.Rating}");
                    Console.WriteLine(
                        $"{TAB}{TAB}{TAB}Rated At => {userMovie.RatedAt}");
                }
            }
        }
    }
}
