using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryServices
{
    internal class AccountService : IRepository<Account, int>
    {
        private readonly pizza_restaurant_ver_5Context _RestaurantContext = null;
        public int lastId { get; set; }
        // TODO Można też dodać parametr size

        public AccountService(pizza_restaurant_ver_5Context Restaurant_Ver3Context)
        {
            this._RestaurantContext = Restaurant_Ver3Context;
            this.lastId = _RestaurantContext.Account.ToList().Count + 1;
        }

        public async Task Delete(int id)
        {
            var Account = await _RestaurantContext.Account.FirstOrDefaultAsync(b => b.IdAccount == id);
            if (Account != null)
            {
                _RestaurantContext.Remove(Account);
            }
        }

        public async Task<List<Account>> GetAll()
        {
            return await _RestaurantContext.Account.ToListAsync();
        }

        // Test bez async i await
        public List<Account> GetAllTest()
        {
            return _RestaurantContext.Account.ToList();
        }

        public async Task<Account> GetById(int id)
        {
            return await _RestaurantContext.Account.FindAsync(id);
        }

        public async Task<Account> Insert(Account entity)
        {
            await _RestaurantContext.Account.AddAsync(entity);
            lastId++;
            return entity;
        }

        public async Task Save()
        {
            await _RestaurantContext.SaveChangesAsync();
        }
    }
}