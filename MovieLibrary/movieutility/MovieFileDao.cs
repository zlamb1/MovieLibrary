using MovieLibrary.interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary
{
    internal class MovieFileDao : IFileDao
    {
        // file dao designed to read and write movies.csv
        private ILogger<IFileDao> logger;
        public List<object> Input { get; set; }
        public List<object> Result { get; set; }
        public bool ExceptionOccured { get; set; }
        public bool IgnoreFirstLine { get; set; }
        public string File { get; set; }
        public MovieFileDao(ILogger<IFileDao> _logger)
        {
            logger = _logger;
            Result = new List<object>();
            IgnoreFirstLine = false;
            ExceptionOccured = false;
        }
        public void Read()
        {
            if (File == null) return;
            logger.Log(LogLevel.Information, "Reading movie file!");
            try
            {
                StreamReader reader = new StreamReader(File);
                if (IgnoreFirstLine && !reader.EndOfStream)
                    reader.ReadLine();
                while (!reader.EndOfStream)
                { 
                    string line = reader.ReadLine();
                    Movie movie;
                    if (line.Contains('"'))
                    {
                        int firstOccurence = line.IndexOf('"');
                        string quotation = line.Substring(firstOccurence, line.LastIndexOf('"') - firstOccurence + 1);
                        string removed = line.Replace("," + quotation, "");
                        string[] parts = removed.Split(',');
                        //Console.WriteLine("," + quotation);
                        movie = new Movie(int.Parse(parts[0]), quotation.Replace("\"", ""), parts[1]);
                    } else
                    {
                        string[] parts = line.Split(',');
                        movie = new Movie(int.Parse(parts[0]), parts[1], parts[2]);
                    }
                    Result.Add(movie);
                }
                reader.Close();
                return;
            }
            catch (FileNotFoundException)
            {
                logger.Log(LogLevel.Error, $"File Not Found: {File}.");
            }
            catch (IOException)
            {
                logger.Log(LogLevel.Error, "Unknown IOException.");
            }
            ExceptionOccured = true;
        }
        public void Write()
        {
            try
            {
                StreamWriter sw = new StreamWriter(File, true);
                foreach (object movie in Input)
                {
                    logger.Log(LogLevel.Information, "Writing: " + movie);
                    sw.WriteLine(movie);
                }
                sw.Close();
            } catch (Exception ex)
            {
                logger.Log(LogLevel.Error, "Error while writing movie: " + ex.Message);
                ExceptionOccured = true;
            }
        }
        public void Reset()
        {
            Result.Clear();
            ExceptionOccured = false;
            IgnoreFirstLine = false;
            File = null;
        }

    }
}
