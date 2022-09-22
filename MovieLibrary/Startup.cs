using System;
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
            return services.BuildServiceProvider();
        }

    }
}
