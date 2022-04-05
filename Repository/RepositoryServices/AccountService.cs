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
        private readonly pizza_restaurant_ver_3Context _Restaurant_Ver3Context = null;

        public AccountService(pizza_restaurant_ver_3Context Restaurant_Ver3Context)
        {
            this._Restaurant_Ver3Context = Restaurant_Ver3Context;
        }

        public async Task Delete(int id)
        {
            var Account = await _Restaurant_Ver3Context.Account.FirstOrDefaultAsync(b => b.IdAccount == id);
            if (Account != null)
            {
                _Restaurant_Ver3Context.Remove(Account);
            }
        }

        public async Task<List<Account>> GetAll()
        {
            return await _Restaurant_Ver3Context.Account.ToListAsync();
        }

        public async Task<Account> GetById(int id)
        {
            return await _Restaurant_Ver3Context.Account.FindAsync(id);
        }

        public async Task<Account> Insert(Account entity)
        {
            await _Restaurant_Ver3Context.Account.AddAsync(entity);
            return entity;
        }

        public async Task Save()
        {
            await _Restaurant_Ver3Context.SaveChangesAsync();
        }
    }
}