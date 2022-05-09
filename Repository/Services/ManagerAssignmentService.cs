using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using DataBaseAccess.Models;

namespace Repository.Services
{
    public class ManagerAssignmentService
    {
        private readonly IManagerAssignmentRepository _managerAssignRepo;

        public ManagerAssignmentService(IManagerAssignmentRepository managerAssignRepo)
        {
            _managerAssignRepo = managerAssignRepo;
        }

        public Task<List<ManagerAssignment>> GetManagerAssigment()
        {
            return _managerAssignRepo.GetAll();
        }

    }
}
