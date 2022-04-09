using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using DataBaseAccess.Models;

namespace Repository.Services
{
    public class DishesService
    {
        private readonly IRepository<Dishes> _dishesRepo;

        public DishesService(IRepository<Dishes> dishesRepo)
        {
            _dishesRepo = dishesRepo;
        }
    }
}
