using System;
using System.Collections.Generic;

namespace DataBaseAccess.Models
{
    public partial class Table
    {
        public Table()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int IdTable { get; set; }
        public int? Capacity { get; set; }
        public int RestaurantIdRestaurant { get; set; }

        public virtual Restaurant restaurant { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
