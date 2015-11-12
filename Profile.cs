using System;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;

namespace CashMana.Models
{
    public class Profile
    {
        public string Password { get; set; }
        public DateTime CurrentData { get; set; }
        public ObservableCollection<Account> Accounts { get; set; }
      //  public CurrentCategories Categories { get; set; }
    }
}
