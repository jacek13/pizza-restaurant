using System;
using System.Collections.Generic;

namespace DataBaseAccess.Models
{
    public partial class Reservation
    {
        public int IdReservation { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartOfReservation { get; set; }
        public TimeOnly EndOfReservation { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int? ManagerIdManager { get; set; }
        public int? ClientIdClient { get; set; }
        public int TableIdTable { get; set; }
        public int? TableRestaurantIdRestaurant { get; set; }

        public virtual Client ClientIdClientNavigation { get; set; }
        public virtual Manager ManagerIdManagerNavigation { get; set; }
        public virtual Table Table { get; set; }
    }
}
