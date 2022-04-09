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
        private readonly IRepository<Account> _accountRepo;

        public AccountService(IRepository<Account> accountRepo)
        {
            _accountRepo = accountRepo;
        }
    }
}
