using System;
using System.ComponentModel.DataAnnotations;

namespace TabPizzaRestaurant.Models
{
    public class AccountFront
    {
        [Required]
        public int IdAccount { get; set; }

        [Required]
        public string EMail { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "First Name is too long.")]
        [MinLength(3, ErrorMessage = "First Name is too short")]
        public string Name { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Last Name is too long.")]
        [MinLength(3, ErrorMessage = "Last Name is too short")]
        public string Surname { get; set; }

        public DateTime? AccountCreationDate { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Role { get; set; }

    }
}
