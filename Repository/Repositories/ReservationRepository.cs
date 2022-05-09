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
        Task<List<Reservation>> fetchAllReservationsIn(Restaurant restaurant);
        Task<List<Reservation>> getReservationByTimeAndTable(
            int tableID, 
            TimeOnly reservationStart, 
            TimeOnly reservationEnd,
            DateOnly date);
        Task update(Reservation reservation);
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

        public async Task<List<Reservation>> fetchAllReservationsIn(Restaurant restaurant)
        {
            using (var context = _factory.CreateDbContext())
            {
                var reservations = from r in context.Reservation
                                   join t in context.Table
                                   on r.TableIdTable equals t.IdTable
                                   join re in context.Restaurant
                                   on t.IdTable equals restaurant.IdRestaurant
                                   select r;

                return await Task.FromResult(reservations.ToList());
            }
        }

        public async Task<List<Reservation>> getReservationByTimeAndTable(
            int tableID,
            TimeOnly reservationStart,
            TimeOnly reservationEnd,
            DateOnly date)
        {
            using (var context = _factory.CreateDbContext())
            {
                var reservations = from r in context.Reservation
                                   join t in context.Table
                                   on r.TableIdTable equals tableID
                                   where (r.Date.CompareTo(date).Equals(0)
                                   && r.StartOfReservation < reservationEnd
                                   && reservationStart < r.EndOfReservation)
                                   select r;

                return await Task.FromResult(reservations.Distinct().ToList());
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

        public async Task update(Reservation reservation)
        {
            using (var context = _factory.CreateDbContext())
            {
                context.Reservation.Update(reservation);
                await context.SaveChangesAsync();
            }
        }

        public Task<Reservation> Update(Reservation entity)
        {
            return null;
        }
    }
}
