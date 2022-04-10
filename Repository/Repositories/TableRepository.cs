using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class TableRepository : IRepository<Table>
    {
        private readonly IDbContextFactory<RestaurantDBContext> _factory;
        public int lastId { get; set; }

        public TableRepository(IDbContextFactory<RestaurantDBContext> factory)
        {
            this._factory = factory;
            using (var context = factory.CreateDbContext())
            {
                lastId = context.Table.ToList().Count + 1;
            }
        }

        public async Task Delete(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                var Table = await context.Table.FirstOrDefaultAsync(b => b.IdTable == id);
                if (Table != null)
                {
                    context.Remove(Table);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<List<Table>> GetAll()
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Table.ToListAsync();
            }
        }

        public async Task<Table> GetById(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Table.FindAsync(id);
            }
        }

        public async Task<Table> Insert(Table entity)
        {
            using (var context = _factory.CreateDbContext())
            {
                await context.Table.AddAsync(entity);
                await context.SaveChangesAsync();
                lastId++;
                return entity;
            }
        }
    }
}
