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

        private static string TAB = "  ";
        private static int PAGE_SIZE = 5;

        public SearchMenu(ILogger<IMenu> _logger) : base(_logger)
        {

        }

        public override void Start()
        {
            base.Start();

            string title = InputUtility.GetStringWithPrompt("What is title of the movie? (blank for all) ");
            bool showReviews = InputUtility.GetBoolWithPrompt("Do you want to see reviews? (Y/N)");

            using (var movieContext = new MovieContext())
            {
                // sort out movies that have a matching title then eager load their genres and reviews
                var sorted = movieContext.Movies
                    .Where(x => x.Title.StartsWith(title))
                    .Include("MovieGenres.Genre")
                    .Include("UserMovies.User").ToList();

                Console.Clear();
                Console.WriteLine(sorted.Count() + " Movie(s) returned!");
                Console.WriteLine();

                int index = 0;
                foreach (var movie in sorted)
                {
                    if (index % PAGE_SIZE == 0 && index >= PAGE_SIZE)
                    {
                        Console.WriteLine();
                        bool result = InputUtility.GetBoolWithPrompt("Would you like to continue to the next page? (Y/N)");
                        if (!result) break;
                        
                        Console.Clear();
                        int page = (index + 1) / PAGE_SIZE;
                        Console.WriteLine("Page " + page + "/" + 
                            Math.Ceiling((float)sorted.Count() / PAGE_SIZE));
                        Console.WriteLine();
                    }

                    Console.WriteLine(movie.Title);
                    Console.WriteLine(TAB + "Release Date: " + movie.ReleaseDate);
                    string genresStr = TAB + "Genres => ";
                    foreach (var genre in movie.MovieGenres)
                    {
                        if (genre != movie.MovieGenres.First())
                            genresStr += " | ";
                        genresStr += genre.Genre.Name;
                    }
                    if (movie.MovieGenres.Count() > 0)
                        Console.WriteLine(genresStr);

                    if (showReviews && movie.UserMovies.Count() > 0)
                    {
                        Console.WriteLine(TAB + "Reviews => ");
                        foreach (var userMovie in movie.UserMovies)
                        {
                            Console.WriteLine(TAB + TAB + "Review ID: " + userMovie.Id);
                            Console.WriteLine(TAB + TAB + TAB + "User ID: " + userMovie.User.Id);
                            Console.WriteLine(TAB + TAB + TAB + "Rating: " + userMovie.Rating);
                            Console.WriteLine(TAB + TAB + TAB + "Rated At: " + userMovie.RatedAt);
                        }
                    }
                    index++;
                }
            }

            Console.WriteLine();
            WaitForInput();
        }
    }
}
