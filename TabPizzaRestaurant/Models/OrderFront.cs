using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TabPizzaRestaurant.Models
{
    public class OrderFront
    { 
        [Required]
        [StringLength(20, ErrorMessage = "First Name is too long.")]
        [MinLength(3, ErrorMessage = "First Name is too short")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Last Name is too long.")]
        [MinLength(3, ErrorMessage = "Last Name is too short")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }
    }
}
