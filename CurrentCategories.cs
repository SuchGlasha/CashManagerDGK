using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Automation.Peers;
using CashMana.Models.Categories;

namespace CashMana.Models
{
    public class CurrentCategories
    {
        public Entertaiment Entertaiment { get; set; }
        public Food Food { get; set; }
        public Internet Internet { get; set; }
        public Salary Salary { get; set; }
        public Telephone Telephone { get; set; }
        public Transport Transport { get; set; }

        public Everything Everything { get; set; }

        public CurrentCategories()
        {
           Entertaiment = new Entertaiment();
            Food = new Food();
            Internet = new Internet();
            Salary = new Salary();
            Telephone = new Telephone();
            Everything = new Everything();
            Transport = new Transport();
        }

    }
}
