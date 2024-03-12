using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthApplication.Models;
using AuthApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthApplication.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    //[AllowAnonymous]
    [ApiController]
    public class UserController : ControllerBase
    {
        //public UserService _userService;
        //public UserController(UserService userService)
        //{
        //    _userService = userService;
        //}

        [HttpPost]
        [AllowAnonymous]
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
    }
}