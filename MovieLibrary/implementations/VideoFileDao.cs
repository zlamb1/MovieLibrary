using Microsoft.Extensions.Logging;
using MovieLibrary.interfaces;
using MovieLibrary.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieLibrary.implementations
{
    internal class VideoFileDao : IFileDao
    {
        private ILogger<IFileDao> logger;
        public bool IgnoreFirstLine { get; set; }
        public string File { get; set; }
        public VideoFileDao(ILogger<IFileDao> _logger)
        {
            logger = _logger;
        }
        public object[] Read()
        {
            List<Video> videos = new List<Video>();
            try
            {
                StreamReader reader = new StreamReader(File);
                if (IgnoreFirstLine && !reader.EndOfStream)
                    reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    Video video = new Video(line);
                    videos.Add(video);
                }
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message);
            }
            return videos.Cast<object>().ToArray();
        }
    }
}