using MWTCore.Repository.Interfaces;
using MWTCore.Services.Interfaces;
using MWTDbContext.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MWTCore.Services
{
    public class AccountService: IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<bool> checkUsername(string username)
        {
            return await _accountRepository.RetrieveUsername(username);
        }

        public async Task<int> CreateUser(User usr)
        {
            return await _accountRepository.InsertUser(usr);
        }      

        public async Task<User> UserExists(string Username, string Password)
        {
            return await _accountRepository.IsUser(Username, Password);
        }
    }
}
