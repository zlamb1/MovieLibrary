﻿using Microsoft.Extensions.Logging;
using MovieLibrary.Implementations.UserImpl;
using MovieLibrary.Interfaces;
using MovieLibrary.utility;
using MovieLibraryEntities.Models;
using System;

namespace MovieLibrary.Menus.UserMenus
{
    internal class AddMenu : Menu
    {
        // TODO: use dependency injection for these
        private IBuilder<User> builder;
        private IDisplay<User> display;
        public AddMenu(ILogger<IMenu> _logger,
            IBuilder<User> _builder,
            IDisplay<User> _display) : base(_logger)
        {
            builder = _builder;
            display = _display;
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