using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using DataBaseAccess.Models;

namespace Repository.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepo;

        public AccountService(IAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public Task<List<Account>> GetAccounts()
        {
            return _accountRepo.GetAll();
        }

        public async Task<Account> GetAccountByLoginAndPassword(string login, string password)
        {
            var accounts = await _accountRepo.GetAll();
            var accountTmp = new Account();
            bool found = false;

            foreach (var account in accounts)
                if ((account.Login == login || account.EMail == login) && account.Password == password)
                {
                    accountTmp = account;
                    found = true;
                    break;
                }
            if (found)
                return accountTmp;
            else throw new Exception("Incorrect login or password");
        }

    }
}
