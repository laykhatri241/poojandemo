using MWTDbContext.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MWTCore.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<int> CreateUser(User usr);

        public Task<bool> checkUsername(string username);

        public Task<User> UserExists(string Username, string Password);

    }
}
