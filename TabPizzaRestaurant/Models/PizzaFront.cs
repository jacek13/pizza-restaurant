using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TabPizzaRestaurant.Models
{
    public class PizzaFront
    {
        [Display(Name = "Cena")]
        [Required]
        [DataType(DataType.Currency)]
        [Range(0, 1000, ErrorMessage = "Zła wartość! przedział: (0-1000).")]
        public int price { get; set; }

        [Display(Name = "Koszt")]
        [Required]
        [DataType(DataType.Currency)]
        [Range(0, 1000, ErrorMessage = "Zła wartość! przedział: (0-1000).")]
        public int cost { get; set; }

        [Display(Name = "Dostępność")]
        [Required]
        public bool isAvailable { get; set; }

        [Display(Name = "Nazwa")]
        [Required]
        [StringLength(30, ErrorMessage = "Za Długie.")]
        [MinLength(3, ErrorMessage = "Za krótkie")]
        public string type { get; set; }

        [Display(Name = "Rozmiar")]
        [Required]
        [Range(16, 64, ErrorMessage = "Zły rozmiar! przedział: (16-64).")]
        public int size{ get; set; }

        [Display(Name = "Punkty")]
        [Required]
        [Range(0, 100, ErrorMessage = "Zła wartość! przedział: (0-100).")]
        public int points { get; set; }
    }
}