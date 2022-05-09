using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using DataBaseAccess.Models;

namespace Repository.Services
{
    public class ManagerService
    {
        private readonly IManagerRepository _managerRepo;

        public ManagerService(IManagerRepository managerRepo)
        {
            _managerRepo = managerRepo;
        }

        public async Task<Manager> getManagerFromAccountID(int accountID)
        {
            return await _managerRepo.fetchManagerByAccountId(accountID);
        }
    }
}
