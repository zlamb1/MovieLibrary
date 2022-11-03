using Microsoft.Extensions.DependencyInjection;
using MovieLibraryEntities.Context;
using System;

namespace MovieLibrary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // print all movies
            using (var db = new MovieContext())
            {
                foreach (var movie in db.Movies)
                {
                    Console.WriteLine(movie.Title);
                }
            }

            var provider = new Startup().ConfigureServices();
            var aggregateMenu = provider.GetService<IMenu>();
            aggregateMenu.Start();
        }
    }
}


