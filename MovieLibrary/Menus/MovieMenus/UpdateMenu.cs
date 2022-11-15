using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieLibrary.Interfaces;
using MovieLibrary.menus;
using MovieLibrary.utility;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;
using System;
using System.Linq;

namespace MovieLibrary.Menus.MovieMenus
{
    internal class UpdateMenu : Menu
    {
        private IDisplay<Movie> display;
        private IUpdater<Movie> updater;

        public UpdateMenu(ILogger<IMenu> _logger, IDisplay<Movie> _display, IUpdater<Movie> _updater) : base(_logger)
        {
            display = _display;
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

            Movie movie = null;
            int fieldChoice = 0;
            string value = null;

            using (var movieContext = new MovieContext())
            {
                movie = movieContext.Movies
                    .Include("MovieGenres.Genre")
                    .Include("UserMovies.User")
                    .FirstOrDefault(x => x.Title.StartsWith(title));

                if (movie is null)
                {
                    Restart("Could not find any movies with that title!");
                    return;
                }

                Console.WriteLine();
                display.Display(movie);
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

                fieldChoice = choice.Item2;

                Console.WriteLine();

                switch (fieldChoice)
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
            }

            try
            {
                updater.Update(movie, fieldChoice, value);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                Console.WriteLine(exc.InnerException);
            }


            Console.WriteLine();
            WaitForInput();
        }
    }
}