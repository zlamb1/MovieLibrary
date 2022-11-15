using Microsoft.Extensions.Logging;
using MovieLibrary.Interfaces;
using MovieLibrary.Menus;
using MovieLibrary.utility;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;
using System;

namespace MovieLibrary.Menus.MovieMenus
{
    internal class AddMenu : Menu
    {
        public AddMenu(ILogger<IMenu> _logger) : base(_logger)
        {

        }

        public override void Start()
        {
            base.Start();

            var movie = new Movie();

            var title = InputUtility.GetStringWithPrompt("What is the title of the movie?\n");
            if (string.IsNullOrEmpty(title))
            {
                Restart("The movie title cannot be null/blank!");
                return;
            }
            movie.Title = title;

            Console.WriteLine();

            // TODO: add genre selection

            var releaseDateStr = InputUtility.GetStringWithPrompt("What is the movie's release date (blank for current date)?\n");
            if (string.IsNullOrEmpty(releaseDateStr))
                movie.ReleaseDate = DateTime.Now;
            else
                movie.ReleaseDate = DateTime.Parse(releaseDateStr);

            Console.WriteLine();
            Console.WriteLine("Generated new movie => ");
            Console.WriteLine("    Id: " + movie.Id);
            Console.WriteLine("    Title: " + movie.Title);
            Console.WriteLine("    Release Date: " + movie.ReleaseDate);

            using (var db = new MovieContext())
            {
                db.Add(movie);
                db.SaveChanges();
            }

            Console.WriteLine();
            WaitForInput();
        }
    }
}
