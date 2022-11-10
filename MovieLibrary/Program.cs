using Microsoft.Extensions.DependencyInjection;
using MovieLibraryEntities.Context;
using System;

namespace MovieLibrary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var provider = new Startup().ConfigureServices();
            var mainMenu = provider.GetService<IMenu>();
            mainMenu.Start();
        }
    }
}


