using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TabPizzaRestaurant.Models
{
    public class ClientFront
    {
        [Required(ErrorMessage = "Email wymagany!")]
        [EmailAddress(ErrorMessage = "Zły format!")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "Login wymagany!")]
        [StringLength(30, ErrorMessage = "Za długie!")]
        [MinLength(5, ErrorMessage = "Za krótkie!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Hasło wymagane!")]
        [StringLength(30, ErrorMessage = "Za długie!")]
        [MinLength(5, ErrorMessage = "Za krótkie!")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$",
            ErrorMessage = "Wymagane znaki specjalne, cyfry, małe i duże litery!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Imię wymagane!")]
        [StringLength(15, ErrorMessage = "Za długie!")]
        [MinLength(5, ErrorMessage = "Za krótkie!")]
        [RegularExpression(@"^[a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ]{1,40}$",
            ErrorMessage = "Błędne znaki!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nazwisko wymagane!")]
        [StringLength(15, ErrorMessage = "Za długie!")]
        [MinLength(5, ErrorMessage = "Za krótkie!")]
        [RegularExpression(@"^[a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ]{1,40}$",
            ErrorMessage = "Błędne znaki!")]
        public string Surname { get; set; }

        [StringLength(11, ErrorMessage = "Za długie!")]
        [Phone(ErrorMessage = "Zły format!")]
        public string PhoneNumber { get; set; }

        [StringLength(30, ErrorMessage = "Za długie!")]
        public string Address { get; set; }

        public string Role { get; set; }
    }
}