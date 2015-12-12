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
using Windows.UI.Xaml.Navigation;
using CashMana.JsonSerializer;
using CashMana.Models;
using CashMana.Views.Account;
using orioks;

// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace CashMana.Views.Settings
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class NewPassword : Page
    {
        public NewPassword()
        {
            this.InitializeComponent();
        }
        Profile _Profile = new Profile();
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var readData = await ReadWrite.readStringFromLocalFile("data");
            Profile CurrentProfile = JsonSerilizer.ToProfile(readData);
            _Profile = CurrentProfile;
           
        }

        private async void Create_Pass(object sender, RoutedEventArgs e)
        {
            _Profile.Password = newPassBox.Text;
            await ReadWrite.saveStringToLocalFile("data", JsonSerilizer.ToJson(_Profile));

            this.Frame.Navigate(
              typeof(AccountPage)
              );
        }
    }
}
