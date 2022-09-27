using Microsoft.Extensions.Logging;
using MovieLibrary.interfaces;
using MovieLibrary.models;
using System;

namespace MovieLibrary.implementations
{
    internal class ListingMenu : IMenu
    {
        private readonly string[] types = { "Movies", "Shows", "Videos" };
        private readonly int listType;
        private readonly ILogger<IMenu> logger;
        private readonly IFileDao dao;
        public ListingMenu(int _listType, ILogger<IMenu> _logger, IFileDao _dao)
        {
            listType = _listType;
            logger = _logger;
            dao = _dao;
        }
        public void Start()
        {
            if (dao != null)
            {
                dao.IgnoreFirstLine = true;
                dao.File = "data/" + types[listType].ToLower() + ".csv";
                Console.WriteLine("<---" + types[listType] + " Listing--->");
                object[] objects = dao.Read();
                for (int i = 0; i < Math.Min(10, objects.Length); i++)
                    ((Media)objects[i]).Display();
            }
            else logger.LogError("Could not create IFileDao!");
        }
    }
}
