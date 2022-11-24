using Microsoft.Extensions.Logging;
using MovieLibrary.Interfaces;
using MovieLibrary.Menus.MovieMenus;
using MovieLibrary.utility;
using System;

namespace MovieLibrary.Menus
{
    internal class MainMenu : Menu
    {
        public MainMenu(ILogger<IMenu> _logger) : base(_logger)
        {
            
        }

        public override void Start()
        {
            base.Start();
            Console.WriteLine("Choose a menu to start: ");
            Console.WriteLine("1) Movie Menu");
            Console.WriteLine("2) User Menu");
            var choice = InputUtility.GetInt32WithPrompt();
            if (!choice.Item1 || (choice.Item2 < 1 || choice.Item2 > 4))
            {
                Restart("That is not a valid choice!");
                return;
            }
            Result = choice.Item2;
        }
    }
}
