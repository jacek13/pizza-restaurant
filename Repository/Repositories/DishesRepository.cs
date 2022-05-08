using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//TODO
namespace Repository.Repositories
{
    public interface IDishesRepository : IRepository<Dishes>
    {
        public Task<List<Dishes>> GetOrderDishes(int orderId);
    }

    public class DishesRepository : IDishesRepository
    {
        private readonly IDbContextFactory<RestaurantDBContext> _factory;
        public int lastId { get; set; } 
        // TODO Można też dodać parametr size

        public DishesRepository(IDbContextFactory<RestaurantDBContext> factory)
        {
            this._factory = factory;
            using (var context = factory.CreateDbContext())
            {
                this.lastId = context.Dishes.ToList().Count + 1;
            }
        }

        public DishesRepository()
        {
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
            using(var context = _factory.CreateDbContext())
            {
                return await context.Dishes.ToListAsync();
            }
        }
                
        public async Task<Dishes> GetById(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Dishes.FindAsync(id);
            }
        }

        public async Task<List<Dishes>> GetOrderDishes(int orderId)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Dishes.Where(d => d.OrderIdOrder == orderId).ToListAsync();
            }
        }

        public async Task<Dishes> Insert(Dishes entity)
        {
            using (var context = _factory.CreateDbContext())
            {
                await context.Dishes.AddAsync(entity);
                await context.SaveChangesAsync();
                lastId++;
                return entity;
            }
        }

        public Task<Dishes> Update(Dishes entity)
        {
            return null;
        }
    }
}
