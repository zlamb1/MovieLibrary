using Microsoft.Extensions.Logging;
using MovieLibrary.interfaces;
using MovieLibrary.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieLibrary.implementations
{
    internal class MediaFileDao : IFileDao
    {
        private readonly ILogger<IFileDao> logger;
        public object[] Args { get; set; }
        public bool IgnoreFirstLine { get; set; }
        public string File { get; set; }
        public MediaFileDao(ILogger<IFileDao> _logger)
        {
            logger = _logger;
        }
        public object[] Read()
        {
            List<object> media = new List<object>();
            try
            {
                StreamReader reader = new StreamReader(File);
                if (IgnoreFirstLine && !reader.EndOfStream)
                    reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    Media obj;
                    switch((int)Args?[0])
                    {
                        case 0:
                            obj = new Movie(line);
                            break;
                        case 1:
                            obj = new Show(line);
                            break;
                        case 2:
                            obj = new Video(line);
                            break;
                        default:
                            obj = null;
                            break;
                    }
                    if (obj != null) media.Add(obj);
                }
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message);
            }
            return media.Cast<object>().ToArray();
        }
    }
}
