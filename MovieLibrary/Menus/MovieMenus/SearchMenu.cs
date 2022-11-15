using Microsoft.Extensions.Logging;
using MovieLibrary.Interfaces;
using MovieLibrary.utility;
using MovieLibraryEntities.Models;
using System;
using System.Linq;

namespace MovieLibrary.Menus.MovieMenus
{
    internal class SearchMenu : Menu
    {
        private static int PAGE_SIZE = 5;
        private IFinder<Movie> finder;
        private IDisplay<Movie> display;
        public SearchMenu(ILogger<IMenu> _logger, 
            IFinder<Movie> _finder, IDisplay<Movie> _display) : base(_logger)
        {
            finder = _finder;
            display = _display;
        }
        public override void Start()
        {
            base.Start();
            string title = InputUtility.GetStringWithPrompt("What is title of the movie? (blank for all)\n");

            var movies = finder.Find(title);

            Console.Clear();
            Console.WriteLine(movies.Count() + " Movie(s) returned!");
            Console.WriteLine();

            int index = 0;
            foreach (var movie in movies)
            {
                if (index % PAGE_SIZE == 0 && index >= PAGE_SIZE)
                {
                    Console.WriteLine();
                    bool result = InputUtility.GetBoolWithPrompt("Would you like to continue to the next page? (Y/N)");
                    if (!result) break;
                    Console.Clear();
                    int page = (index + 1) / PAGE_SIZE;
                    Console.WriteLine("Page " + page + "/" +
                        Math.Ceiling((float)movies.Count() / PAGE_SIZE));
                    Console.WriteLine();
                }

                display.Display(movie);
                index++;
            }

            Console.WriteLine();
            WaitForInput();
        }
    }
}
