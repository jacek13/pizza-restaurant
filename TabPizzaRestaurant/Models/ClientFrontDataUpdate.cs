using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TabPizzaRestaurant.Models
{
    public class ClientFrontDataUpdate
    {
        [Required]
        public int IdClient { get; set; }
        
        [Required]
        public int Points { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int AccountIdAccount { get; set; }
    }
}