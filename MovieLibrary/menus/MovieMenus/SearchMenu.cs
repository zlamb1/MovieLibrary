using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieLibrary.Interfaces;
using MovieLibrary.utility;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;
using System;
using System.Linq;

namespace MovieLibrary.Menus.MovieMenus
{
    internal class SearchMenu : Menu
    {
        private static int PAGE_SIZE = 5;
        private IDisplay<Movie> display;
        public SearchMenu(ILogger<IMenu> _logger, IDisplay<Movie> _display) : base(_logger)
        {
            display = _display;
        }
        public override void Start()
        {
            base.Start();
            string title = InputUtility.GetStringWithPrompt("What is title of the movie? (blank for all)\n");
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
                    display.Display(movie);
                    index++;
                }
            }
            Console.WriteLine();
            WaitForInput();
        }
    }
}
