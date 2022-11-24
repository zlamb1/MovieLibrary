using Microsoft.Extensions.Logging;
using MovieLibrary.Interfaces;
using MovieLibrary.utility;
using System;

namespace MovieLibrary.Menus.UserMenus
{
    internal class UserMenu : Menu
    {
        public UserMenu(ILogger<IMenu> _logger) : base(_logger)
        {
            base.Start();
            Console.WriteLine("Choose an option: ");
            Console.WriteLine("1) Add User");
            var choice = InputUtility.GetInt32WithPrompt();
            if (!choice.Item1 || (choice.Item2 < 1 || choice.Item2 > 1))
            {
                Restart("That is not a valid choice!");
                return;
            }
            Result = choice.Item2;
        }
    }
}
