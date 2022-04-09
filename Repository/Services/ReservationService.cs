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
        private readonly IRepository<Reservation> _ReservationRepo;

        public ReservationService(IRepository<Reservation> ReservationRepo)
        {
            _ReservationRepo = ReservationRepo;
        }
    }
}
