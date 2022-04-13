using Microsoft.EntityFrameworkCore;
using MWTCore.Repository.Interfaces;
using MWTDbContext;
using MWTDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWTCore.Repository
{
    public class AccountRepository: IAccountRepository
    {
        private readonly StoreAppDbCon _context;
        public AccountRepository(StoreAppDbCon context)
        {
            _context = context;
        }

        public async Task<int> InsertUser(User usr)
        {
            _context.users.Add(usr);
            return await _context.SaveChangesAsync();

        }

        public async Task<User> IsUser(string Username, string Password)
        {
            return await (_context.users.FirstOrDefaultAsync(u => u.Username.Equals(Username) && u.Password.Equals(Password)));
        }

        public async Task<bool> RetrieveUsername(string username)
        {
            return await (_context.users.AnyAsync(usr => usr.Username.Equals(username)));
        }
    }
}
