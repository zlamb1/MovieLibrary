using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibraryEntities.Models;

using MovieLibrary.Interfaces;
using MovieLibrary.Implementations;
using MovieLibrary.Menus;

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
            .AddTransient<IFinder<Movie>, MovieFinder>()
            .AddTransient<IDisplay<Movie>, MovieDisplay>()
            .AddTransient<IUpdater<Movie>, MovieUpdater>()
            .AddTransient<IDeleter<Movie>, MovieDeleter>();
            return services.BuildServiceProvider();
        }
    }
}
