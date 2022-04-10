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
        private readonly IDishesRepository _dishesRepo;

        public DishesService(IDishesRepository dishesRepo)
        {
            _dishesRepo = dishesRepo;
        }
    }
}
