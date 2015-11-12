using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMana.Models.Categories
{
    public class Internet : Category
    {
        public double Amount { get; set; }


        public Internet()
        {
            Amount = 0;
            name = "Интернет";
        }
    }
}
