using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryServices
{
    internal class ManagerAssignmentService : IRepository<ManagerAssignment, int>
    {
        private readonly pizza_restaurant_ver_6Context _RestaurantContext = null;
        public int lastId { get; set; }

        public ManagerAssignmentService(pizza_restaurant_ver_6Context Restaurant_Ver3Context)
        {
            this._RestaurantContext = Restaurant_Ver3Context;
            lastId = _RestaurantContext.ManagerAssignment.ToList().Count + 1;
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
            // TODO do zaimplementowania!

            //var ManagerAssignment = await _RestaurantContext.ManagerAssignment.FirstOrDefaultAsync(b => b.IdManagerAssignment == id);
            //if (ManagerAssignment != null)
            //{
            //    _RestaurantContext.Remove(ManagerAssignment);
            //}
        }

        public async Task<List<ManagerAssignment>> GetAll()
        {
            return await _RestaurantContext.ManagerAssignment.ToListAsync();
        }

        public async Task<ManagerAssignment> GetById(int id)
        {
            return await _RestaurantContext.ManagerAssignment.FindAsync(id);
        }

        public async Task<ManagerAssignment> Insert(ManagerAssignment entity)
        {
            await _RestaurantContext.ManagerAssignment.AddAsync(entity);
            lastId++;
            return entity;
        }

        public async Task Save()
        {
            await _RestaurantContext.SaveChangesAsync();
        }
    }
}
