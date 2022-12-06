using Microsoft.EntityFrameworkCore;
using MovieLibrary.Interfaces;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;
using System.Collections.Generic;

namespace MovieLibrary.Implementations.UserImpl
{
    internal class UserInfo
    {
        public int TotalUsers { get; set; }
        public long TotalAge { get; set; }
        public int TotalMales { get; set; }
        public int TotalFemales { get; set; }
        public Dictionary<Occupation, int> Occupations { get; set; }
        public UserInfo() {
            Occupations = new Dictionary<Occupation, int>();
        }
        public float GetAverageAge()
        {
            return (float)TotalAge / (float)TotalUsers; 
        }
        public Occupation GetMostPopularOccupation()
        {
            Occupation mostPopular = null;
            int numberOfUsers = 0;
            foreach (var occupation in Occupations.Keys)
            {
                if (Occupations[occupation] > numberOfUsers)
                {
                    mostPopular = occupation; 
                    numberOfUsers = Occupations[occupation];
                }
            }
            return mostPopular;
        }
    }

    internal class UserSorter : ISorter<User>
    {
        public List<object> Sort(params object[] args)
        {
            var userInfo = new UserInfo();
            using (var ctx = new MovieContext())
            {
                foreach (var user in ctx.Users.Include("Occupation"))
                {
                    userInfo.TotalUsers++;
                    userInfo.TotalAge += user.Age;
                    if (!userInfo.Occupations.ContainsKey(user.Occupation))
                        userInfo.Occupations[user.Occupation] = 0;
                    userInfo.Occupations[user.Occupation]++;
                    if (user.Gender == "M")
                        userInfo.TotalMales++;
                    else userInfo.TotalFemales++;
                }
            }
            return new List<object>() { (object)userInfo  };
        }
    }
}
