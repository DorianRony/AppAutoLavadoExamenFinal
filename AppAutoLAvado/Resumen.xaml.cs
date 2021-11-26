using AppAutoLAvado.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.OpenWhatsApp;
using Xamarin.Forms.Xaml;

namespace AppAutoLAvado
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Resumen : ContentPage
    {
        ReservaClass reservaClass { get; set; }
        User userLog { get; set; }
        public Resumen(User user,ReservaClass reserva, bool ver)
        {
            InitializeComponent();
            reservaClass = reserva;
            userLog = user;
            lblFecha.Text = reserva.fecha.ToString("dd-MM-yyyy HH:mm:ss");
            lblPrecio.Text = reserva.valor.ToString();
            lblServicios.Text = reserva.tipoLavado;

            if (ver)
            {
                lblTituloPedido.Text = "Pedido N° "+ reserva.idReserva + " Cliente "+ reserva.cliente.nombre.ToString();
            }
            else
            {
                lblTituloPedido.Text = reserva.cliente.nombre.ToString() + " Su pedido ha sido Procesado con exito";
            }
        }

        private async void btnSubmit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Reservas(userLog));
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (userLog.tipoUsr.Equals("EMPLEADO"))
                {
                    string sufijo = "+593";
                    string telefono = reservaClass.cliente.telefono;

                    string celular = sufijo + telefono.Substring(1);

                    Chat.Open(celular, "Hola, le saluda un asesor " +
                        "de Lavadora de Autos Israel para confirmar su reserva para nuestro servicio de " + reservaClass.tipoLavado +
                        " a las " + reservaClass.fecha + " con un valor " + reservaClass.valor + " Dolares");
                }
                else
                {
                    Chat.Open("+34747414885", "Hola, mi nombre es " + reservaClass.cliente.nombre + " Necesito informacion de mi reserva n° " + reservaClass.idReserva.ToString());
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}