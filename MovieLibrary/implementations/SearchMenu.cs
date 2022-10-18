using Microsoft.Extensions.Logging;
using MovieLibrary.interfaces;
using MovieLibrary.menus;
using MovieLibrary.models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MovieLibrary.implementations
{
    internal class SearchMenu : IMenu
    {
        private readonly string[] mediaTypes;
        private readonly ILogger<IMenu> logger;
        private readonly IFileDao dao;
        public SearchMenu(string[] _mediaTypes, ILogger<IMenu> _logger, IFileDao _dao)
        {
            mediaTypes = _mediaTypes;
            logger = _logger;
            dao = _dao;
        }
        public void Start()
        {
            if (dao != null)
            {
                string searchTitle = 
                    InputUtility.GetStringWithPrompt(
                        "What is the title of the movie you are searching for? (blank for all) ",
                        addColon: false);
                // I'm also not a big fan of how I'm doing this but it works
                List<Media[]> mediaArrays = new List<Media[]>();
                for (int i = 0; i < mediaTypes.Length; i++)
                {
                    dao.Args = new object[] { i };
                    dao.IgnoreFirstLine = true;
                    dao.File = "data/" + mediaTypes[i].ToLower() + ".csv";
                    // unsafe cast?
                    mediaArrays.Add(dao.Read().Cast<Media>().ToArray());
                }
                var allFiltered = new List<Media>();
                foreach (Media[] mediaArray in mediaArrays)
                {
                    var filtered = mediaArray.Where(x => 
                        x.Title.IndexOf(searchTitle, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                    foreach (Media media in filtered)
                        allFiltered.Add(media);
                }
                Console.WriteLine("<---Media Listing - " + allFiltered.Count + " Results--->");
                foreach (Media media in allFiltered)
                    Console.WriteLine(media.Title + " => " + media.GetType().Name);
            }
            else logger.LogError("Could not create IFileDao!");
        }
    }
}
