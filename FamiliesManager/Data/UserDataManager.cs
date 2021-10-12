using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace FamilyManager.Data
{
    public class UserDataManager : IUserService
    {
        private List<User> users;

        public UserDataManager()
        {
            users = new[]
            {
                new User
                {
                    Id = 1,
                    Username = "Luis",
                    Password = "1234"
                    
                },
            }.ToList();
        }

        public User ValidateUser(string userName, string password)
        {
            User first = users.FirstOrDefault(user => user.Username.Equals(userName));
            if (first == null)
            {
                throw new Exception("User not found");
            }

            if (!first.Password.Equals(password))
            {
                throw new Exception("Incorrect password");
            }

            return first;
        }
    }
}