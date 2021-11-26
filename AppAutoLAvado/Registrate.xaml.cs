using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppAutoLAvado
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registrate : ContentPage
    {
        Consultas consultas = new Consultas();
        public Registrate()
        {
            InitializeComponent();
        }

        private async void btnAcptar_Clicked(object sender, EventArgs e)
        {

            Boolean validar = false;

            if (String.IsNullOrWhiteSpace(txtNombre.Text))
            {
                validar = true;
            }

            if (String.IsNullOrWhiteSpace(txtApellido.Text))
            {
                validar = true;
            }

            if (String.IsNullOrWhiteSpace(txtCi.Text))
            {
                validar = true;

            }

            if (String.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                validar = true;
            }

            if (String.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                validar = true;

            }

            if (String.IsNullOrWhiteSpace(txtPassword.Text))
            {
                validar = true;
            }

            if (String.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                validar = true;
            }

            if (String.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                validar = true;
            }

            if (validar)
            {
                await this.DisplayAlert("Advertencia", "Tiene Campos vacios complete la informacion para continuar", "OK");
            }
            else if (consultas.GetClienteByCedula(txtCi.Text) != null)
            {
                await this.DisplayAlert("Advertencia", "El Cliente ya se encuentra registrado", "OK");
            }
            else if (consultas.GetUserByNick(txtUsuario.Text) != null)
            {
                await this.DisplayAlert("Advertencia", "El nombre de usuario ya se encuentra registrado", "OK");
            }
            else
            {
                User user = new User();
                user.nickUsr = txtUsuario.Text;
                user.passUsr = txtPassword.Text;
                user.tipoUsr = "Cliente";
                user.estadoUsr = true;

                if (consultas.GetUserGuardar(user))
                {
                    User userGrd = consultas.GetUserByNick(user.nickUsr);

                    Cliente cliente = new Cliente();
                    cliente.nombre = txtNombre.Text;
                    cliente.apellido = txtApellido.Text;
                    cliente.cedula = txtCi.Text;
                    cliente.correo = txtCorreo.Text;
                    cliente.direccion = txtDireccion.Text;
                    cliente.telefono = txtTelefono.Text;
                    cliente.estadoCliente = true;
                    cliente.usuario = userGrd;

                    if (consultas.GetClientGuardar(cliente))
                    {
                        await Navigation.PushAsync(new Login());
                    }
                }
            }
        }
    }
}