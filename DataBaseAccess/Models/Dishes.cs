using System;
using System.Collections.Generic;

namespace DataBaseAccess.Models
{
    public partial class Dishes
    {
        public int OrderIdOrder { get; set; }
        public int PizzaIdPizza { get; set; }
        public int? HistoricalCost { get; set; }
        public int? HistoricalPrice { get; set; }
        public int? Amount { get; set; }

        public virtual Order OrderIdOrderNavigation { get; set; }
        public virtual Pizza PizzaIdPizzaNavigation { get; set; }
    }
}
