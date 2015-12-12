
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using CashMana.JsonSerializer;
using CashMana.Models;
using CashMana.ViewModels;
using CashMana.Views.Account;
using CashMana.Views.Settings;
using orioks;

// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace CashMana.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class AccountPage : Page
    {
        public AccountPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
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

        Profile DataProfile = new Profile();

        private Models.Account _lastSelectedItem;


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (CheckLastPageForward(typeof(AddNewAccount))) //Переход с AddNewAccount via Forward
            {
                //NothingTODO
            }
            else if (CheckLastPage(typeof (AddNewAccount)))
            {
                var backStack = Frame.BackStack;
                var backStackCount = backStack.Count;

                if (backStackCount > 1)
                {
                    backStack.RemoveAt(backStackCount - 1);
                    backStack.RemoveAt(backStackCount - 2);
                }
            
             
                var vm = DataContext as AccountPageViewModel;
                vm.TestItems.Clear();

                var readDataa = await ReadWrite.readStringFromLocalFile("data");
                Profile profile = JsonSerilizer.ToProfile(readDataa);
                DataProfile = profile;
                if (profile == null)
                {
                    
                }
                else
                {
                    vm.TestItems.Clear();
                    foreach (var item in profile.Accounts)
                    {
                        vm.TestItems.Add(item);
                    }
                   
                }
              
            }
            else if (CheckLastPage(typeof(AddNewHistory)))
            {
                var backStack = Frame.BackStack;
                var backStackCount = backStack.Count;

                if (backStackCount > 1)
                {
                    backStack.RemoveAt(backStackCount - 1);
                    backStack.RemoveAt(backStackCount - 2);
                }


                var vm = DataContext as AccountPageViewModel;
                vm.TestItems.Clear();

                var readDataa = await ReadWrite.readStringFromLocalFile("data");
                Profile CurrentProfilea = JsonSerilizer.ToProfile(readDataa);
                DataProfile = CurrentProfilea;
                string limits = "";
                var category = DataProfile.Accounts[_lastSelectedItem.Idacc].Categories;

                if (category.Telephone.isShow)
                {
                    if (category.Telephone.Amount >= category.Telephone.Limit * 0.95)
                    {
                        limits += category.Telephone.name + " ";
                        category.Telephone.isShow = false;
                    
                    }

                }
                if (category.Transport.isShow)
                {
                    if (category.Transport.Amount >= category.Transport.Limit * 0.95)
                    {
                        limits +=  category.Transport.name + " " ;
                        category.Transport.isShow = false;
                 
                    }

                }
               if (category.Food.isShow)
                {
                    if (category.Food.Amount >= category.Food.Limit * 0.95)
                    {
                        limits += category.Food.name + " ";
                        category.Food.isShow = false;
                    }

                }
                if (category.Entertaiment.isShow)
                {
                    if (category.Entertaiment.Amount >= category.Entertaiment.Limit * 0.95)
                    {
                        limits += category.Entertaiment.name + " ";
                        category.Entertaiment.isShow = false;
                    }

                }
              if (category.Internet.isShow)
                {
                    if (category.Internet.Amount >= category.Internet.Limit * 0.95)
                    {
                        limits += category.Internet.name + " ";
                        category.Internet.isShow = false;
                    }

                }
               if (category.Everything.isShow)
                {
                    if (category.Everything.Amount >= category.Everything.Limit * 0.95)
                    {
                        limits += category.Everything.name + " ";
                        category.Everything.isShow = false;
                    }

                }
                if (limits != "")
                {
                    ContentDialog dialogo = new ContentDialog();
                    dialogo.Title = "Вы превысили лимит на следующие категории:"+" " + limits;
                    dialogo.PrimaryButtonText = "Ок";

                    await dialogo.ShowAsync();

                }


                foreach (var item in CurrentProfilea.Accounts)
                {
                    vm.TestItems.Add(item);
                }


                if (e.Parameter != null)
                {
                    //  Parameter is item ID
                    var id = (int)e.Parameter;
                    var readData = await ReadWrite.readStringFromLocalFile("data");
                    Profile CurrentProfile = JsonSerilizer.ToProfile(readData);
                    foreach (var sub in CurrentProfile.Accounts)
                    {
                        if (sub.Idacc == id)
                        {
                            _lastSelectedItem = sub;

                        }
                    }
                }

            }
            else if (CheckLastPageForward(typeof(AddNewHistory)))
            {
                var backStack = Frame.BackStack;
                var backStackCount = backStack.Count;

                if (backStackCount > 1)
                {
                    backStack.RemoveAt(backStackCount - 1);
                    backStack.RemoveAt(backStackCount - 2);
                }

               
                var vm = DataContext as AccountPageViewModel;
                vm.TestItems.Clear();

                var readDataa = await ReadWrite.readStringFromLocalFile("data");
                Profile CurrentProfilea = JsonSerilizer.ToProfile(readDataa);
                DataProfile = CurrentProfilea;
              

                foreach (var item in CurrentProfilea.Accounts)
                {
                    vm.TestItems.Add(item);
                }


                if (e.Parameter != null)
                {
                    //  Parameter is item ID
                    var id = (int)e.Parameter;
                    var readData = await ReadWrite.readStringFromLocalFile("data");
                    Profile CurrentProfile = JsonSerilizer.ToProfile(readData);
                    foreach (var sub in CurrentProfile.Accounts)
                    {
                        if (sub.Idacc == id)
                        {
                            _lastSelectedItem = sub;

                        }
                    }
                }

            }
            else
            {
                var readDataa = await ReadWrite.readStringFromLocalFile("data");
                Profile CurrentProfilea = JsonSerilizer.ToProfile(readDataa);
              
                var vm = DataContext as AccountPageViewModel;
                vm.TestItems.Clear();
                
                  
                  
                    DataProfile = CurrentProfilea;
                    foreach (var item in CurrentProfilea.Accounts)
                    {
                        vm.TestItems.Add(item);
                    }
                

                if (e.Parameter != null)
                {
                    //  Parameter is item ID
                    var id = (int)e.Parameter;
                    var readData = await ReadWrite.readStringFromLocalFile("data");
                    Profile CurrentProfile = JsonSerilizer.ToProfile(readData); 
                    foreach (var sub in CurrentProfile.Accounts)
                    {
                        if (sub.Idacc == id)
                        {
                            _lastSelectedItem = sub;
                            
                        }
                    }
                }

                UpdateForVisualState(AdaptiveStates.CurrentState);

                // Don't play a content transition for first item load.
                // Sometimes, this content will be animated as part of the page transition.
                DisableContentTransitions();
            }
          
        }



       

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(
                 typeof(AddNewAccount),DataProfile,
                 new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());

        }


        private async void Delete_OnClick(object sender, RoutedEventArgs e)
        {

            ContentDialog dialogo = new ContentDialog();
          
            dialogo.PrimaryButtonText = "Удалить";
            dialogo.SecondaryButtonText = "Отмена";
            dialogo.SecondaryButtonClick += Tela_SecondaryButtonClick;
            dialogo.PrimaryButtonClick += Tela_PrimaryButtonClick;
            dialogo.Title = "Вы точно хотите удалить операцию?";


            await dialogo.ShowAsync();

        }

        private void Tela_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }

        private async void Tela_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            History del = new History();
            del = DetailListView.SelectedItem as History;
            DataProfile.Accounts[_lastSelectedItem.Idacc].Histories.Remove(del);
            await ReadWrite.saveStringToLocalFile("data", JsonSerilizer.ToJson(DataProfile));
        

        }

        private void DisableContentTransitions()
        {
            if (DetailListView != null)
            {
                DetailListView.ItemContainerTransitions.Clear();
            }
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            // Assure we are displaying the correct item. This is necessary in certain adaptive cases.
            MasterListView.SelectedItem = _lastSelectedItem;
        }

        private void AdaptiveStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            UpdateForVisualState(e.NewState, e.OldState);
        }


        private void UpdateForVisualState(VisualState newState, VisualState oldState = null)
        {
            var isNarrow = newState == NarrowState;

            if (isNarrow && oldState == DefaultState && _lastSelectedItem != null)
            {
                // Resize down to the detail item. Don't play a transition.
                Frame.Navigate(typeof(HistoryPage), _lastSelectedItem.Idacc, new SuppressNavigationTransitionInfo());
            }

            EntranceNavigationTransitionInfo.SetIsTargetElement(MasterListView, isNarrow);
            if (DetailListView != null)
            {
                EntranceNavigationTransitionInfo.SetIsTargetElement(DetailListView, !isNarrow);
            }
        }

        private void ToHistory_Click(object sender, ItemClickEventArgs e)
        {

            var clickedItem = e.ClickedItem as Models.Account;
            _lastSelectedItem = clickedItem;
            DetailListView.ItemsSource = clickedItem.Histories;

            if (AdaptiveStates.CurrentState == NarrowState)
            {
                // Use "drill in" transition for navigating from master list to detail view
                Frame.Navigate(typeof(HistoryPage), clickedItem.Idacc, new DrillInNavigationTransitionInfo());
            }
            else
            {
                // Play a refresh animation when the user switches detail items.
                EnableContentTransitions();
            }

            //var clickedItem = e.ClickedItem as Models.Account;
            //DataToProvide data = new DataToProvide();
            //data.id = clickedItem.Idacc;
            //data.MyProfile = DataProfile;
            //this.Frame.Navigate(
            //   typeof(HistoryPage), data,
            //   new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
        }

        private void EnableContentTransitions()
        {
            DetailListView.ItemContainerTransitions.Clear();
            DetailListView.ItemContainerTransitions.Add(new EntranceThemeTransition());
        }

        private void AddnewHistory_OnClick(object sender, RoutedEventArgs e)
        {
            DataToProvide addHist = new DataToProvide();
            addHist.id = _lastSelectedItem.Idacc;
            addHist.MyProfile = DataProfile;

            this.Frame.Navigate(
             typeof(AddNewHistory), addHist,
             new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
        }

        private async void DeleteAcc_OnClick(object sender, RoutedEventArgs e)
        {
            ContentDialog dialogo = new ContentDialog();

            dialogo.PrimaryButtonText = "Удалить";
            dialogo.SecondaryButtonText = "Отмена";
            dialogo.SecondaryButtonClick += New_SecondaryButtonClick;
            dialogo.PrimaryButtonClick += New_PrimaryButtonClick;
            dialogo.Title = "Вы точно хотите удалить счёт?";

            await dialogo.ShowAsync();
        }

        private async void New_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Models.Account del = new Models.Account();
            del = _lastSelectedItem;
            DataProfile.Accounts.Remove(del);
            await ReadWrite.saveStringToLocalFile("data", JsonSerilizer.ToJson(DataProfile));
            this.Frame.Navigate(
            typeof(AccountPage), _lastSelectedItem.Idacc,
            new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
        }

        private void New_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            //throw new NotImplementedException();
        }

    }
}
