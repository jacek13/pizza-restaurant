using System;
using System.ComponentModel.DataAnnotations;

namespace TabPizzaRestaurant.Models
{
    public class ReservationFront
    {
        [Required]
        [StringLength(20, ErrorMessage = "Name is too long.")]
        [MinLength(3, ErrorMessage = "Name is too short")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }


        [Required]
        public DateOnly Date { get; set; }


        [Required]
        [Timestamp]
        public string ReservationStart { get; set; }


        [Required]
        [Timestamp]
        public string ReservationEnd { get; set; }

    }
}
