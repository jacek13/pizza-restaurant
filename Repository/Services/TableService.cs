using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using DataBaseAccess.Models;

namespace Repository.Services
{
    public class TableService
    {
        private readonly IRepository<Table> _TableRepo;

        public TableService(IRepository<Table> TableRepo)
        {
            _TableRepo = TableRepo;
        }
    }
}
