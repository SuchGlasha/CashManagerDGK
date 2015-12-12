using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMana.Models.Categories
{
    public class Entertaiment : Category
    {
        public double Amount { get; set; }


        public Entertaiment()
        {
            Amount = 0;
            name = "Развлечения";
        }
    }
}
