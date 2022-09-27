using Microsoft.Extensions.Logging;
using MovieLibrary.interfaces;
using MovieLibrary.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieLibrary.implementations
{
    internal class ShowFileDao : IFileDao
    {
        private ILogger<IFileDao> logger;
        public bool IgnoreFirstLine { get; set; }
        public string File { get; set; }
        public ShowFileDao(ILogger<IFileDao> _logger)
        {
            logger = _logger;
        }
        public object[] Read()
        {
            List<Show> shows = new List<Show>();
            try
            {
                StreamReader reader = new StreamReader(File);
                if (IgnoreFirstLine && !reader.EndOfStream)
                    reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    Show show = new Show(line);
                    shows.Add(show);
                }
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message);
            }
            return shows.Cast<object>().ToArray();
        }
    }
}
