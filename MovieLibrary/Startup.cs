using System;
using MovieLibrary.interfaces;
using MovieLibrary.menus;
using MovieLibrary.movieutility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
            services.AddSingleton<IMenu, AggregateMenu>()
                .AddTransient<IFactory<IMenu>, MenuFactory>()
                .AddTransient<IFileDao, MovieFileDao>()
                .AddTransient<IFilter, MovieFilter>();
            return services.BuildServiceProvider();
        }

    }
}
