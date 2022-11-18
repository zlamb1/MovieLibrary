using Microsoft.Extensions.Logging;
using MovieLibrary.Interfaces;
using MovieLibrary.utility;
using MovieLibraryEntities.Models;
using System;

namespace MovieLibrary.Menus.MovieMenus
{
    internal class AddMenu : Menu
    {

        private IBuilder<Movie> builder;
        private IDisplay<Movie> display;

        public AddMenu(ILogger<IMenu> _logger, 
            IBuilder<Movie> _builder,
            IDisplay<Movie> _display) : base(_logger)
        {
            builder = _builder;
            display = _display;
        }

        public override void Start()
        {
            base.Start();

            var title = InputUtility.GetStringWithPrompt("What is the title of the movie?\n");
            Console.WriteLine();

            var placeholderTitle = title;
            if (string.IsNullOrEmpty(placeholderTitle))
            {
                placeholderTitle = "(no title specified)";
            }

            var genres = InputUtility.GetStringWithPrompt(
                $"What are the genres of {placeholderTitle}? " +
                $"(| delimited, blank for none)\n");
            Console.WriteLine();

            var releaseDate = InputUtility.GetStringWithPrompt(
                $"What is {placeholderTitle}'s release date? (blank for current date)\n");
            Console.WriteLine();

            try
            {
                var movie = builder.Build(title, genres, releaseDate);
                display.Display(movie);
            } catch(Exception exc) { 
                Console.WriteLine(exc.Message);
            }

            Console.WriteLine();
            WaitForInput();
        }
    }
}
