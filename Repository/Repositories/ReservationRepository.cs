using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IReservationRepository : IRepository<Reservation>
    {

    }

    public class ReservationRepository : IReservationRepository
    {
        private readonly IDbContextFactory<RestaurantDBContext> _factory;
        public int lastId { get; set; }

        public ReservationRepository(IDbContextFactory<RestaurantDBContext> factory)
        {
            this._factory = factory;
            using (var context = _factory.CreateDbContext())
            {
                lastId = context.Reservation.ToList().Count + 1;
            }
        }

        public async Task Delete(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                var Reservation = await context.Reservation.FirstOrDefaultAsync(b => b.IdReservation == id);
                if (Reservation != null)
                {
                    context.Remove(Reservation);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<List<Reservation>> GetAll()
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Reservation.ToListAsync();
            }
        }

        public async Task<Reservation> GetById(int id)
        {
            using (var context = _factory.CreateDbContext())
            {
                return await context.Reservation.FindAsync(id);
            }
        }

        public async Task<Reservation> Insert(Reservation entity)
        {
            using (var context = _factory.CreateDbContext())
            {
                await context.Reservation.AddAsync(entity);
                await context.SaveChangesAsync();
                lastId++;
                return entity;
            }
        }
    }
}
