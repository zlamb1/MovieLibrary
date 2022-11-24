using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibraryEntities.Models;

using MovieLibrary.Interfaces;
using MovieLibrary.Menus;
using MovieLibrary.Implementations.MovieImpl;
using MovieLibrary.Implementations.UserImpl;

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
            services
                .AddSingleton<IMenu, MainMenu>()
                .AddSingleton<IMenuContext, MenuContext>()
                .AddTransient<IFinder<Movie>, MovieFinder>()
                .AddTransient<IDisplay<Movie>, MovieDisplay>()
                .AddTransient<IBuilder<Movie>, MovieBuilder>()
                .AddTransient<IUpdater<Movie>, MovieUpdater>()
                .AddTransient<IDeleter<Movie>, MovieDeleter>()
                .AddTransient<IDisplay<User>, UserDisplay>()
                .AddTransient<IBuilder<User>, UserBuilder>();
            return services.BuildServiceProvider();
        }
    }
}
