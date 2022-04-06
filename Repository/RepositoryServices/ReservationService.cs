using DataBaseAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryServices
{
    internal class ReservationService : IRepository<Reservation, int>
    {
        private readonly pizza_restaurant_ver_5Context _RestaurantContext = null;
        public int lastId { get; set; }

        public ReservationService(pizza_restaurant_ver_5Context Restaurant_Ver3Context)
        {
            this._RestaurantContext = Restaurant_Ver3Context;
            lastId = _RestaurantContext.Reservation.ToList().Count + 1;
        }

        public async Task Delete(int id)
        {
            var Reservation = await _RestaurantContext.Reservation.FirstOrDefaultAsync(b => b.IdReservation == id);
            if (Reservation != null)
            {
                _RestaurantContext.Remove(Reservation);
            }
        }

        public async Task<List<Reservation>> GetAll()
        {
            return await _RestaurantContext.Reservation.ToListAsync();
        }

        public async Task<Reservation> GetById(int id)
        {
            return await _RestaurantContext.Reservation.FindAsync(id);
        }

        public async Task<Reservation> Insert(Reservation entity)
        {
            await _RestaurantContext.Reservation.AddAsync(entity);
            lastId++;
            return entity;
        }

        public async Task Save()
        {
            await _RestaurantContext.SaveChangesAsync();
        }
    }
}
