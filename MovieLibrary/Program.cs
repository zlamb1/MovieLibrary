using Microsoft.Extensions.DependencyInjection;
using MovieLibrary.Interfaces;

namespace MovieLibrary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var provider = new Startup().ConfigureServices();
            var menuContext = provider.GetService<IMenuContext>();
            menuContext.Start();
        }
    }
}


