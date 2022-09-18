using MovieLibrary.interfaces;
using MovieLibrary.movieutility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace MovieLibrary.menus
{
    internal class MenuFactory : IFactory<IMenu>
    {
        private readonly ILogger<IMenu> logger;
        private readonly IServiceProvider provider;
        public MenuFactory(ILogger<IMenu> _logger, IServiceProvider _provider)
        {
            logger = _logger;
            provider = _provider;
        }
        public IMenu Create(params object[] args)
        {
            // default to listing movies in the case of an exception
            int index = 1;
            try
            {
                index = (int)args[0];
            } catch (ArgumentOutOfRangeException)
            {
                logger.Log(LogLevel.Error, "Menu ID not provided to menu factory!");
            } catch (InvalidCastException)
            {
                logger.Log(LogLevel.Error, "Invalid cast when creating menu!");
            }
            switch (index)
            {
                case 1:
                    return new ListMovies(
                        logger, 
                        provider.GetService<IFileDao>(),
                        provider.GetService<IFilter>());
                case 2:
                    // I don't know how to get multiple implementations of one interface
                    // from the service provider, and it doesn't really make sense to me
                    // to make a FactoryFactory. (so in this case I just instantiate it myself)
                    return new AddMovies(
                        logger, 
                        provider.GetService<IFileDao>(),
                        new MovieFactory(provider.GetService<ILogger<IFactory<Movie>>>()));
                default:
                    return new MainMenu(logger);
            }
        }
    }
}
