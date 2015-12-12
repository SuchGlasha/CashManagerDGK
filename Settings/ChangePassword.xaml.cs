using System;
using System.Collections.Generic;
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
using orioks;

// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace CashMana.Views.Settings
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class ChangePassword : Page
    {
        public ChangePassword()
        {
            this.InitializeComponent();
        }


        private async void Tela_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            this.Frame.Navigate(
          typeof(SettingsPage),
          new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());

        }

        private async void Error(string title)
        {
            ContentDialog dialogo = new ContentDialog();
            dialogo.PrimaryButtonText = "Ок";
            dialogo.Title = title;
            await dialogo.ShowAsync();
        }
        private async void NewAccount(object sender, RoutedEventArgs e)
        {
            var readDataa = await ReadWrite.readStringFromLocalFile("data");
            Profile CurrentProfilea = JsonSerilizer.ToProfile(readDataa);
            if (CurrentProfilea.Password == accpass.Password)
            {
                if (accnewpass.Password == accnewpasslast.Password)
                {
                    if (accnewpasslast.Password == "" && accnewpass.Password == "")
                    {
                        Error("Пароль должен содержать символы");
                    }
                    else
                    {
                        CurrentProfilea.Password = accnewpass.Password;
                        await ReadWrite.saveStringToLocalFile("data", JsonSerilizer.ToJson(CurrentProfilea));

                        ContentDialog dialogo = new ContentDialog();

                        dialogo.PrimaryButtonText = "Отлично!";
                        dialogo.PrimaryButtonClick += Tela_PrimaryButtonClick;
                        dialogo.Title = "Пароль был успешно изменен!";


                        await dialogo.ShowAsync();
                    }
               

                   
                }
                else
                {
                    Error("Пароли не совпадают");
                    accnewpasslast.Password = "";
                }
            }
            else
            {
                Error("Неверный пароль");
                accpass.Password = "";
            }

           
        }
    }
}
