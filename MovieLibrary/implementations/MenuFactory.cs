using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibrary.interfaces;
using System;

namespace MovieLibrary.implementations
{
    internal class MenuFactory : IFactory
    {
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
                    return new ListingMenu(type, provider.GetService<ILogger<IMenu>>(),
                        provider.GetService<IFileDao>());
                default:
                    return null;
            }
        }
    }
}
