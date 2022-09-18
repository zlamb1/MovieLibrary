using MovieLibrary.interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MovieLibrary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var provider = new Startup().ConfigureServices();
            var menuManager = provider.GetService<IMenu>();
            menuManager.Start();
        }
    }
}


