using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using DataBaseAccess.Models;


namespace Repository.Services
{
    public class PizzaService
    {
        private readonly IRepository<Pizza> _pizzaRepo;

        public PizzaService(IRepository<Pizza> pizzaRepo)
        {
            _pizzaRepo = pizzaRepo;
        }

        public Task<List<Pizza>> GetMenu()
        {
            return _pizzaRepo.GetAll();
        }
    }
}
