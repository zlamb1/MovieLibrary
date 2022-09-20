using MovieLibrary.interfaces;
using FS.FilterExpressionCreator.Enums;
using FS.FilterExpressionCreator.Extensions;
using FS.FilterExpressionCreator.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MovieLibrary.menus
{
    internal class ListMovies : IMenu
    {
        private readonly ILogger<IMenu> logger;
        private readonly IFilter filter; 
        private int tries = 1; 
        private IFileDao handler; 
        public ListMovies(ILogger<IMenu> _logger, IFileDao _handler, IFilter _filter)
        {
            logger = _logger;
            handler = _handler;
            filter = _filter;
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
                if (tries >= 2)
                {
                    Console.WriteLine("An exception has occured three times. Terminating...");
                    return;
                }
                if (!cont) return;
                handler.Reset();
                tries++;
                Start();
                return;
            }
            // Cast the default List<object> into List<Movie>.
            // We can now work with the results from the read file.
            Console.WriteLine("Warning: not filtering will print out all of the movies.");
            bool filtering = InputUtility.GetBoolWithPrompt(
                prompt: "Do you want to filter the results?");
            List<object> Results = handler.Result;
            if (filtering)
            {
                filter.Input = handler.Result;
                Console.WriteLine("<---Filtering Utility--->");
                Console.WriteLine("Filtering Operators: =, <, > (place as first char of filter)");
                Console.WriteLine("If no operators are entered, the default is contains.");
                Console.WriteLine("Enter nothing if you want no filtering in that field.");
                filter.Filter(
                    InputUtility.GetStringWithPrompt(prompt: "Enter MovieID Filter"),
                    InputUtility.GetStringWithPrompt(prompt: "Enter Movie Title Filter"),
                    InputUtility.GetStringWithPrompt(prompt: "Enter Movie Genre Filter (| delimited, no operators)"));
                Console.WriteLine("<-------Results--------->");
                Results = filter.Output;
            }
            int i = 0;
            int iter = 10;
            while (true)
            {
                int iterateTo = Math.Min(i * iter + iter, Results.Count);
                Console.WriteLine("<---" + (i * iter) + " - " + iterateTo + "--->");
                for (int j = (i * iter); j < iterateTo; j++)
                {
                    Console.WriteLine((j + 1) + ". " + Results[j]);
                }
                Console.WriteLine("<------------>");
                bool cont = InputUtility.GetBoolWithPrompt(prompt: "Do you want to print the next " + iter + " results?");
                if (!cont) break;
                i++;
            }
        }
    }
}
