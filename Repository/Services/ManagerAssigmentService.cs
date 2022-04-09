using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using DataBaseAccess.Models;

namespace Repository.Services
{
    public class ManagerAssigmentService
    {
        private readonly IRepository<ManagerAssignment> _managerAssignRepo;

        public ManagerAssigmentService(IRepository<ManagerAssignment> managerAssignRepo)
        {
            _managerAssignRepo = managerAssignRepo;
        }
    }
}
