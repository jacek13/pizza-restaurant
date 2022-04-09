using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using DataBaseAccess.Models;

namespace Repository.Services
{
    public class ClientService
    {
        private readonly IRepository<Client> _clientRepo;

        public ClientService(IRepository<Client> accountRepo)
        {
            _clientRepo = accountRepo;
        }
    }
}
