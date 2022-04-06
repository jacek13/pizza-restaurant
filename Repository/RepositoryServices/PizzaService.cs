using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryServices
{
    internal class PizzaService : IRepository<Pizza, int>
    {
        private readonly pizza_restaurant_ver_5Context _RestaurantContext = null;
        public int lastId { get; set; }

        public PizzaService(pizza_restaurant_ver_5Context Restaurant_Ver3Context)
        {
            this._RestaurantContext = Restaurant_Ver3Context;
            lastId = _RestaurantContext.Pizza.ToList().Count + 1;
        }

        public async Task Delete(int id)
        {
            var Pizza = await _RestaurantContext.Pizza.FirstOrDefaultAsync(b => b.IdPizza == id);
            if (Pizza != null)
            {
                _RestaurantContext.Remove(Pizza);
            }
        }

        public async Task<Pizza> UpdatePizzaAvailability(int id, bool isAvailable)
        {
            var entity = await _RestaurantContext.Pizza.FirstOrDefaultAsync(b => b.IdPizza == id);
            entity.IsAvailable = isAvailable;
            _RestaurantContext.Pizza.Update(entity);
            return entity;
        }

        public async Task<List<Pizza>> GetAll()
        {
            return await _RestaurantContext.Pizza.ToListAsync();
        }

        public async Task<Pizza> GetById(int id)
        {
            return await _RestaurantContext.Pizza.FindAsync(id);
        }

        public async Task<Pizza> Insert(Pizza entity)
        {
            await _RestaurantContext.Pizza.AddAsync(entity);
            lastId++;
            return entity;
        }

        public async Task Save()
        {
            await _RestaurantContext.SaveChangesAsync();
        }
    }
}
