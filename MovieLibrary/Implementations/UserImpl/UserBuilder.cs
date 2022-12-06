using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieLibrary.Interfaces;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;
using System;
using System.Linq;
using System.Threading;

namespace MovieLibrary.Implementations.UserImpl
{
    internal class UserBuilder : IBuilder<User>
    {
        public UserBuilder()
        {

        }

        public User Build(params object[] args)
        {
            var user = new User();
            
            if (args.Length < 4)
            {
                throw new ArgumentNullException("The user builder expects three arguments!");
            }

            var genderStr = args[0].ToString();
            if (string.IsNullOrEmpty(genderStr))
            {
                throw new ArgumentNullException("The gender of the new user cannot be blank/null!");
            }
            switch (genderStr.ToLower())
            {
                case "male":
                case "m":
                    user.Gender = "M";
                    break;
                case "female":
                case "f":
                    user.Gender = "F";
                    break;
                default:
                    throw new ArgumentException("Expecting either male or female gender.");
            }

            var ageStr = args[1].ToString();
            if (string.IsNullOrEmpty(ageStr))
            {
                throw new ArgumentNullException("The age of the new user cannot be blank/null!");
            }
            try
            {
                var age = int.Parse(ageStr);
                user.Age = age;
            } catch (FormatException)
            {
                throw new ArgumentException("The new user's age must be an integer!");
            }

            var zipCodeStr = args[2].ToString();
            if (string.IsNullOrEmpty(zipCodeStr))
            {
                throw new ArgumentNullException("The zip code of the new user cannot be blank/null!");
            }
            user.ZipCode = zipCodeStr;

            var occupationStr = args[3].ToString();
            if (string.IsNullOrEmpty(occupationStr))
            {
                throw new ArgumentNullException("The occupation of the new user cannot be blank/null!");
            }

            using (var ctx = new MovieContext())
            {
                var occupation = ctx.Occupations
                    .FirstOrDefault(x => x.Name.StartsWith(occupationStr));

                if (occupation is null)
                {
                    throw new ArgumentException(
                        $"Could not find any occupations that match {occupationStr}!");
                }

                user.Occupation = occupation;

                ctx.Add(user);
                ctx.SaveChanges();

                return ctx.Users
                    .Include("Occupation")
                    .FirstOrDefault(x => x.Id == user.Id);
            }
        }
    }
}
