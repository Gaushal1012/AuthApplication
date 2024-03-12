using AuthApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApplication.Services
{
    public class UserService : IUserService
    {
        // This would typically be a database context or some other data access mechanism.
        private readonly List<User> _users = new List<User>
        {
            new User { Id = 1, Username = "admin", Password = "admin", Role = "Admin" },
            new User { Id = 2, Username = "user", Password = "user", Role = "User" }
            // Add more users as needed...
        };

        public User Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(u => u.Username == username && u.Password == password);
            return user;
        }
    }
}
