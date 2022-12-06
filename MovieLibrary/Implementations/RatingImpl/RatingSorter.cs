using Microsoft.EntityFrameworkCore;
using MovieLibrary.Interfaces;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieLibrary.Implementations.RatingImpl
{
    internal class RatingInfo
    {
        public RatingInfo() {

        }
        public long MovieId { get; set; }
        public string MovieTitle { get; set; }
        public string OccupationName { get; set; }
        public long TotalRating { get; set; }
        public int NumberOfRatings { get; set; }
        public float AverageRating { get; set; }
    }
    internal class RatingSorter : ISorter<UserMovie>
    {
        public List<object> Sort(params object[] args)
        {
            if (args[0] == null || args[0] is not Occupation)
            {
                throw new ArgumentNullException("Cannot sort ratings with an invalid occupation!");
            }

            var occupation = (Occupation) args[0];

            var ratings = new List<RatingInfo>();
            using (var ctx = new MovieContext())
            {
                var userMovies = ctx.UserMovies
                    .Include("Movie")
                    .Include("User.Occupation")
                    .Where(x => x.User.Occupation.Name == occupation.Name);
                foreach (var userMovie in userMovies)
                {
                    var ratingInfo = ratings.Find(x => x.MovieId == userMovie.Movie.Id);
                    if (ratingInfo is null)
                    {
                        ratingInfo = new RatingInfo();
                        ratingInfo.MovieId = userMovie.Movie.Id;
                        ratingInfo.MovieTitle = userMovie.Movie.Title;
                        ratingInfo.OccupationName = userMovie.User.Occupation.Name;
                        ratingInfo.TotalRating = userMovie.Rating;
                        ratingInfo.NumberOfRatings = 1;
                        ratingInfo.AverageRating = userMovie.Rating;
                        ratings.Add(ratingInfo);
                    } else
                    {
                        ratingInfo.TotalRating += userMovie.Rating;
                        ratingInfo.NumberOfRatings += 1;
                        ratingInfo.AverageRating = 
                            (float)ratingInfo.TotalRating / (float)ratingInfo.NumberOfRatings;
                    }
                }
            }
            // sort by total rating, then by movie title
            return ratings.OrderByDescending(x => x.TotalRating)
                .ThenBy(x => x.MovieTitle)
                .Cast<object>()
                .ToList();
        }
    }
}
