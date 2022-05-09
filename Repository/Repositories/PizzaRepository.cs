using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IPizzaRepository : IRepository<Pizza>
    {
        Task<Pizza> UpdatePizzaAvailability(int id, bool isAvailable);
    }

    public class PizzaRepository : IPizzaRepository
    {
        private readonly IDbContextFactory<RestaurantDBContext> _factory;
        public int lastId { get; set; }

        public PizzaRepository(IDbContextFactory<RestaurantDBContext> factory)
        {
            this._factory = factory;
            using(var context = factory.CreateDbContext())
            {
                lastId = context.Pizza.ToList().Count + 1;
            }
        }

        public PizzaRepository()
        {
        }

        public async Task Delete(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                var Pizza = await context.Pizza.FirstOrDefaultAsync(b => b.IdPizza == id);
                if (Pizza != null)
                {
                    context.Remove(Pizza);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<Pizza> UpdatePizzaAvailability(int id, bool isAvailable)
        {
            using (var context = _factory.CreateDbContext())
            {
                var entity = await context.Pizza.FirstOrDefaultAsync(b => b.IdPizza == id);
                entity.IsAvailable = isAvailable;
                context.Pizza.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<List<Pizza>> GetAll()
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Pizza.ToListAsync();
            }
        }

        public async Task<Pizza> GetById(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Pizza.FindAsync(id);
            }
        }

        public async Task<Pizza> Insert(Pizza entity)
        {
            using (var context = _factory.CreateDbContext())
            {
                await context.Pizza.AddAsync(entity);
                await context.SaveChangesAsync();
                lastId++;
                return entity;
            }
        }
        public async Task<Pizza> Update(Pizza pizza)
        {
            using (var context = _factory.CreateDbContext())
            {
                context.Pizza.Update(pizza);
                await context.SaveChangesAsync();
                return pizza;
            }
        }
    }
}
