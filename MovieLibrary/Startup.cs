using System;
using MovieLibrary.menus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibrary.interfaces;
using MovieLibrary.Interfaces;
using MovieLibrary.Display;
using MovieLibraryEntities.Models;
using MovieLibrary.Updater;

namespace MovieLibrary
{
    internal class Startup
    {
        public IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddFile("app.log");
            });
            services.AddSingleton<IMenu, MainMenu>()
            .AddSingleton<IMenuContext, MenuContext>()
            .AddTransient<IDisplay<Movie>, MovieDisplay>()
            .AddTransient<IUpdater<Movie>, MovieUpdater>();
            return services.BuildServiceProvider();
        }

    }
}
