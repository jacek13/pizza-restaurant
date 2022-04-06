using System;
using System.Collections.Generic;

namespace DataBaseAccess.Models
{
    public partial class Reservation
    {
        public int IdReservation { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartOfReservation { get; set; }
        public TimeSpan EndOfReservation { get; set; }
        public int? ManagerIdManager { get; set; }
        public int? ClientIdClient { get; set; }
        public int TableIdTable { get; set; }
        public int? TableRestaurantIdRestaurant { get; set; }

        public virtual Client ClientIdClientNavigation { get; set; }
        public virtual Manager ManagerIdManagerNavigation { get; set; }
        public virtual Table Table { get; set; }
    }
}
