using Microsoft.Extensions.Logging;
using MovieLibrary.Implementations.RatingImpl;
using MovieLibrary.Interfaces;
using MovieLibrary.utility;
using MovieLibraryEntities.Models;
using System;
using System.Linq;

namespace MovieLibrary.Menus.UserMenus
{
    internal class SortMenu : Menu
    {
        private readonly IFinder<Occupation> occupationFinder;
        private readonly ISorter<UserMovie> ratingSorter;

        public SortMenu(IFinder<Occupation> _occupationFinder,
            ISorter<UserMovie> _ratingSorter,
            ILogger<IMenu> logger) : base(logger) {
            occupationFinder = _occupationFinder;
            ratingSorter = _ratingSorter;
        }

        public override void Start()
        {
            base.Start();

            Console.WriteLine("Available occupations =>");

            // find all occupations by searching with an empty string
            var allOccupations = occupationFinder.Find("");
            foreach (var occupation in allOccupations)
            {
                Console.WriteLine($"\t=> {occupation.Name}");
            }
            Console.WriteLine();

            // make sure inputted occupation is valid
            var occupationInput = InputUtility.GetStringWithPrompt(
                "Which occupation would you like to sort reviews by?\n");
            var foundOccupation = occupationFinder.First(occupationInput);
            if (foundOccupation is null)
            {
                Restart($"{occupationInput} is not a valid occupation!");
                return;
            }
            Console.WriteLine();

            try
            {
                var sortedRatings = ratingSorter.Sort(foundOccupation);
                for (int i = 0; i < Math.Min(1, sortedRatings.Count()); i++)
                {
                    var rating = (RatingInfo) sortedRatings[i];
                    Console.WriteLine($"Top Rated Movie => {rating.MovieTitle}");
                    Console.WriteLine($"Occupation => {rating.OccupationName}");
                    Console.WriteLine($"Total Rating => {rating.TotalRating}");
                    Console.WriteLine($"Number of Ratings => {rating.NumberOfRatings}");
                    Console.WriteLine($"Average Rating => {rating.AverageRating}/5");
                    Console.WriteLine();
                }
            } catch (Exception exc)
            {
                LogError(exc.Message);
                Console.WriteLine(exc.Message);
            }

            WaitForInput();
        }

    }
}
