using MovieLibrary.interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MovieLibrary.menus
{
    internal class AddMovies : IMenu
    {
        private readonly ILogger<IMenu> logger;
        private readonly IFileDao handler;
        private readonly IFactory<Movie> movieFactory;
        private int tries = 1; 

        public AddMovies(ILogger<IMenu> _logger, IFileDao _handler, 
            IFactory<Movie> _movieFactory)
        {
            logger = _logger;
            handler = _handler;
            movieFactory = _movieFactory;
        }
        public int GetResults() { return 0; }
        public void Start()
        {
            logger.Log(LogLevel.Information, "Listing Movies Menu Started...");
            var file = "movies.csv";
            // IFileDao settings need to be applied before reading.
            handler.File = file;
            handler.IgnoreFirstLine = true;
            handler.Read();
            if (handler.ExceptionOccured)
            {
                // Sleep the thread to wait for the logger to finish logging any exceptions.
                // If this isn't done sometimes Console.WriteLine will mix with the logger message.
                Thread.Sleep(1);
                // If we had an exception occur we can attempt to restart the menu.
                bool cont = InputUtility.GetBoolWithPrompt(
                    prompt: "An exception has occured. Do you want to continue?");
                if (tries >= 4)
                {
                    Console.WriteLine("The menu has restarted five times. Terminating...");
                    return;
                }
                if (!cont) return;
                handler.Reset();
                tries++;
                Start();
                return;
            }
            List<object> Results = handler.Result;
            string id = InputUtility.GetStringWithPrompt(prompt: "Enter the new movie's ID (empty to auto-generate)");
            string title = InputUtility.GetStringWithPrompt(prompt: "Enter the new movie's title");
            string genres = InputUtility.GetStringWithPrompt(prompt: "Enter the new movies' genres (| delimited)");
            if (genres == "") genres = null;
            object movie = movieFactory.Create(
                id, title, genres, 
                Results.Cast<Movie>().ToList());
            if(movie == null)
            {
                bool cont = InputUtility.GetBoolWithPrompt(
                    prompt: "Do you want to try again?");
                if (tries >= 4)
                {
                    Console.WriteLine("The menu has restarted five times. Terminating...");
                    return;
                }
                handler.Reset();
                tries++;
                Start();
                return;
            } else
            {
                // write movie to file
                handler.Input = new List<object> { movie };
                handler.Write();
            }
        }
    }
}
