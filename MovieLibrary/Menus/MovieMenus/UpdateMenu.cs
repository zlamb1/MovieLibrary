using Microsoft.Extensions.Logging;
using MovieLibrary.Interfaces;
using MovieLibrary.utility;
using MovieLibraryEntities.Models;
using System;

namespace MovieLibrary.Menus.MovieMenus
{
    internal class UpdateMenu : Menu
    {
        private IDisplay<Movie> display;
        private IFinder<Movie> finder;
        private IUpdater<Movie> updater;

        public UpdateMenu(ILogger<IMenu> _logger, 
            IDisplay<Movie> _display, 
            IFinder<Movie> _finder,
            IUpdater<Movie> _updater) : base(_logger)
        {
            display = _display;
            finder = _finder;
            updater = _updater;
        }

        public override void Start()
        {
            base.Start();
            string title = InputUtility.GetStringWithPrompt("What is title of the movie?\n");

            if (string.IsNullOrEmpty(title))
            {
                Restart("You cannot search for a movie with a blank/null title!");
                return;
            }

            var movie = finder.First(title);

            if (movie is null)
            {
                Restart("Could not find any movies with that title!");
                return;
            }

            Console.WriteLine();
            
            try
            {
                display.Display(movie);
            }
            catch (Exception exc)
            {
                LogError(exc.Message);
                Console.WriteLine(exc.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Choose an option: ");
            Console.WriteLine("1) Update Title");
            Console.WriteLine("2) Update Genres");
            Console.WriteLine("3) Update Release Date");

            var choice = InputUtility.GetInt32WithPrompt();

            if (!choice.Item1 || (choice.Item2 < 1 || choice.Item2 > 3))
            {
                Restart("That is not a valid choice! (1 - 3)");
                return;
            }

            Console.WriteLine();

            string value = null;
            switch (choice.Item2)
            {
                case 1:
                    value = InputUtility.GetStringWithPrompt("What is the new title of the movie?\n");
                    break;
                case 2:
                    value = InputUtility.GetStringWithPrompt("What are the new genres of the movie? (| delimited)\n");
                    break;
                case 3:
                    value = InputUtility.GetStringWithPrompt("What is the new release date of the movie? (blank for current date)\n");
                    break;
            }

            try
            {
                updater.Update(movie, choice.Item2, value);
            }
            catch (Exception exc)
            {
                LogError(exc.Message);
                Console.WriteLine(exc.Message);
            }

            Console.WriteLine();
            WaitForInput();
        }
    }
}