using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryServices
{
    internal class ClientService : IRepository<Client, int>
    {
        private readonly pizza_restaurant_ver_5Context _RestaurantContext = null;
        public int lastId { get; set; }

        public ClientService(pizza_restaurant_ver_5Context Restaurant_Ver3Context)
        {
            this._RestaurantContext = Restaurant_Ver3Context;
            lastId = _RestaurantContext.Client.ToList().Count + 1;
        }

        public async Task Delete(int id)
        {
            var Client = await _RestaurantContext.Client.FirstOrDefaultAsync(b => b.IdClient == id);
            if (Client != null)
            {
                _RestaurantContext.Remove(Client);
            }
        }

        public async Task<Client> UpdatePoints(int id, int newPoints)
        {
            var entity = await _RestaurantContext.Client.FirstOrDefaultAsync(b => b.AccountIdAccount == id);
            entity.Points += newPoints;
            _RestaurantContext.Client.Update(entity);
            return entity;
        }

        public async Task<List<Client>> GetAll()
        {
            return await _RestaurantContext.Client.ToListAsync();
        }

        public async Task<Client> GetById(int id)
        {
            return await _RestaurantContext.Client.FindAsync(id);
        }

        public async Task<Client> Insert(Client entity)
        {
            await _RestaurantContext.Client.AddAsync(entity);
            lastId++;
            return entity;
        }

        public async Task Save()
        {
            await _RestaurantContext.SaveChangesAsync();
        }
    }
}
