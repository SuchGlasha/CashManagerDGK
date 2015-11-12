# CashManagerDGK

краusing System.Collections.ObjectModel;
using CashMana.Models.Categories;

namespace CashMana.Models
{
   public class Account
    {
        public string Name { get; set; }
        public double CurrentBalance {get; set; }
        public double start { get; set; }
        public ObservableCollection<History> Histories { get; set; }
        public int Idacc { get; set; }

        public CurrentCategories Categories { get; set; }

        public ObservableCollection<CategoryStat> ListCategoryStats { get; set; }
    }
}
