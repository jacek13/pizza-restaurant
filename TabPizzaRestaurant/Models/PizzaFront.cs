using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TabPizzaRestaurant.Models
{
    public class PizzaFront
    {
        [Required]
        [DataType(DataType.Currency)]
        public int price { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public int cost { get; set; }

        [Required]
        public bool isAvailable { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Name is too long.")]
        [MinLength(3, ErrorMessage = "Name is too short")]
        public string type { get; set; }

        [Required]
        public int size{ get; set; }

        [Required]
        public int points { get; set; }
    }
}