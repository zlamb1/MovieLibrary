using MovieLibrary.Interfaces;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;
using System;
using System.Linq;

namespace MovieLibrary.Implementations.UserMovieImpl
{
    internal class RatingBuilder : IBuilder<UserMovie>
    {
        public UserMovie Build(params object[] args)
        {
            if (args[0] == null || args[0] is not User)
            {
                throw new ArgumentException("Cannot build a rating with an invalid user!");
            }

            if (args[1] == null || args[1] is not Movie)
            {
                throw new ArgumentException("Cannot build a rating with an invalid movie!");
            }

            var userMovie = new UserMovie();
            userMovie.User = (User)args[0];
            userMovie.Movie = (Movie)args[1];

            try
            {
                var rating = int.Parse(args[2].ToString());
                if (rating < 0 || rating > 5)
                {
                    throw new ArgumentException("The rating must be between 1 and 5!");
                }
                userMovie.Rating = rating;
            }
            catch (FormatException)
            {
                throw new ArgumentException("The rating must be an integer!");
            }

            userMovie.RatedAt = DateTime.Now;
    
            using (var ctx = new MovieContext())
            {
                // search for hot database User and Movie
                userMovie.User = ctx.Users
                    .FirstOrDefault(x => x.Id == userMovie.User.Id);
                userMovie.Movie = ctx.Movies
                    .FirstOrDefault(x => x.Id == userMovie.Movie.Id);
                // add rating to database and save changes
                ctx.Add(userMovie);
                ctx.SaveChanges();
            }

            return userMovie;
        }
    }
}
