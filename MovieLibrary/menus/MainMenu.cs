using MovieLibrary.menus;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MovieLibrary
{
    internal class MainMenu : IMenu
    {
        private readonly ILogger<IMenu> logger;
        private int choice;

        public MainMenu(ILogger<IMenu> _logger)
        {
            logger = _logger;
        }
        public int GetResults() { return choice; }
        public void Start()
        {
            logger.Log(LogLevel.Information, "Program Starting...");
            Console.WriteLine("<--- Options --->");
            Console.WriteLine("1. List all movies in a file.");
            Console.WriteLine("2. Add movies to file.");
            Console.WriteLine("3. Exit");
            Console.WriteLine("<--------->");
            choice = InputUtility.GetInt32WithPrompt("Enter option", expected: new int[] { 1, 2, 3 });
        }
    }
}
