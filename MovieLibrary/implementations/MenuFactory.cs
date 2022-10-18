using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibrary.interfaces;
using System;

namespace MovieLibrary.implementations
{
    internal class MenuFactory : IFactory
    {
        // I don't know if I'm a big fan of passing this into menu instances, but it works for now.
        private static readonly string[] types = { "Movies", "Shows", "Videos" };

        private readonly IServiceProvider provider;
        public MenuFactory(IServiceProvider _provider)
        {
            provider = _provider;
        }
        public object Create(params object[] args)
        {
            switch(args?[0])
            {
                case 0:
                    return new ChoiceMenu();
                case 1 or 2 or 3:
                    int type = (int)args[0] - 1;
                    return new ListingMenu(types, type, provider.GetService<ILogger<IMenu>>(),
                        provider.GetService<IFileDao>());
                case 4:
                    return new SearchMenu(types, provider.GetService<ILogger<IMenu>>(),
                        provider.GetService<IFileDao>());
                default:
                    return null;
            }
        }
    }
}
