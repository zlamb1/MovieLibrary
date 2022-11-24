using Microsoft.Extensions.Logging;
using MovieLibrary.Implementations.UserImpl;
using MovieLibrary.Interfaces;
using MovieLibrary.utility;
using MovieLibraryEntities.Models;
using System;

namespace MovieLibrary.Menus.UserMovies
{
    internal class AddMenu : Menu
    {
        // TODO: use dependency injection for these
        private IBuilder<User> builder = new UserBuilder();
        private IDisplay<User> display = new UserDisplay();
        public AddMenu(ILogger<IMenu> _logger) : base(_logger)
        {

        }
        public override void Start()
        {
            base.Start();

            var gender = InputUtility.GetStringWithPrompt("What is the user's gender? (M/F)\n");
            Console.WriteLine();

            var age = InputUtility.GetStringWithPrompt("What is the new user's age?\n");
            Console.WriteLine();

            var zipCode = InputUtility.GetStringWithPrompt("What is the new user's zip code?\n");
            Console.WriteLine();

            var occupation = InputUtility.GetStringWithPrompt("What is the new user's occupation?\n");
            Console.WriteLine();

            try
            {
                var user = builder.Build(gender, age, zipCode, occupation);
                display.Display(user);
            }
            catch (Exception exc)
            {
                LogError(exc.Message);
                Console.WriteLine(exc.Message);
            }

            Console.WriteLine();
            WaitForInput();
        }
    }
}
