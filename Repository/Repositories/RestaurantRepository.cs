using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class RestaurantRepository : IRepository<Restaurant>
    {
        private readonly IDbContextFactory<RestaurantDBContext> _factory;
        public int lastId { get; set; }

        public RestaurantRepository(IDbContextFactory<RestaurantDBContext> factory)
        {
            this._factory = factory;
            using (var context = _factory.CreateDbContext())
            {
                lastId = context.Restaurant.ToList().Count + 1;
            }
        }

        public async Task Delete(int id)
        {
            using(var context = _factory.CreateDbContext())
            {
                var Restaurant = await context.Restaurant.FirstOrDefaultAsync(b => b.IdRestaurant == id);
                if (Restaurant != null)
                {
                    context.Remove(Restaurant);
                }
            }
        }

        public async Task<List<Restaurant>> GetAll()
        {
            using(var context = _factory.CreateDbContext())
            {
                return await context.Restaurant.ToListAsync();
            }
        }

        public async Task<Restaurant> GetById(int id)
        {
            using(var context = _factory.CreateDbContext())
            {
                return await context.Restaurant.FindAsync(id);
            }
        }

        public async Task<Restaurant> Insert(Restaurant entity)
        {
            using(var context = _factory.CreateDbContext())
            {
                await context.Restaurant.AddAsync(entity);
                lastId++;
                return entity;
            }
        }
    }
}
