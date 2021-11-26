using AppAutoLAvado.Modelos;
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
    public partial class Reserva : ContentPage
    {
        User usuario { get; set; }     
        Cliente cliente { get; set; }     
        public Reserva(User user, Cliente client)
        {
            InitializeComponent();
            usuario = user;
            cliente = client;
            fechaPicker.MinimumDate = DateTime.Now;
            fechaPicker.Date = DateTime.Now.AddDays(1);
            timePicker.Time = DateTime.Now.TimeOfDay;
            DateTime dateTime = fechaDateTime(fechaPicker.Date, timePicker.Time);
            lblFecha.Text = dateTime.ToString("dd-MM-yyyy HH:mm:ss");
        }

        private DateTime fechaDateTime(DateTime date, TimeSpan time)
        {
            return new DateTime(date.Year, date.Month, date.Day,time.Hours,time.Minutes,time.Seconds);
        }

        private void fechaPicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            DateTime dateTime = fechaDateTime(fechaPicker.Date, timePicker.Time);
            lblFecha.Text = dateTime.ToString("dd-MM-yyyy HH:mm:ss");
        }

        private void timePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            DateTime dateTime = fechaDateTime(fechaPicker.Date, timePicker.Time);
            lblFecha.Text = dateTime.ToString("dd-MM-yyyy HH:mm:ss");
        }

        private async void btnAcptar_Clicked(object sender, EventArgs e)
        {
            if(timePicker.Time.Hours > 18 || timePicker.Time.Hours < 8) {
                await DisplayAlert("Hora","Debe escoger una hora entre las 8am y las 6pm", "ok");
                return;
            }

            float precio = 0;
            string mensaje="";
            if (chLavCompleto.IsChecked)
            {
                mensaje = "Lavado completo";
                precio += 10;
            }
            else if (chLavadoExpres.IsChecked)
            {
                mensaje = "Lavado express";
                precio += 3;
            }
            else if (chLavadoNormal.IsChecked)
            {
                mensaje = "Lavado normal";
                precio += 5;
            }

            if (chAceite.IsChecked)
            {
                precio += 25;
            }

            string fecha = lblFecha.Text;

            bool answer = await DisplayAlert("El precio de tu reserva es de: " + precio + "Dolares", "Desea confirmar su reserva?", "Si", "No");
            if (answer)
            {
                DateTime dateTime = fechaDateTime(fechaPicker.Date, timePicker.Time);

                ReservaClass reserva = new ReservaClass();
                reserva.cambioAceite = chAceite.IsChecked;
                reserva.fecha = dateTime;
                reserva.estadoReserva = true;
                reserva.tipoLavado = mensaje;
                reserva.valor = precio;
                reserva.cliente = cliente;
                
                Consultas consultas = new Consultas();
                if (consultas.GetReservaGuardar(reserva))
                {
                    await Navigation.PushAsync(new Resumen(usuario,reserva,false));
                }               
            }
        }
    }
}