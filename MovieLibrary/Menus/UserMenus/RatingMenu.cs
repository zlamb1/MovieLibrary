using Microsoft.Extensions.Logging;
using MovieLibrary.Interfaces;
using MovieLibrary.utility;
using MovieLibraryEntities.Models;
using System;

namespace MovieLibrary.Menus.UserMenus
{
    internal class RatingMenu : Menu
    {
        private IFinder<Movie> movieFinder;
        private IDisplay<Movie> movieDisplay;

        private IFinder<User> userFinder;
        private IDisplay<User> userDisplay;

        private IBuilder<UserMovie> ratingBuilder;
        private IDisplay<UserMovie> ratingDisplay;

        public RatingMenu(IFinder<Movie> _movieFinder, IDisplay<Movie> _movieDisplay,
                          IFinder<User> _userFinder, IDisplay<User> _userDisplay,
                          IBuilder<UserMovie> _ratingBuilder, 
                          IDisplay<UserMovie> _ratingDisplay,
                          ILogger<IMenu> _logger) : base(_logger)
        {
            movieFinder = _movieFinder;
            movieDisplay = _movieDisplay;

            userFinder = _userFinder;
            userDisplay = _userDisplay; 

            ratingBuilder = _ratingBuilder;
            ratingDisplay = _ratingDisplay;
        }
        public override void Start()
        {
            base.Start();

            string userId = InputUtility.GetStringWithPrompt(
                "Please enter your user id.\n");

            User user = null;
            try
            {
                user = userFinder.First(userId);
                Console.WriteLine();

                if (user is null)
                {
                    Restart($"Could not find a user with the ID => {userId}");
                    return;
                }

                userDisplay.Display(user);
                Console.WriteLine();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }

            string movieTitle = InputUtility.GetStringWithPrompt(
                "What is the title of the movie you want to rate?\n");

            Movie movie = null;
            try
            {
                movie = movieFinder.First(movieTitle);
                Console.WriteLine();

                if (movie is null)
                {
                    Restart($"Could not find a movie with the title => {movieTitle}");
                    return;
                }
                movieDisplay.Display(movie);
                Console.WriteLine();

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }

            string rating = InputUtility.GetStringWithPrompt(
                "What is your rating of the movie? (1 - 5)\n");
            Console.WriteLine();

            try
            {
                var _rating = ratingBuilder.Build(user, movie, rating);
                ratingDisplay.Display(_rating);
            } catch (Exception exc) {
                Console.WriteLine(exc.Message);
            }

            Console.WriteLine();
            WaitForInput();
        }
    }
}
