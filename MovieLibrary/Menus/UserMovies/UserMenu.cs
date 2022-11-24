using Microsoft.Extensions.Logging;
using MovieLibrary.Interfaces;

namespace MovieLibrary.Menus.UserMovies
{
    internal class UserMenu : Menu
    {
        public UserMenu(ILogger<IMenu> _logger) : base(_logger)
        {

        }
    }
}
