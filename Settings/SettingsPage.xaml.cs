using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CashMana.Controls;
using CashMana.Views.Settings;

// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace CashMana.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private List<NavMenuItem> navlist = new List<NavMenuItem>(
           new[]
           {
                new NavMenuItem()
                {
                    Symbol = Symbol.ReportHacked,
                    Label = "Безопасность",
                    DestPage = typeof(ChangePassword)
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.AllApps,
                    Label = "Категории",
                    DestPage = typeof(MakeLimit)
                },
                new NavMenuItem()
                {
                    Symbol = Symbol.Caption,
                    Label = "Карты",
                    DestPage = typeof(VISA)
                },
           });

        public SettingsPage()
        {
            this.InitializeComponent();
            NavMenuList.ItemsSource = navlist;
        }

        private void NavMenuList_ItemInvoked(object sender, ListViewItem listViewItem)
        {
            var item = (NavMenuItem)((NavMenuListView)sender).ItemFromContainer(listViewItem);

            if (item != null)
            {
                if (item.DestPage != null &&
                    item.DestPage != this.Frame.CurrentSourcePageType)
                {
                    this.Frame.Navigate(item.DestPage, item.Arguments);
                }
            }
        }
        
        private void NavMenuItemContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (!args.InRecycleQueue && args.Item != null && args.Item is NavMenuItem)
            {
                args.ItemContainer.SetValue(AutomationProperties.NameProperty, ((NavMenuItem)args.Item).Label);
            }
            else
            {
                args.ItemContainer.ClearValue(AutomationProperties.NameProperty);
            }
        }
    }
}
