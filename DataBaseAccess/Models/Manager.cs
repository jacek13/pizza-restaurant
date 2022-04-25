using System;
using System.Collections.Generic;

namespace DataBaseAccess.Models
{
    public partial class Manager
    {
        public Manager()
        {
            ManagerAssignments = new HashSet<ManagerAssignment>();
            Reservations = new HashSet<Reservation>();
        }

        public int IdManager { get; set; }
        public int? SalaryNetto { get; set; }
        public int? SalaryBrutto { get; set; }
        public int AccountIdAccount { get; set; }

        public virtual Account AccountIdAccountNavigation { get; set; }
        public virtual ICollection<ManagerAssignment> ManagerAssignments { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
