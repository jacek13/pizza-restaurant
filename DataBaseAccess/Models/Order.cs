using System;
using System.Collections.Generic;

namespace DataBaseAccess.Models
{
    public partial class Order
    {
        public Order()
        {
            DishesCollection = new HashSet<Dishes>();
        }

        public int IdOrder { get; set; }
        public string DeliveryAdress { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public int? ClientIdClient { get; set; }
        public int? RestaurantIdRestaurant { get; set; }

        public virtual Client Client { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<Dishes> DishesCollection { get; set; }
    }
}
