using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseAccess.Models;
using Repository.Repositories;

namespace Repository.Services
{
    public class RestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepo;

        public RestaurantService(IRestaurantRepository restaurantRepo)
        {
            _restaurantRepo = restaurantRepo;
        }

        public async Task<List<Restaurant>> GetRestaurants()
        {
            return await _restaurantRepo.GetAll();
        }

        public async Task<Restaurant> getRestaurantBy(int restaurantID)
        {
            return await _restaurantRepo.GetById(restaurantID);
        }

        public async Task<List<Restaurant>> getAllAssignedRestaurants(int managerID)
        {
            return await _restaurantRepo.fetchAllRestaurantsManagedByManager(managerID);
        }

    }
}
