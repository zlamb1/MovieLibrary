using Microsoft.Extensions.Logging;
using MovieLibrary.Interfaces;
using MovieLibrary.utility;
using MovieLibraryEntities.Models;
using System;

namespace MovieLibrary.Menus.MovieMenus
{
    internal class DeleteMenu : Menu
    {

        private IDeleter<Movie> deleter;

        public DeleteMenu(ILogger<IMenu> _logger, IDeleter<Movie> _deleter) : base(_logger)
        {
            deleter = _deleter;
        }

        public override void Start()
        {
            base.Start();

            string movieName = InputUtility.GetStringWithPrompt(
                "What is the title of the movie?\n");
            
            try
            {
                deleter.Delete(movieName);
            } catch (Exception exc)
            {
                LogError(exc.Message);
                Console.WriteLine(exc.Message);
            }

            Console.WriteLine();
            WaitForInput();
        }
    }
}