using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//TODO
namespace Repository.RepositoryServices
{
    internal class DishesService : IRepository<Dishes, int>
    {
        private readonly pizza_restaurant_ver_5Context _RestaurantContext = null;
        public int lastId { get; set; }
        // TODO Można też dodać parametr size

        public DishesService(pizza_restaurant_ver_5Context Restaurant_Ver3Context)
        {
            this._RestaurantContext = Restaurant_Ver3Context;
            this.lastId = _RestaurantContext.Dishes.ToList().Count + 1;
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException("W naszym projekcie historyczne zamówienia mają być zapisywane!");
            //var Dishes = await _RestaurantContext.Dishes.FirstOrDefaultAsync(b => b.IdDishes == id);
            //if (Dishes != null)
            //{
            //    _RestaurantContext.Remove(Dishes);
            //}
        }

        public async Task<List<Dishes>> GetAll()
        {
            return await _RestaurantContext.Dishes.ToListAsync();
        }

        // Test bez async i await
        public List<Dishes> GetAllTest()
        {
            return _RestaurantContext.Dishes.ToList();
        }

        public async Task<Dishes> GetById(int id)
        {
            return await _RestaurantContext.Dishes.FindAsync(id);
        }

        public async Task<Dishes> Insert(Dishes entity)
        {
            await _RestaurantContext.Dishes.AddAsync(entity);
            lastId++;
            return entity;
        }

        public async Task Save()
        {
            await _RestaurantContext.SaveChangesAsync();
        }
    }
}
