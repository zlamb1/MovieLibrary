using Microsoft.Extensions.Logging;
using MovieLibrary.interfaces;
using MovieLibrary.models;
using System;

namespace MovieLibrary.implementations
{
    internal class ListingMenu : IMenu
    {
        private readonly string[] mediaTypes; 
        private readonly int mediaType;
        private readonly ILogger<IMenu> logger;
        private readonly IFileDao dao;
        public ListingMenu(string[] _mediaTypes, int _mediaType, ILogger<IMenu> _logger, IFileDao _dao)
        {
            mediaTypes = _mediaTypes;
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
                dao.File = "data/" + mediaTypes[mediaType].ToLower() + ".csv";
                Console.WriteLine("<---" + mediaTypes[mediaType] + " Listing--->");
                object[] objects = dao.Read();
                for (int i = 0; i < Math.Min(10, objects.Length); i++)
                    ((Media)objects[i]).Display();
            }
            else logger.LogError("Could not create IFileDao!");
        }
    }
}
