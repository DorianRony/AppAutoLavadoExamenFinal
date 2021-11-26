using System;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AppAutoLAvado
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();

            /*MainPage = new NavigationPage(new Login());*/
        }


        private async void btnRegistrate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registrate());
        }


        private async void btnLogIn_Clicked(object sender, EventArgs e)
        {
            try
            {
                string user = txtUsuario.Text;
                string pass = txtPassword.Text;
                if (!String.IsNullOrWhiteSpace(user) && !String.IsNullOrWhiteSpace(pass))
                {
                    Consultas consultas = new Consultas();
                    User usuario = consultas.GetUserlogin(user, pass);

                    if (usuario != null)
                    {
                        await Navigation.PushAsync(new Reservas(usuario));
                        //await Navigation.PushAsync(new Reserva(usuario, cliente));

                    }
                    else
                    {
                        DisplayAlert("Alert", "Credenciales Incorrectas", "OK");
                    }
                }
                else
                {
                    DisplayAlert("Alert", "Credenciales Incorrectas", "OK");
                }

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }


        }
    }
}