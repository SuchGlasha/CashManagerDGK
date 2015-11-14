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
using CashMana.Controls;
using CashMana.JsonSerializer;
using CashMana.Models;
using orioks;

// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace CashMana.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
            MyUserControl1.AcessarSistema();
        }

        private Profile MyProfile;
        
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var readData = await ReadWrite.readStringFromLocalFile("data");
            Profile CurrentProfile = JsonSerilizer.ToProfile(readData);
            MyProfile = CurrentProfile;

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (Pass.Text == MyProfile.Password)
            {
                this.Frame.Navigate(
            typeof(AccountPage),0,
            new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
            }
        }
        
    }
}
