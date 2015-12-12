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
using Windows.UI.Xaml.Navigation;
using CashMana.JsonSerializer;
using CashMana.Models;
using orioks;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CashMana.Controls
{
    public sealed partial class MyUserControl1 : UserControl
    {
        public MyUserControl1()
        {
            this.InitializeComponent();


        }

        public async static void AcessarSistema()
        {
            var acessar = new MyUserControl1();
            ContentDialog dialogo = new ContentDialog();

            dialogo.PrimaryButtonText = "Acessar";
            dialogo.PrimaryButtonClick += Tela_PrimaryButtonClick;



           

            dialogo.Content = acessar;

            await dialogo.ShowAsync();


        }

        private void hyperEsqueci_Click(object sender, RoutedEventArgs e)
        {

        }
     
        private async static void Tela_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var tela = sender.Content as MyUserControl1;
            var readDataa = await ReadWrite.readStringFromLocalFile("data");
            Profile profile = JsonSerilizer.ToProfile(readDataa);

            if (tela.passwordBoxSenha.Password == profile.Password)
            {

            }
            else
            {

                var acessar = new MyUserControl1();
                ContentDialog dialogo = new ContentDialog();

                dialogo.PrimaryButtonText = "Войти";
                dialogo.PrimaryButtonClick += Tela_PrimaryButtonClick;





                dialogo.Content = acessar;

                await dialogo.ShowAsync();


            }

        }
    }
}
