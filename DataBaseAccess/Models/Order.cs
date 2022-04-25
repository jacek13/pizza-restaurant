using System;
using System.Collections.Generic;

namespace DataBaseAccess.Models
{
    public partial class Order
    {
        public Order()
        {
            Dishes = new HashSet<Dishes>();
        }

        public int IdOrder { get; set; }
        public string DeliveryAdress { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string PhoneNumber { get; set; }
        public string AdditionalInformation { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? ClientIdClient { get; set; }
        public int? RestaurantIdRestaurant { get; set; }

        public virtual Client ClientIdClientNavigation { get; set; }
        public virtual Restaurant RestaurantIdRestaurantNavigation { get; set; }
        public virtual ICollection<Dishes> Dishes { get; set; }
    }
}
