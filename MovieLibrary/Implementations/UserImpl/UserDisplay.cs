using MovieLibrary.Interfaces;
using MovieLibraryEntities.Models;
using System;

namespace MovieLibrary.Implementations.UserImpl
{
    internal class UserDisplay : IDisplay<User>
    {
        private static string TAB = "  ";
        public void Display(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("Cannot display a null user!");
            }

            Console.WriteLine($"User Id => {user.Id}");

            Console.WriteLine($"{TAB}Gender => {user.Gender}");
            Console.WriteLine($"{TAB}Age => {user.Age}");
            Console.WriteLine($"{TAB}Zip Code => {user.ZipCode}");
            if (user.Occupation != null)
            {
                Console.WriteLine($"{TAB}Occupation => {user.Occupation.Name}");
            } else
            {
                Console.WriteLine($"{TAB}Occupation => N/A");
            }
        }
    }
}
