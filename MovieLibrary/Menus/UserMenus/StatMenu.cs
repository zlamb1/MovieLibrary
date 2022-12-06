using Microsoft.Extensions.Logging;
using MovieLibrary.Implementations.UserImpl;
using MovieLibrary.Interfaces;
using MovieLibraryEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Menus.UserMenus
{
    internal class StatMenu : Menu
    {

        private IDisplay<User> userDisplay;
        private ISorter<User> userSorter;

        public StatMenu(IDisplay<User> _userDisplay,
            ISorter<User> _userSorter,
            ILogger<IMenu> _logger) : base(_logger)
        {
            userDisplay = _userDisplay;
            userSorter = _userSorter;
        }

        public override void Start()
        {
            base.Start();

            var list = userSorter.Sort();
            var userInfo = (UserInfo)list[0];

            Console.WriteLine("User Statistics =>");
            Console.WriteLine($"\tAverage User Age => {(int)userInfo.GetAverageAge()} years old");
            Console.WriteLine($"\tAverage User Occupation => {userInfo.GetMostPopularOccupation().Name}");
            Console.WriteLine($"\tNumber of Male Users => {userInfo.TotalMales}");
            Console.WriteLine($"\tNumber of Female Users => {userInfo.TotalFemales}");

            Console.WriteLine();
            WaitForInput();
        }
    }
}
