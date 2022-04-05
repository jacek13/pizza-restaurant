using System;
using System.Collections.Generic;

namespace DataBaseAccess.Models
{
    public partial class Client
    {
        public Client()
        {
            Order = new HashSet<Order>();
            Reservation = new HashSet<Reservation>();
        }

        public int IdClient { get; set; }
        public int Points { get; set; }
        public string Address { get; set; }
        public int AccountIdAccount { get; set; }

        public virtual Account AccountIdAccountNavigation { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
