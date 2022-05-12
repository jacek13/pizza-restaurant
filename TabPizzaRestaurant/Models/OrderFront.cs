using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TabPizzaRestaurant.Models
{
    public class OrderFront
    {
        [Required(ErrorMessage = "Imię wymagane!")]
        [StringLength(30, ErrorMessage = "Za długie!")]
        [MinLength(5, ErrorMessage = "Za krótkie!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko wymagane!")]
        [StringLength(30, ErrorMessage = "Za długie!")]
        [MinLength(5, ErrorMessage = "Za krótkie!")]
        public string LastName { get; set; }

        [StringLength(11, ErrorMessage = "Za długie!")]
        [Phone(ErrorMessage = "Zły format!")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Pole wymagane!")]
        [StringLength(30, ErrorMessage = "Za długie!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Pole wymagane!")]
        [StringLength(40, ErrorMessage = "Za długie!")]
        public string City { get; set; }

        [Required]
        public int RestaurantId { get; set; }
    }
}
