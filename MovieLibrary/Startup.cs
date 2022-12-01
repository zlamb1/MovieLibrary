using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibraryEntities.Models;

using MovieLibrary.Interfaces;
using MovieLibrary.Menus;
using MovieLibrary.Implementations.MovieImpl;
using MovieLibrary.Implementations.UserImpl;
using MovieLibrary.Implementations;
using MovieLibrary.Implementations.UserMovieImpl;
using MovieLibrary.Implementations.RatingImpl;
using MovieLibrary.Implementations.OccupationImpl;

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

                .AddTransient<IBuilder<Movie>, MovieBuilder>()
                .AddTransient<IDeleter<Movie>, MovieDeleter>()
                .AddTransient<IDisplay<Movie>, MovieDisplay>()
                .AddTransient<IFinder<Movie>, MovieFinder>()
                .AddTransient<IUpdater<Movie>, MovieUpdater>()

                .AddTransient<IBuilder<User>, UserBuilder>()
                .AddTransient<IDisplay<User>, UserDisplay>()
                .AddTransient<IFinder<User>, UserFinder>()

                .AddTransient<IBuilder<UserMovie>, RatingBuilder>()
                .AddTransient<IDisplay<UserMovie>, RatingDisplay>()
                .AddTransient<ISorter<UserMovie>, RatingSorter>()
                
                .AddTransient<IFinder<Occupation>, OccupationFinder>();
                
            return services.BuildServiceProvider();
        }
    }
}
