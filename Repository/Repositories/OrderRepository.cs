using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly IDbContextFactory<RestaurantDBContext> _factory;
        public int lastId { get; set; }

        public OrderRepository(IDbContextFactory<RestaurantDBContext> factory)
        {
            this._factory = factory;
            using (var context = factory.CreateDbContext())
            {
                lastId = context.Order.ToList().Count + 1;
            }
        }

        public async Task Delete(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                var Order = await context.Order.FirstOrDefaultAsync(b => b.IdOrder == id);
                if (Order != null)
                    context.Remove(Order);
            }
        }

        public async Task<List<Order>> GetAll()
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Order.ToListAsync();
            }
        }

        public async Task<Order> GetById(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Order.FindAsync(id);
            }
        }

        public async Task<Order> Insert(Order entity)
        {
            using (var context = _factory.CreateDbContext())
            {
                await context.AddAsync(entity);
                lastId++;
                return entity;
            }
        }
    }
}
