using Microsoft.Extensions.Logging;
using MovieLibrary.interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieLibrary.implementations
{
    internal class MovieFileDao : IFileDao
    {
        private ILogger<IFileDao> logger;
        public bool IgnoreFirstLine { get; set; }
        public string File { get; set; }
        public MovieFileDao(ILogger<IFileDao> _logger)
        {
            logger = _logger;
        }
        public object[] Read()
        {
            List<Movie> movies = new List<Movie>();
            try
            {
                StreamReader reader = new StreamReader(File);
                if (IgnoreFirstLine && !reader.EndOfStream)
                    reader.ReadLine();
                while(!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    Movie movie = new Movie(line);
                    movies.Add(movie);
                }
            } catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message);
            }
            return movies.Cast<object>().ToArray();
        }
    }
}
