using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        public Task<List<Order>> GetUserOrders(int clientId);
        public Task<Order> UpdateOrderStatus(int id, char newStatus);
        public Task<List<Order>> GetRestaurantOrders(int RestaurantId);
    }


    public class OrderRepository : IOrderRepository
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

        public OrderRepository()
        {
        }

        public async Task Delete(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                var Order = await context.Order.FirstOrDefaultAsync(b => b.IdOrder == id);
                if (Order != null)
                {
                    context.Remove(Order);
                    await context.SaveChangesAsync();
                }
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

        public async Task<List<Order>> GetUserOrders(int clientId)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Order.Where(o => o.ClientIdClient == clientId).ToListAsync();
            }
        }


        public async Task<List<Order>> GetRestaurantOrders(int RestaurantId)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Order.Where(o => o.RestaurantIdRestaurant == RestaurantId).ToListAsync();
            }
        }

        public async Task<Order> UpdateOrderStatus(int id, char newStatus)
        {
            using (var context = _factory.CreateDbContext())
            {
                var entity = await context.Order.FirstOrDefaultAsync(o => o.IdOrder == id);
                entity.Status = newStatus.ToString();
                context.Order.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
        public async Task<Order> Insert(Order entity)
        {
            using (var context = _factory.CreateDbContext())
            {
                await context.Order.AddAsync(entity);
                await context.SaveChangesAsync();
                lastId++;
                return entity;
            }
        }

        public Task<Order> Update(Order entity)
        {
            return null;
        }
    }
}
