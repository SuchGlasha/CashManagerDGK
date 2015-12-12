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

// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace CashMana.Views.Settings
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class VISA : Page
    {
        public VISA()
        {
            this.InitializeComponent();
        }

        private async void Error(string title)
        {
            ContentDialog dialogo = new ContentDialog();
            dialogo.PrimaryButtonText = "Ок";
            dialogo.Title = title;
            await dialogo.ShowAsync();
        }

        private void AddCart(object sender, RoutedEventArgs e)
        {
            double accStart;

            if (!Double.TryParse(VisaCard.Text, out accStart))
            {
                Error("Поле номер карты может содержать только числа");
                VisaCard.Text = "";
                VisaCard.Focus(FocusState.Keyboard);

            }
            else
            {
                if (VisaCard.Text == "" || VisaCard.Text.Length < 11 || VisaCard.Text.Length > 13)
                {
                    Error("Поле номер карты должно содержать 12 символов");
                }
                else
                {
                    this.Frame.Navigate(
                    typeof(SettingsPage),
                    new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
                }
            }
           
            
        }
    }
}
