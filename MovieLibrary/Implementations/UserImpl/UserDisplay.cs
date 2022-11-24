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
            Console.WriteLine(user.Id + " =>");

            Console.WriteLine($"{TAB}Gender => {user.Gender}");
            Console.WriteLine($"{TAB}Age => {user.Age}");
            Console.WriteLine($"{TAB}Zip Code => {user.ZipCode}");
            Console.WriteLine($"{TAB}Occupation => {user.Occupation}");
        }
    }
}
