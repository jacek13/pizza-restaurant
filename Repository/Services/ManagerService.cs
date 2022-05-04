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

        public async Task<Manager> GetManagerByAccountId(int accountId)
        {
            var managers = await _managerRepo.GetAll();
            var managerTmp = new Manager();
            bool found = false;

            foreach (var manager in managers)
                if (manager.AccountIdAccount == accountId)
                {
                    managerTmp = manager;
                    found = true;
                    break;
                }
            if (found)
                return managerTmp;
            else throw new Exception("Manager not found!");
        }


    }
    

}
