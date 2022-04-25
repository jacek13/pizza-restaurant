using System;
using System.Collections.Generic;

namespace DataBaseAccess.Models
{
    public partial class Client
    {
        public Client()
        {
            Orders = new HashSet<Order>();
            Reservations = new HashSet<Reservation>();
        }

        public int IdClient { get; set; }
        public int Points { get; set; }
        public string Address { get; set; }
        public int AccountIdAccount { get; set; }

        public virtual Account AccountIdAccountNavigation { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
