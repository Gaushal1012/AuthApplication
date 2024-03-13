using AuthApplication.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApplication.Services
{
    public class UserService : IUserService
    {
        // This would typically be a database context or some other data access mechanism.
        //private readonly List<User> _users = new List<User>
        //{
        //    new User { Id = 1, Username = "admin", Password = "admin", Role = "Admin" },
        //    new User { Id = 2, Username = "user", Password = "user", Role = "User" }
        //    // Add more users as needed...
        //};

        private string connectionString = "Data Source=syn-sql-01; Initial Catalog=gaushal.dholiya; User ID=gaushal.dholiya; Password=Gaushal@!@#$%^; Timeout=120";
        public static List<User> userList = new List<User>();

        public List<User> GetAllUsers()
        {
            SqlConnection con = new SqlConnection(connectionString);
            string sqlQuery = "SELECT * FROM [dbo].[Users]";
            SqlCommand sqlcomm = new SqlCommand(sqlQuery, con);

            con.Open();
            SqlDataReader reader = sqlcomm.ExecuteReader();

            // Clear the list before populating it with new data
            userList.Clear();

            while (reader.Read())
            {
                User project = new User
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Username = reader["Username"].ToString(),
                    Password = reader["Password"].ToString(),
                    Role = reader["Role"].ToString()
                };
                userList.Add(project);
            }

            con.Close();
            return userList;
        }

        public User Authenticate(string username, string password)
        {
            //Function call
            GetAllUsers();

            byte[] bytesToEncode = System.Text.Encoding.UTF8.GetBytes(password);
            string encodedString = Convert.ToBase64String(bytesToEncode);

            var user = userList.Find(u => u.Username == username && u.Password== encodedString);
            return user;
        }
    }
}
