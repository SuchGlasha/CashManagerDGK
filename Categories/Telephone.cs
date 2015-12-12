using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMana.Models.Categories
{
    public class Telephone : Category
    {
        public double Amount { get; set; }


        public Telephone()
        {
            Amount = 0;
            name = "Телефон";
        }

    }
}
