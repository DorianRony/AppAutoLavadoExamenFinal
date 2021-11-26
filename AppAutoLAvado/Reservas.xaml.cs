using AppAutoLAvado.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppAutoLAvado
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Reservas : ContentPage
    {
        Consultas consultas = new Consultas();      

        User usuario { get; set;}
        Cliente clienteLog { get; set;}
        public Reservas(User user)
        {
            InitializeComponent();
            usuario = user;
            if (user.tipoUsr.Equals("EMPLEADO")){
                lblReserva.Text = "Reservas de Clientes";
                List<ReservaClass> reservaClasses = consultas.GetReservaList();
                ListaReservas.ItemsSource = new ObservableCollection<ReservaClass>(reservaClasses);
                btnNuevo.IsVisible = false;
                lblTipo.Text = "EMPLEADO";
            }
            else
            {
                lblTipo.Text = "CLIENTE";
                Cliente cliente = consultas.GetClienteByUser(user.idUsr);
                clienteLog = cliente;
                btnNuevo.IsVisible = true;
                lblReserva.Text = "Reservas de "+cliente.nombre;
                List<ReservaClass> reservaClasses = consultas.GetReservaListCliente(cliente.idCliente);
                ListaReservas.ItemsSource = new ObservableCollection<ReservaClass>(reservaClasses);
            }
        }

        private async void ListaReservas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new Resumen(usuario, (ReservaClass) e.SelectedItem, true));
        }

        protected override bool OnBackButtonPressed()
        {
            // If you want to continue going back
            base.OnBackButtonPressed();
            //return false;

            // If you want to stop the back button
            return true;

        }

        private async void btnNuevo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Reserva(usuario, clienteLog));
        }

        private async void btnSalir_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }
    }
}