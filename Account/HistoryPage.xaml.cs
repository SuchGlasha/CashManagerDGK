using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using CashMana.JsonSerializer;
using CashMana.Models;
using CashMana.ViewModels;
using CashMana.Views.Account;
using orioks;

// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace CashMana.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class HistoryPage : Page
    {
        public HistoryPage()
        {
            this.InitializeComponent();
        }


        private Profile CurrentProfile = new Profile();
        private int myID;
     
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

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (CheckLastPageForward(typeof(AddNewHistory))) //Переход с AddNewAccount via Forward
            {
                //    var backStack = Frame.BackStack;
                //    var backStackCount = backStack.Count;

                //    if (backStackCount > 1)
                //    {
                //        backStack.RemoveAt(backStackCount - 1);
                //        backStack.RemoveAt(backStackCount - 2);
                //    }
                //    var profile = e.Parameter as Models.Account;

                //    var vm = DataContext as HistoryPageViewModel;



                //    vm.SelectedItem = profile;\
                var profile = e.Parameter as Models.Account;
                myID = profile.Idacc;
            }
            else if (CheckLastPage(typeof (AddNewHistory)))
            {
                var backStack = Frame.BackStack;
                var backStackCount = backStack.Count;

                if (backStackCount > 1)
                {
                    backStack.RemoveAt(backStackCount - 1);
                    backStack.RemoveAt(backStackCount - 2);
                }
                var profile = e.Parameter as DataToProvide;
                CurrentProfile = profile.MyProfile;
                var vm = DataContext as HistoryPageViewModel;

                await ReadWrite.saveStringToLocalFile("data", JsonSerilizer.ToJson(profile.MyProfile));
                
                vm.SelectedItem = profile.MyProfile.Accounts[profile.id];

               
            }
            else
            {
            
                   var vm = DataContext as HistoryPageViewModel;
                var readData = await ReadWrite.readStringFromLocalFile("data");
                myID = (int) e.Parameter;
                var profile = JsonSerilizer.ToProfile(readData);
               
                Models.Account list = profile.Accounts[myID];
                CurrentProfile = JsonSerilizer.ToProfile(readData);
               
                vm.SelectedItem = list;


            }
        }



        private bool ShouldGoToWideState()
        {
            return Window.Current.Bounds.Width >= 860;
        }

        private void PageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            if (ShouldGoToWideState())
            {
                // We shouldn't see this page since we are in "wide master-detail" mode.
                // Play a transition as we are navigating from a separate page.
                NavigateBackForWideState(useTransition: true);
            }
            else
            {
                // Realize the main page content.
                FindName("RootPanel");
            }

            Window.Current.SizeChanged += Window_SizeChanged;
        }

        private void PageRoot_Unloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= Window_SizeChanged;
        }

        private void Window_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            if (ShouldGoToWideState())
            {
                // Make sure we are no longer listening to window change events.
                Window.Current.SizeChanged -= Window_SizeChanged;

                // We shouldn't see this page since we are in "wide master-detail" mode.
                NavigateBackForWideState(useTransition: false);
            }
        }

        void NavigateBackForWideState(bool useTransition)
        {
            // Evict this page from the cache as we may not need it again.
            NavigationCacheMode = NavigationCacheMode.Disabled;

            if (useTransition)
            {
                Frame.GoBack(new EntranceNavigationTransitionInfo());
            }
            else
            {
                Frame.GoBack(new SuppressNavigationTransitionInfo());
            }
        }


        //   private Models.Account accountDATA = new Models.Account();
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
           DataToProvide addHist = new DataToProvide();
            addHist.id = myID;
            addHist.MyProfile = CurrentProfile;

            this.Frame.Navigate(
             typeof(AddNewHistory), addHist ,
             new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
        }
    }
}
