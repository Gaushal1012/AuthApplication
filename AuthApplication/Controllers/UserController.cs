using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using AuthApplication.Entities;
using AuthApplication.Models;
using AuthApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApplication.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    //[AllowAnonymous]
    [ApiController]
    public class UserController : ControllerBase
    {
        private string connectionString = "Data Source=syn-sql-01; Initial Catalog=gaushal.dholiya; User ID=gaushal.dholiya; Password=Gaushal@!@#$%^; Timeout=120";
        public static List<User> userList = new List<User>();

        [HttpPost]
        //[AllowAnonymous]
        [Route("/add")]
        public IActionResult Authenticate(AuthenticateModel model)
        {
            if (model == null)
                return BadRequest(new { message = "username or password is incorrect" });

            return Ok(model);
        }

        [HttpGet]
        [Route("/auth")]
        public IActionResult AdminOnly()
        {
            return Ok("Admin Only Content");
        }

        [Route("user/adduser")]
        //[AllowAnonymous]
        [HttpPost]
        public void AddUser(User user)
        {
            // Implement SQL insert operation here
            SqlConnection con = new SqlConnection(connectionString);
            string sqlQuery = "INSERT INTO [dbo].[Users] (Id, Username, Password, Role) VALUES (@Id, @Username, @Password, @Role)";
            SqlCommand sqlcomm = new SqlCommand(sqlQuery, con);
            con.Open();

            byte[] bytesToEncode = System.Text.Encoding.UTF8.GetBytes(user.Password);
            string encodedString = Convert.ToBase64String(bytesToEncode);

            sqlcomm.Parameters.AddWithValue("@Id", user.Id);
            sqlcomm.Parameters.AddWithValue("@Username", user.Username);
            sqlcomm.Parameters.AddWithValue("@Password", encodedString);
            sqlcomm.Parameters.AddWithValue("@Role", user.Role);

            sqlcomm.ExecuteNonQuery();
            con.Close();
        }

        [Route("/allusers")]
        [HttpGet]
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
    }
}