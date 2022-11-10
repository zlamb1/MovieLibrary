using Microsoft.Extensions.Logging;
using MovieLibrary.menus;
using MovieLibrary.utility;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            using (var db = new MovieContext())
            {
                Console.WriteLine();

                Movie movie = new Movie();

                var title = InputUtility.GetStringWithPrompt("What is the title of the movie? ");
                if (string.IsNullOrEmpty(title))
                {
                    Restart("The movie title cannot be null/blank!");
                    return;
                }
                movie.Title = title;

                Console.WriteLine();

                var releaseDateStr = InputUtility.GetStringWithPrompt("What is the movie's release date (blank for current date)? ");
                if (string.IsNullOrEmpty(releaseDateStr))
                    movie.ReleaseDate = DateTime.Now;
                else
                    movie.ReleaseDate = DateTime.Parse(releaseDateStr);

                Console.WriteLine();
                Console.WriteLine("Generated new movie => ");
                Console.WriteLine("    Id: " + movie.Id);
                Console.WriteLine("    Title: " + movie.Title);
                Console.WriteLine("    Release Date: " + movie.ReleaseDate);

                db.Add(movie);
                db.SaveChanges();
            }

            Console.WriteLine();
            WaitForInput();
        }
    }
}
