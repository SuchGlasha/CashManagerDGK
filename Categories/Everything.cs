using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMana.Models.Categories
{
    public class Everything : Category
    {

        public double Amount { get; set; }


        public Everything()
        {
            Amount = 0;
            name = "Другое";
        }
    }
}
