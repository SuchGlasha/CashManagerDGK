using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMana.Models.Categories
{
    public class Food : Category
    {
        public double Amount { get; set; }

        public Food()
        {
            Amount = 0;
            name = "Еда";
        }
    }
}
