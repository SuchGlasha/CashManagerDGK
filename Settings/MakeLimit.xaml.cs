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
using orioks;

// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace CashMana.Views.Settings
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MakeLimit : Page
    {
        public MakeLimit()
        {
            this.InitializeComponent();
        }

        private Models.Account _lastSelectedItem;

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var readDataa = await ReadWrite.readStringFromLocalFile("data");
            Profile CurrentProfilea = JsonSerilizer.ToProfile(readDataa);

            MasterListView.ItemsSource = CurrentProfilea.Accounts;
        }

        private void ToHistory_Click(object sender, ItemClickEventArgs e)
        {
            var clickedItem = e.ClickedItem as Models.Account;
            _lastSelectedItem = clickedItem;
            Frame.Navigate(typeof(NewLimit), _lastSelectedItem, new DrillInNavigationTransitionInfo());
        }
    }
}
