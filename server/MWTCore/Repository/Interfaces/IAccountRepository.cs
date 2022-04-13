using MWTDbContext.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MWTCore.Repository.Interfaces
{
    public interface IAccountRepository
    {
        public Task<int> InsertUser(User usr);

        public Task<bool> RetrieveUsername(string username);

        public Task<User> IsUser(string Username, string Password);
    }
}
