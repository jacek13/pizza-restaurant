using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IManagerRepository : IRepository<Manager>
    {
        Task<Manager> fetchManagerByAccountId(int accountID);
    }

    public class ManagerRepository : IManagerRepository
    {
        private readonly IDbContextFactory<RestaurantDBContext> _factory;
        public List<int> foreignKeys; // TODO 
        public int lastId { get; set; }
        public ManagerRepository(IDbContextFactory<RestaurantDBContext> factory)
        {
            this._factory = factory;
            //var tmp = _RestaurantContext.Manager.ToList();
            //foreach (var item in tmp)
            //    foreignKeys.Add(item.AccountIdAccount);
        }

        public async Task Delete(int id)
        {
            using(var context = _factory.CreateDbContext())
            {
                var Manager = await context.Manager.FirstOrDefaultAsync(b => b.IdManager == id);
                if (Manager != null)
                {
                    context.Remove(Manager);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<List<Manager>> GetAll()
        {
            using(var context = _factory.CreateDbContext())
            {
                return await context.Manager.ToListAsync();
            }
        }

        public async Task<Manager> GetById(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Manager.FindAsync(id);
            }
        }

        public async Task<Manager> Insert(Manager entity)
        {
            using (var context = _factory.CreateDbContext())
            {
                await context.Manager.AddAsync(entity);
                await context.SaveChangesAsync();
                lastId++;
                return entity;
            }
        }

        public async Task<Manager> fetchManagerByAccountId(int accountID)
        {
            using (var context = _factory.CreateDbContext())
            {
                Manager manager = context.Manager
                    .Where(manager => manager.AccountIdAccount == accountID)
                    .FirstOrDefault<Manager>();
                return await Task.FromResult(manager);
            }
        }

        public Task<Manager> Update(Manager entity)
        {
            throw new NotImplementedException();
        }
    }
}
