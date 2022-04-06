using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryServices
{
    internal class RestaurantService : IRepository<Restaurant, int>
    {
        private readonly pizza_restaurant_ver_5Context _RestaurantContext = null;
        public int lastId { get; set; }

        public RestaurantService(pizza_restaurant_ver_5Context Restaurant_Ver3Context)
        {
            this._RestaurantContext = Restaurant_Ver3Context;
            lastId = _RestaurantContext.Restaurant.ToList().Count + 1;
        }

        public async Task Delete(int id)
        {
            var Restaurant = await _RestaurantContext.Restaurant.FirstOrDefaultAsync(b => b.IdRestaurant == id);
            if (Restaurant != null)
            {
                _RestaurantContext.Remove(Restaurant);
            }
        }

        public async Task<List<Restaurant>> GetAll()
        {
            return await _RestaurantContext.Restaurant.ToListAsync();
        }

        public async Task<Restaurant> GetById(int id)
        {
            return await _RestaurantContext.Restaurant.FindAsync(id);
        }

        public async Task<Restaurant> Insert(Restaurant entity)
        {
            await _RestaurantContext.Restaurant.AddAsync(entity);
            lastId++;
            return entity;
        }

        public async Task Save()
        {
            await _RestaurantContext.SaveChangesAsync();
        }
    }
}
