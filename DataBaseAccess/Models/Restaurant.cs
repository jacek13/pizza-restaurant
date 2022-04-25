using System;
using System.Collections.Generic;

namespace DataBaseAccess.Models
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            ManagerAssignments = new HashSet<ManagerAssignment>();
            Orders = new HashSet<Order>();
            Tables = new HashSet<Table>();
        }

        public int IdRestaurant { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EMail { get; set; }

        public virtual ICollection<ManagerAssignment> ManagerAssignments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Table> Tables { get; set; }
    }
}
