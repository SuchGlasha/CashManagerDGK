using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMana.Models.Categories
{
    public class Transport : Category
    {
        public double Amount { get; set; }


        public Transport()
        {
            Amount = 0;
            name = "Транспорт";
        }
    }
}
