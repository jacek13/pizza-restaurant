using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IManagerAssignmentRepository : IRepository<ManagerAssignment>
    {

    }
    
    public class ManagerAssignmentRepository : IManagerAssignmentRepository
    {
        private readonly IDbContextFactory<RestaurantDBContext> _factory;
        public int lastId { get; set; }

        public ManagerAssignmentRepository(IDbContextFactory<RestaurantDBContext> factory)
        {
            _factory = factory;
            using(var context = factory.CreateDbContext())
            {
                lastId = context.ManagerAssignment.ToList().Count + 1;
            }
        }

        public ManagerAssignmentRepository()
        {
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
            using (var context = _factory.CreateDbContext())
            {
                return await context.ManagerAssignment.ToListAsync();
            }
        }

        public async Task<ManagerAssignment> GetById(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.ManagerAssignment.FindAsync(id);
            }
        }

        public async Task<ManagerAssignment> Insert(ManagerAssignment entity)
        {
            using (var context = _factory.CreateDbContext())
            {
                await context.ManagerAssignment.AddAsync(entity);
                await context.SaveChangesAsync();
                lastId++;
                return entity;
            }
        }

        public Task<ManagerAssignment> Update(ManagerAssignment entity)
        {
            return null;
        }
    }
}
