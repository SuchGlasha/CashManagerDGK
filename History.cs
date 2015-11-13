using System;

namespace CashMana.Models
{
    public class History
    {
        public double Amount { get; set; }
        public bool Income { get; set; }
        public DateTimeOffset DateOfOperation { get; set; }
        public Category CurrentCategory { get; set; }
        public int Idhis { get; set; }
        
        public History()
        {
            Amount = 0;
            Income = false;
            DateOfOperation = DateTime.Today;
            CurrentCategory = new Category();
        }
    }
}
