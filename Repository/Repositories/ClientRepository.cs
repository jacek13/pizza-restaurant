using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{

    public interface IClientRepository
    {
        Task<List<Client>> GetAll();
        Task<Client> GetById(int id);
        Task<Client> Insert(Client entity);

        //Task<T1> Update(T1 entity); TODO
        Task Delete(int id);

        Task<Client> UpdatePoints(int id, int newPoints);
    }



    public class ClientRepository : IClientRepository
    {
        private readonly IDbContextFactory<RestaurantDBContext> _factory;
        public List<int> foreignKeys; // TODO - nie wiem do czego to potrzebne, zostawiam
        public int lastId { get; set; } // nie wiem do czego to potrzebne, zostawiam


        public ClientRepository(IDbContextFactory<RestaurantDBContext> factory)
        {
            this._factory = factory;
            //var tmp = _RestaurantContext.Client.ToList();
            //foreach (var item in tmp)
            //    foreignKeys.Add(item.AccountIdAccount);
        }

        public async Task Delete(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                var Client = await context.Client.FirstOrDefaultAsync(b => b.IdClient == id);
                if (Client != null)
                {
                    context.Remove(Client);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<Client> UpdatePoints(int id, int newPoints)
        {
            using (var context = _factory.CreateDbContext())
            {
                var entity = await context.Client.FirstOrDefaultAsync(b => b.AccountIdAccount == id);
                entity.Points += newPoints;
                context.Client.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<List<Client>> GetAll()
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Client.ToListAsync();
            }
        }

        public async Task<Client> GetById(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Client.FindAsync(id);
            }
        }

        public async Task<Client> Insert(Client entity)
        {
            using (var context = _factory.CreateDbContext())
            {
                await context.AddAsync(entity);
                await context.SaveChangesAsync();
                lastId++;
                return entity;
            }
        }

    }
}
