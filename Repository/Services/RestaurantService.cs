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
    }
}
