using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieLibrary.utility;
using MovieLibraryEntities.Context;
using System;
using System.Linq;

namespace MovieLibrary.menus.MovieMenus
{
    internal class SearchMenu : Menu
    {

        public SearchMenu(ILogger<IMenu> _logger) : base(_logger)
        {

        }

        public override void Start()
        {
            base.Start();

            string title = InputUtility.GetStringWithPrompt("What is title of the movie? (blank for all) ");

            using (var movieContext = new MovieContext())
            {
                var sorted = movieContext.Movies
                    .Where(x => x.Title.StartsWith(title))
                    .Include("MovieGenres.Genre");

                Console.WriteLine(sorted.Count() + " Movie(s) returned!");
                Console.WriteLine();

                foreach (var movie in sorted)
                {
                    Console.WriteLine(movie.Title);
                    string genresStr = "  Genres => ";
                    foreach (var genre in movie.MovieGenres)
                    {
                        if (genre != movie.MovieGenres.First())
                            genresStr += " | ";
                        genresStr += genre.Genre.Name;
                    }
                    if (movie.MovieGenres.Count() > 0)
                        Console.WriteLine(genresStr);
                }
            }

            Console.WriteLine();
            WaitForInput();
        }
    }
}
