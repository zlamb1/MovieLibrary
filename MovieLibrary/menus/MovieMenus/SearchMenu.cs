using Microsoft.Extensions.Logging;
using MovieLibrary.utility;
using MovieLibraryEntities.Context;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            string title = InputUtility.GetStringWithPrompt("What is title of the movie? ");

            using (var db = new MovieContext())
            {
                var sorted = db.Movies.Where(x => x.Title.StartsWith(title));

                Console.WriteLine(sorted.Count() + " Movie(s) returned!");
                Console.WriteLine();

                foreach (var movie in sorted)
                {
                    Console.WriteLine(movie.Title);
                }
            }

            Console.WriteLine();
            WaitForInput();
        }
    }
}
