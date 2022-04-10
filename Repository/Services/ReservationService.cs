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
        private readonly IReservationRepository _ReservationRepo;

        public ReservationService(IReservationRepository ReservationRepo)
        {
            _ReservationRepo = ReservationRepo;
        }
    }
}
