using MovieLibrary.Interfaces;
using MovieLibraryEntities.Models;
using System;

namespace MovieLibrary.Implementations.RatingImpl
{
    internal class RatingDisplay : IDisplay<UserMovie>
    {
        private static string TAB = "  ";

        public void Display(UserMovie rating)
        {
            if (rating is null)
            {
                throw new ArgumentNullException("Cannot display a null rating!");
            }

            Console.WriteLine(rating.Id + " =>");
            Console.WriteLine($"{TAB}User Id => {rating.User.Id}");
            Console.WriteLine($"{TAB}Movie Title => {rating.Movie.Title}");
            Console.WriteLine($"{TAB}Rating (1-5) => {rating.Rating}");
            Console.WriteLine($"{TAB}Rated At: {rating.RatedAt}");
        }
    }
}
