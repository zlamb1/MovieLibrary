using Microsoft.EntityFrameworkCore.Update;
using MovieLibrary.Interfaces;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieLibrary.Implementations.UserImpl
{
    internal class UserFinder : IFinder<User>
    {
        public User First(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("The user ID cannot be null!");
            using (var ctx = new MovieContext())
            {
                return ctx.Users
                    .FirstOrDefault(x => x.Id == int.Parse(id));
            }
        }

        public List<User> Find(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("The user ID cannot be null!");
            using (var ctx = new MovieContext())
            {
                return ctx.Users
                    .Where(x => x.Id == int.Parse(id))
                    .ToList();
            }
        }
    }
}
