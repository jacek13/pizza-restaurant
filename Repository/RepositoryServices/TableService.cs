using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryServices
{
    internal class TableService : IRepository<Table, int>
    {
        private readonly pizza_restaurant_ver_6Context _RestaurantContext = null;
        public int lastId { get; set; }

        public TableService(pizza_restaurant_ver_6Context Restaurant_Ver3Context)
        {
            this._RestaurantContext = Restaurant_Ver3Context;
            lastId = _RestaurantContext.Table.ToList().Count + 1;
        }

        public async Task Delete(int id)
        {
            var Table = await _RestaurantContext.Table.FirstOrDefaultAsync(b => b.IdTable == id);
            if (Table != null)
            {
                _RestaurantContext.Remove(Table);
            }
        }

        public async Task<List<Table>> GetAll()
        {
            return await _RestaurantContext.Table.ToListAsync();
        }

        public async Task<Table> GetById(int id)
        {
            return await _RestaurantContext.Table.FindAsync(id);
        }

        public async Task<Table> Insert(Table entity)
        {
            await _RestaurantContext.Table.AddAsync(entity);
            lastId++;
            return entity;
        }

        public async Task Save()
        {
            await _RestaurantContext.SaveChangesAsync();
        }
    }
}
