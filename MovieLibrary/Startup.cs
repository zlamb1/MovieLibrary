using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibrary.implementations;
using MovieLibrary.interfaces;

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
            services.AddTransient<IMenu, AggregateMenu>()
            .AddTransient<IFactory, MenuFactory>()
            .AddTransient<IFileDao, MediaFileDao>();
            return services.BuildServiceProvider();
        }

    }
}
