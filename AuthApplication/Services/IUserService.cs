using AuthApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApplication.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
    }
}
