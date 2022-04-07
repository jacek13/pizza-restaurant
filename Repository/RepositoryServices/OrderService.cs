using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryServices
{
    internal class OrderService : IRepository<Order, int>
    {
        private readonly pizza_restaurant_ver_6Context _RestaurantContext = null;
        public int lastId { get; set; }

        public OrderService(pizza_restaurant_ver_6Context Restaurant_Ver3Context)
        {
            this._RestaurantContext = Restaurant_Ver3Context;
            lastId = _RestaurantContext.Order.ToList().Count + 1;
        }

        public async Task Delete(int id)
        {
            var Order = await _RestaurantContext.Order.FirstOrDefaultAsync(b => b.IdOrder == id);
            if (Order != null)
            {
                _RestaurantContext.Remove(Order);
            }
        }

        public async Task<List<Order>> GetAll()
        {
            return await _RestaurantContext.Order.ToListAsync();
        }

        public async Task<Order> GetById(int id)
        {
            return await _RestaurantContext.Order.FindAsync(id);
        }

        public async Task<Order> Insert(Order entity)
        {
            await _RestaurantContext.Order.AddAsync(entity);
            lastId++;
            return entity;
        }

        public async Task Save()
        {
            await _RestaurantContext.SaveChangesAsync();
        }
    }
}
