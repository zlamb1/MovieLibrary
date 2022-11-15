using Microsoft.Extensions.Logging;
using MovieLibrary.utility;
using System;

namespace MovieLibrary.menus
{
    internal class MainMenu : Menu
    {
        public MainMenu(ILogger<IMenu> _logger) : base(_logger)
        {

        }

        public override void Start()
        {
            base.Start();
            Console.WriteLine("Choose an option: ");
            Console.WriteLine("1) Search Movies");
            Console.WriteLine("2) Add Movie");
            Console.WriteLine("3) Update Movie");
            Console.WriteLine("4) Delete Movie");
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
