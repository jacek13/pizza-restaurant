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
        private readonly IRepository<Manager> _managerRepo;

        public ManagerService(IRepository<Manager> managerRepo)
        {
            _managerRepo = managerRepo;
        }
    }
}
