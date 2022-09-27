using Microsoft.Extensions.DependencyInjection;
using MovieLibrary.interfaces;

namespace MovieLibrary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var provider = new Startup().ConfigureServices();
            var menu = provider.GetService<IMenu>();
            menu.Start();
        }
    }
}


