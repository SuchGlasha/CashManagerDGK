using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CashMana.JsonSerializer;
using CashMana.Models;
using CashMana.ViewModels;
using orioks;

// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace CashMana.Views.Account
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class AddNewHistory : Page
    {
        public AddNewHistory()
        {
            this.InitializeComponent();




        }

        private bool CheckLastPage(Type desiredPage)
        {
            var lastPage = Frame.BackStack.LastOrDefault();
            return (lastPage != null && lastPage.SourcePageType.Equals(desiredPage)) ? true : false;
        }

        private bool CheckLastPageForward(Type desiredPage)
        {
            var lastPage = Frame.ForwardStack.LastOrDefault();
            return (lastPage != null && lastPage.SourcePageType.Equals(desiredPage)) ? true : false;
        }



        private DataToProvide acc = new DataToProvide();
        private Profile CurrentProfile = new Profile();
        private int myID;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            acc = e.Parameter as DataToProvide;
            myID = acc.id;
            CurrentProfile = acc.MyProfile;



            //var vm = DataContext as AddNewHistoryViewModel;

            //vm.TestItems = new ObservableCollection<Category>(GetFakeRuntimeItems());


        }

        private async void AddHistory(object sender, RoutedEventArgs e)
        {
          
            History NewHistory = new History();
        
           
            NewHistory.CurrentCategory.name = AutoSuggestBox.Text;

            NewHistory.Amount = Convert.ToInt32(AmountBox.Text);
            if (((ComboBoxItem) IncomeBox.SelectedItem).Content.ToString() == "Доход") NewHistory.Income = true;
            else NewHistory.Income = false;
            NewHistory.Idhis = acc.MyProfile.Accounts[myID].Histories.Count;
            NewHistory.DateOfOperation = DateBox.Date;

            CurrentProfile.Accounts[myID].Histories.Add(NewHistory);
            await ReadWrite.saveStringToLocalFile("data", JsonSerilizer.ToJson(CurrentProfile));
            if (CheckLastPageForward(typeof(AccountPage))) //Переход с AddNewAccount via Forward
            {
                this.Frame.Navigate(
           typeof(AccountPage), acc.id,
           new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
            }
            else if (CheckLastPage(typeof (AccountPage)))
            {
                this.Frame.Navigate(
          typeof(AccountPage), acc.id,
          new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
            }
            else
            {
                 this.Frame.Navigate(
          typeof(HistoryPage), acc,
          new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
            }

          

        }




        public List<Category> GetFakeRuntimeItems()
        {
            var items = new List<Category>();
            items.Add(new Category() { name = "Зарплата"});
            items.Add(new Category() { name = "Транспорт" });
            items.Add(new Category() { name = "Еда" });
            items.Add(new Category() { name = "Развлечения" });
            items.Add(new Category() { name = "Телефон" });
            items.Add(new Category() { name = "Интернет" });

            return items;
        }

        private void Suggest_Textchanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
           var vm = DataContext as AddNewHistoryViewModel;
           
            if (args.Reason == AutoSuggestionBoxTextChangeReason.SuggestionChosen)
            {
                return;
            }
         

            var x = new ObservableCollection<Category>(GetFakeRuntimeItems());

            ObservableCollection<Category> myList = new ObservableCollection<Category>();
            foreach (var myString in x)
            {
                if (myString.name.Contains(sender.Text) == true)
                {
                    myList.Add(myString);
                }
            }

          

            vm.TestItems = myList;
           



        }

        private void Suggest_Chosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {


            var x = new ObservableCollection<Category>(GetFakeRuntimeItems());
            foreach (var cut in x)
            {
                if (args.SelectedItem.Equals(cut))
                {
                    AutoSuggestBox.Text = cut.name;
                }
            }
           
        }
    }


}
