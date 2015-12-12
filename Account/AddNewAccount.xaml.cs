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
using CashMana.Views.Settings;
using orioks;


// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace CashMana.Views.Account
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class AddNewAccount : Page
    {
        public AddNewAccount()
        {
            this.InitializeComponent();
        }

        Profile DataProfile = new Profile();
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            DataProfile = e.Parameter as Profile;


        }

        private async void NewAccount(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            if (DataProfile != null)
            {
                profile = DataProfile;
            }
   double accStart;
   
            Models.Account acc = new Models.Account();
            acc.Name = accname.Text;


            if (String.IsNullOrEmpty(accname.Text))
            {
                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color = Color.FromArgb(255, 255, 145, 145);
               
                accname.Focus(FocusState.Keyboard);

            }
            else
            {
                if (!Double.TryParse(accbalance.Text, out accStart))
                {
                    SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                    mySolidColorBrush.Color = Color.FromArgb(255, 255, 145, 145);
                    //    accbalance.Foreground = mySolidColorBrush;
                    accbalance.BorderBrush = mySolidColorBrush;
                    accbalance.Text = "";
                  

                }
                else
                {
                    acc.start = Convert.ToDouble(accbalance.Text);
                    if (profile.Accounts.Count == 0)
                    {
                        acc.Idacc = 0;
                    }
                    else
                    {
                        acc.Idacc = profile.Accounts.Count;
                    }

                    acc.Histories = new ObservableCollection<History>();
                    profile.Accounts.Add(acc);

                    await ReadWrite.saveStringToLocalFile("data", JsonSerilizer.ToJson(profile));
                    if (profile.Password == null)
                    {
                        this.Frame.Navigate(
                      typeof(NewPassword),
                      new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
                    }
                    else
                    {
                        this.Frame.Navigate(
                      typeof(AccountPage),
                      new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
                    }
                }

            }
