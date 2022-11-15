using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibrary.Interfaces;
using MovieLibrary.Menus.MovieMenus;
using MovieLibraryEntities.Models;
using System;

namespace MovieLibrary.Menus
{
    internal class MenuContext : IMenuContext
    {
        private readonly IServiceProvider provider;

        public MenuContext(IServiceProvider _provider)
        {
            provider = _provider;
        }

        public void Start()
        {
            while (true)
            {
                var mainMenu = provider.GetService<IMenu>();
                mainMenu.Start();
                switch (mainMenu.Result)
                {
                    case 1:
                        new SearchMenu(
                            GetLogger<IMenu>(),
                            provider.GetService<IDisplay<Movie>>())
                            .Start();
                        break;
                    case 2:
                        new AddMenu(GetLogger<IMenu>())
                            .Start();
                        break;
                    case 3:
                        new UpdateMenu(
                            GetLogger<IMenu>(),
                            provider.GetService<IDisplay<Movie>>(), 
                            provider.GetService<IUpdater<Movie>>())
                            .Start();
                        break;
                    case 4:
                        new DeleteMenu(
                            GetLogger<IMenu>(),
                            provider.GetService<IDeleter<Movie>>())
                            .Start();
                        break;
                    default:
                        break;
                }
            }
        }
        private ILogger<T> GetLogger<T>()
        {
            return provider.GetService<ILogger<T>>();
        }
    }
}
