using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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

        private async void Error(string title)
        {
            ContentDialog dialogo = new ContentDialog();
            dialogo.PrimaryButtonText = "Ок";
            dialogo.Title = title;
            await dialogo.ShowAsync();
        }

        private async void AddHistory(object sender, RoutedEventArgs e)
        {

            double accStart;

            if (!Double.TryParse(AmountBox.Text, out accStart))
            {
                Error("Введите сумму в рублях");
                AmountBox.Text = "";
                AmountBox.Focus(FocusState.Keyboard);

            }
            else if(Convert.ToDouble(AmountBox.Text) < 0)
            {
                Error("Некорректные данные. Необходимо ввести число");
                AmountBox.Text = "";
                AmountBox.Focus(FocusState.Keyboard);
            }
            else if (Convert.ToDouble(AmountBox.Text) > 1000000000000)
            {
                Error("Слишком большое число");
            }
            else if (AutoSuggestBox.Text == "")
            {
                Error("Введите название категории");
            }
            else if (AutoSuggestBox.Text.Length > 19)
            {
                Error("Введите название не длинее 20-ти символов");
            }
            else
            {
                History NewHistory = new History();


                NewHistory.CurrentCategory.name = AutoSuggestBox.Text;

              
                NewHistory.Amount = Convert.ToDouble(AmountBox.Text);

                if (((ComboBoxItem)IncomeBox.SelectedItem).Content.ToString() == "Доход") NewHistory.Income = true;
                else NewHistory.Income = false;
                NewHistory.Idhis = acc.MyProfile.Accounts[myID].Histories.Count;    
                NewHistory.DateOfOperation = DateBox.Date;
                CurrentProfile.Accounts[myID].Histories.Add(NewHistory);
                CurrentProfile.Accounts[myID].Histories.OrderBy(o => o.DateOfOperation).Reverse();
                await ReadWrite.saveStringToLocalFile("data", JsonSerilizer.ToJson(CurrentProfile));
                if (CheckLastPageForward(typeof(AccountPage))) //Переход с AddNewAccount via Forward
                {
                    this.Frame.Navigate(
               typeof(AccountPage), acc.id,
               new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
                }
                else if (CheckLastPage(typeof(AccountPage)))
                {
                    this.Frame.Navigate(
              typeof(AccountPage), acc.id,
              new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
                }
                else
                {
                    DataToProvide data = new DataToProvide();
                    data.MyProfile = CurrentProfile;
                    data.id = myID;
                    this.Frame.Navigate(
             typeof(HistoryPage), data,
             new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
                }
            }
            

          

        }




        public static List<Category> GetFakeRuntimeItems()
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

        public static List<Category> Outcome()
        {
            var items = new List<Category>();
      
            items.Add(new Category() { name = "Транспорт" });
            items.Add(new Category() { name = "Еда" });
            items.Add(new Category() { name = "Развлечения" });
            items.Add(new Category() { name = "Телефон" });
            items.Add(new Category() { name = "Интернет" });

            return items;
        }
        private void Suggest_Textchanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
           //var vm = DataContext as AddNewHistoryViewModel;
           
            if (args.Reason == AutoSuggestionBoxTextChangeReason.SuggestionChosen)
            {
                return;
            }
         

            var x = new ObservableCollection<Category>(GetFakeRuntimeItems());

            ObservableCollection<string> myList = new ObservableCollection<string>();
            foreach (var myString in x)
            {
                if (myString.name.Contains(sender.Text) == true)
                {
                    myList.Add(myString.name);
                }
            }

            



            AutoSuggestBox.ItemsSource = myList;
           



        }

       



        private void Suggest_Chosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args.SelectedItem.ToString() == "Зарплата")
            {
                IncomeBox.SelectedIndex = 0;
                IncomeBox.IsEnabled = false;
            }
            else if (args.SelectedItem.ToString() == "Телефон" ||
                     args.SelectedItem.ToString() == "Одежда" ||
                     args.SelectedItem.ToString() == "Интернет" ||
                     args.SelectedItem.ToString() == "Еда" ||
                    args.SelectedItem.ToString() == "Развлечения" ||
                     args.SelectedItem.ToString() == "Транспорт"
                )
            {
                IncomeBox.SelectedIndex = 1;
                IncomeBox.IsEnabled = false;
            }

            else IncomeBox.IsEnabled = true;

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
