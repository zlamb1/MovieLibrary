using MovieLibrary.interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.movieutility
{
    internal class MovieFactory : IFactory<Movie>
    {
        private readonly ILogger<IFactory<Movie>> logger;
        public MovieFactory(ILogger<IFactory<Movie>> _logger)
        {
            logger = _logger;
        }
        public Movie Create(params object[] args)
        {
            string _params = "[ int, string, string, List<Movie> ]";
            if (args.Length < 4)
            {
                logger.Log(LogLevel.Error, "MovieFactory expects four arguments!");
                return null;
            }
            List<Movie> movies;
            try
            {
                movies = (List<Movie>)args[3];
            } catch (InvalidCastException)
            {
                logger.Log(LogLevel.Error, "MovieFactory expects a List<Movie> (param 3) " + _params + "!");
                return null;
            }
            string title;
            try
            {
                title = (string)args[1];
            } catch (InvalidCastException)
            {
                logger.Log(LogLevel.Error, "MovieFactory expects a string (param 1) " + _params + "!");
                return null;
            }
            // Add quotes to the title if it contains a comma.
            if (title.Contains(',') && title.Substring(0, 1) != "\"")
                title = "\"" + title + "\"";
            int nID = -1;
            foreach (Movie movie in movies)
            {
                // find highest movie ID; 
                if (nID < movie.MovieID)
                {
                    nID = movie.MovieID + 1;
                }
                // check if any movies have the same title.
                if (movie.Title != null && movie.Title.Equals(title))
                {
                    logger.Log(LogLevel.Error, "Movie with that title already exists!");
                    return null;
                }
            }
            try
            {
                string id = (string)args[0];
                if (id != "") nID = int.Parse(id);
            } catch (FormatException)
            {
                logger.Log(LogLevel.Error, "ID provided to MovieFactory was not an integer!");
                return null;
            } catch (InvalidCastException)
            {
                logger.Log(LogLevel.Error, "MovieFactory expects an integer (param 0) " + _params + "!");
                return null;
            }
            
            string genres; 
            try
            {
                if (args[2] == null)
                    genres = "(no genres listed)";
                else genres = (string)args[2];
            } catch (InvalidCastException)
            {
                logger.Log(LogLevel.Error, "MovieFactory expects a string (param 2) " + _params + "!");
                return null;
            }
            if (genres == "")
            {
                logger.Log(LogLevel.Error, "No genres provided!");
                return null;
            }
            return new Movie(nID, title, genres);
        }
    }
}
