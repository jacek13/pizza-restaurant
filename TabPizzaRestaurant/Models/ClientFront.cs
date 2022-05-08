using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TabPizzaRestaurant.Models
{
    public class ClientFront
    {
        //[Required]
        //[EmailAddress]
        public string EMail { get; set; }

        //[Required]
        //[StringLength(30, ErrorMessage = "Login is too long.")]
        //[MinLength(5, ErrorMessage = "Login is too short")]
        public string Login { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required]
        //[StringLength(15, ErrorMessage = "First Name is too long.")]
        //[MinLength(5, ErrorMessage = "First Name is too short")]
        public string Name { get; set; }

        //[Required]
        //[StringLength(15, ErrorMessage = "Last Name is too long.")]
        //[MinLength(5, ErrorMessage = "Last Name is too short")]
        public string Surname { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Role { get; set; }
    }
}