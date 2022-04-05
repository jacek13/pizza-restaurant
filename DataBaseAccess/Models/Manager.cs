using System;
using System.Collections.Generic;

namespace DataBaseAccess.Models
{
    public partial class Manager
    {
        public Manager()
        {
            ManagerAssignment = new HashSet<ManagerAssignment>();
            Reservation = new HashSet<Reservation>();
        }

        public int IdManager { get; set; }
        public string Role { get; set; }
        public int AccountIdAccount { get; set; }

        public virtual Account AccountIdAccountNavigation { get; set; }
        public virtual ICollection<ManagerAssignment> ManagerAssignment { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
