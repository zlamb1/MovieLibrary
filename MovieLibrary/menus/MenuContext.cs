using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibrary.interfaces;
using System;

namespace MovieLibrary.menus
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
                        new SearchMenu(GetLogger<IMenu>()).Start();
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
