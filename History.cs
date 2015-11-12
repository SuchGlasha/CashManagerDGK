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
    }
}
