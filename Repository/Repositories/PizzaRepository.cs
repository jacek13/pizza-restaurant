using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class PizzaRepository : IRepository<Pizza>
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

        public async Task Delete(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                var Pizza = await context.Pizza.FirstOrDefaultAsync(b => b.IdPizza == id);
                if (Pizza != null)
                {
                    context.Remove(Pizza);
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
                lastId++;
                return entity;
            }
        }
    }
}
