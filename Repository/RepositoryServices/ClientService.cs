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
        private readonly pizza_restaurant_ver_3Context _Restaurant_Ver3Context = null;

        public ClientService(pizza_restaurant_ver_3Context Restaurant_Ver3Context)
        {
            this._Restaurant_Ver3Context = Restaurant_Ver3Context;
        }

        public async Task Delete(int id)
        {
            var Client = await _Restaurant_Ver3Context.Client.FirstOrDefaultAsync(b => b.IdClient == id);
            if (Client != null)
            {
                _Restaurant_Ver3Context.Remove(Client);
            }
        }

        public async Task<List<Client>> GetAll()
        {
            return await _Restaurant_Ver3Context.Client.ToListAsync();
        }

        public async Task<Client> GetById(int id)
        {
            return await _Restaurant_Ver3Context.Client.FindAsync(id);
        }

        public async Task<Client> Insert(Client entity)
        {
            await _Restaurant_Ver3Context.Client.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await _Restaurant_Ver3Context.SaveChangesAsync();
        }
    }
}
