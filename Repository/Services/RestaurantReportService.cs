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


        public async Task<List<Tuple<Dishes, DateOnly>>> GetRestaurantDishes(int restaurantId)
        {
            
            List<Tuple<Dishes, DateOnly>> restDishes = new List<Tuple<Dishes, DateOnly>>();

            try
            {
                 var restOrders = await _orderRepo.GetRestaurantOrders(restaurantId);
                

                if (restOrders is null)
                    throw new Exception("Orders are NULL");
                foreach (var restOrder in restOrders)
                {
                    var tmp = await _dishesRepo.GetOrderDishes(restOrder.IdOrder);
                    foreach (var tmp1 in tmp)
                        restDishes.Add(new Tuple<Dishes, DateOnly>(tmp1, DateOnly.FromDateTime(restOrder.Date)));
                   
                }
                return restDishes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Tuple<Dishes, DateOnly, int>>> GetDishes()
        {

            List<Tuple<Dishes, DateOnly, int>> restDishes = new List<Tuple<Dishes, DateOnly, int>>();

            try
            {
                
                var restOrders = await _orderRepo.GetAll();

                if (restOrders is null)
                    throw new Exception("Orders are NULL");
                foreach (var restOrder in restOrders)
                {
                    var tmp = await _dishesRepo.GetOrderDishes(restOrder.IdOrder);
                    foreach (var tmp1 in tmp)
                        restDishes.Add(new Tuple<Dishes, DateOnly, int>(tmp1, DateOnly.FromDateTime(restOrder.Date), restOrder.RestaurantIdRestaurant.GetValueOrDefault()));

                }
                return restDishes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }




    }
}
