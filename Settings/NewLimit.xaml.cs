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
    public sealed partial class NewLimit : Page
    {
        public NewLimit()
        {
            this.InitializeComponent();
        }
        private Profile MyProfile;
        private int myID;

        private async void Error(string title)
        {
            ContentDialog dialogo = new ContentDialog();
            dialogo.PrimaryButtonText = "Ок";
            dialogo.Title = title;
            await dialogo.ShowAsync();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var profile = e.Parameter as Models.Account;
            myID = profile.Idacc;
            var readDataa = await ReadWrite.readStringFromLocalFile("data");
            Profile CurrentProfilea = JsonSerilizer.ToProfile(readDataa);
            MyProfile = CurrentProfilea;
        }

        private async void NewLimitT(object sender, RoutedEventArgs e)
        {

            double accStart;

            if (!Double.TryParse(limit.Text, out accStart))
            {
               Error("Введите положительное число");
                limit.Text = "";
                limit.Focus(FocusState.Keyboard);

            }
          else if (Convert.ToDouble(limit.Text) < 0)
          {
                Error("Введите положительное число");
                limit.Text = "";
                limit.Focus(FocusState.Keyboard);
            }
            else
            {
                var category = MyProfile.Accounts[myID].Categories;
                if (comboBox.SelectedIndex == -1)
                {
                    Error("Выберите категорию");
                 
                }
                else
                {
                    string combo = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();
                    if (category.Entertaiment.name == combo)
                    {
                        category.Entertaiment.Limit = Convert.ToDouble(limit.Text);
                        category.Entertaiment.isShow = true;
                    }
                    else if (category.Telephone.name == combo)
                    {
                        category.Telephone.Limit = Convert.ToDouble(limit.Text);
                        category.Telephone.isShow = true;
                    }
                    else if (category.Everything.name == combo)
                    {
                        category.Everything.Limit = Convert.ToDouble(limit.Text);
                        category.Everything.isShow = true;
                    }
                    else if (category.Food.name == combo)
                    {
                        category.Food.Limit = Convert.ToDouble(limit.Text);
                        category.Food.isShow = true;
                    }
                    else if (category.Internet.name == combo)
                    {
                        category.Internet.Limit = Convert.ToDouble(limit.Text);
                        category.Internet.isShow = true;
                    }
                    else if (category.Transport.name == combo)
                    {
                        category.Transport.Limit = Convert.ToDouble(limit.Text);
                        category.Transport.isShow = true;
                    }
                    await ReadWrite.saveStringToLocalFile("data", JsonSerilizer.ToJson(MyProfile));
                    Frame.Navigate(typeof(SettingsPage), new DrillInNavigationTransitionInfo());

                }
              
              
              



           
            }
        


        }
    }
}
