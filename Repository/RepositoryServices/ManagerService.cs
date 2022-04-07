using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryServices
{
    internal class ManagerService : IRepository<Manager, int>
    {
        private readonly pizza_restaurant_ver_6Context _RestaurantContext = null;
        public List<int> foreignKeys; // TODO 
        public int lastId { get; set; }
        public ManagerService(pizza_restaurant_ver_6Context Restaurant_Ver3Context)
        {
            this._RestaurantContext = Restaurant_Ver3Context;
            //var tmp = _RestaurantContext.Manager.ToList();
            //foreach (var item in tmp)
            //    foreignKeys.Add(item.AccountIdAccount);
        }

        public async Task Delete(int id)
        {
            var Manager = await _RestaurantContext.Manager.FirstOrDefaultAsync(b => b.IdManager == id);
            if (Manager != null)
            {
                _RestaurantContext.Remove(Manager);
            }
        }

        public async Task<List<Manager>> GetAll()
        {
            return await _RestaurantContext.Manager.ToListAsync();
        }

        public async Task<Manager> GetById(int id)
        {
            return await _RestaurantContext.Manager.FindAsync(id);
        }

        public async Task<Manager> Insert(Manager entity)
        {
            await _RestaurantContext.Manager.AddAsync(entity);
            lastId++;
            return entity;
        }

        public async Task Save()
        {
            await _RestaurantContext.SaveChangesAsync();
        }
    }
}
