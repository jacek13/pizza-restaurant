using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseAccess.Models;
using Repository.Repositories;

namespace Repository.Services
{
    public class RestaurantReportService
    {
        private readonly IRestaurantRepository _restaurantRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IDishesRepository _dishesRepo;
        private readonly IPizzaRepository _pizzaRepo;

        public RestaurantReportService(IRestaurantRepository restaurantRepo,
            IOrderRepository orderRepo,
            IDishesRepository dishesRepo,
            IPizzaRepository pizzaRepo)
        {
            _restaurantRepo = restaurantRepo;
            _orderRepo = orderRepo;
            _dishesRepo = dishesRepo;  
            _pizzaRepo = pizzaRepo; 
        }

    }
}
