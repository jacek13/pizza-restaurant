using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using DataBaseAccess.Models;

namespace Repository.Services
{
    public class ReservationService
    {
        private readonly IReservationRepository _reservationRepo;

        public ReservationService(IReservationRepository ReservationRepo)
        {
            _reservationRepo = ReservationRepo;
        }

        public async Task<Reservation> addNewReservation(Reservation reservation)
        {
            return await _reservationRepo.Insert(reservation);
        }

        public async Task<List<Reservation>> getAllReservationsIn(Restaurant restaurant)
        {
            return await _reservationRepo.fetchAllReservationsIn(restaurant);
        }

        public async Task<Reservation> GetReservationByID(int ReservationID)
        {
            return await _reservationRepo.GetById(ReservationID);
        }

        public async Task deleteReservationByID(int id)
        {
            await _reservationRepo.Delete(id);
        }

        public async Task updateReservation(Reservation reservation)
        {
            await _reservationRepo.update(reservation);
        }

        public async Task<List<Reservation>> getReservationInTimeFrame(
            int TableID, 
            string ReservationStart, 
            string ReservationEnd,
            DateOnly Date)
        {
            TimeOnly reservationStart = TimeOnly.Parse(ReservationStart);
            TimeOnly reservationEnd = TimeOnly.Parse(ReservationEnd);


            return await _reservationRepo.getReservationByTimeAndTable(
                TableID,
                reservationStart,
                reservationEnd,
                Date);
        }

    }
}
