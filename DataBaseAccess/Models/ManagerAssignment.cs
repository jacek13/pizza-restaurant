using System;
using System.Collections.Generic;

namespace DataBaseAccess.Models
{
    public partial class ManagerAssignment
    {
        public int ManagerIdManager { get; set; }
        public int RestaurantIdRestaurant { get; set; }
        public string AssignmentRole { get; set; }

        public virtual Manager Manager { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
