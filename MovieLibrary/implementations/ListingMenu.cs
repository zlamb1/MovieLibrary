using Microsoft.Extensions.Logging;
using MovieLibrary.interfaces;
using MovieLibrary.models;
using System;

namespace MovieLibrary.implementations
{
    internal class ListingMenu : IMenu
    {
        private readonly string[] types = { "Movies", "Shows", "Videos" };
        private readonly int mediaType;
        private readonly ILogger<IMenu> logger;
        private readonly IFileDao dao;
        public ListingMenu(int _mediaType, ILogger<IMenu> _logger, IFileDao _dao)
        {
            mediaType = _mediaType;
            logger = _logger;
            dao = _dao;
        }
        public void Start()
        {
            if (dao != null)
            {
                dao.Args = new object[] { mediaType };
                dao.IgnoreFirstLine = true;
                dao.File = "data/" + types[mediaType].ToLower() + ".csv";
                Console.WriteLine("<---" + types[mediaType] + " Listing--->");
                object[] objects = dao.Read();
                for (int i = 0; i < Math.Min(10, objects.Length); i++)
                    ((Media)objects[i]).Display();
            }
            else logger.LogError("Could not create IFileDao!");
        }
    }
}
