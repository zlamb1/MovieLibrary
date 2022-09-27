using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibrary.interfaces;
using System;

namespace MovieLibrary.implementations
{
    internal class MenuFactory : IFactory
    {
        private readonly IServiceProvider provider;
        private readonly Func<int, IFactory> factory;
        public MenuFactory(IServiceProvider _provider, Func<int, IFactory> _factory)
        {
            provider = _provider;
            factory = _factory;
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
                        (IFileDao)factory(1).Create(type));
                default:
                    return null;
            }
        }
    }
}
