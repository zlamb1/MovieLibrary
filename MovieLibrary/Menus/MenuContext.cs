using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibrary.Interfaces;
using MovieLibrary.Menus.MovieMenus;
using MovieLibrary.Menus.UserMenus;
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
                        var movieMenu = new MovieMenu(GetLogger<IMenu>());
                        movieMenu.Start();
                        SelectMovieMenu(movieMenu.Result);
                        break;
                    case 2:
                        var userMenu = new UserMenu(GetLogger<IMenu>());
                        userMenu.Start();
                        SelectUserMenu(userMenu.Result);
                        break;
                }
            }
        }
        private void SelectMovieMenu(int result)
        {
            switch (result)
            {
                case 1:
                    new SearchMenu(
                        GetLogger<IMenu>(),
                        provider.GetService<IFinder<Movie>>(),
                        provider.GetService<IDisplay<Movie>>()).Start();
                    break;
                case 2:
                    new MovieMenus.AddMenu(
                        GetLogger<IMenu>(),
                        provider.GetService<IBuilder<Movie>>(),
                        provider.GetService<IDisplay<Movie>>()).Start();
                    break;
                case 3:
                    new UpdateMenu(
                        GetLogger<IMenu>(),
                        provider.GetService<IDisplay<Movie>>(),
                        provider.GetService<IFinder<Movie>>(),
                        provider.GetService<IUpdater<Movie>>()).Start();
                    break;
                case 4:
                    new DeleteMenu(
                        GetLogger<IMenu>(),
                        provider.GetService<IDeleter<Movie>>()).Start();
                    break;
                default:
                    break;
            }
        }
        private void SelectUserMenu(int result)
        {
            switch (result)
            {
                case 1:
                    new UserMenus.AddMenu(
                        GetLogger<IMenu>(),
                        provider.GetService<IBuilder<User>>(),
                        provider.GetService<IDisplay<User>>()).Start();
                    break;
                case 2:
                    new RatingMenu(
                        provider.GetService<IFinder<Movie>>(),
                        provider.GetService<IDisplay<Movie>>(),
                        provider.GetService<IFinder<User>>(), 
                        provider.GetService<IDisplay<User>>(),
                        provider.GetService<IBuilder<UserMovie>>(),
                        provider.GetService<IDisplay<UserMovie>>(),
                        GetLogger<IMenu>()).Start();
                    break;
                default:
                    break;
            }
        }
        private ILogger<T> GetLogger<T>()
        {
            return provider.GetService<ILogger<T>>();
        }
    }
}
