using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using AppAutoLAvado.Modelos;

namespace AppAutoLAvado
{
    internal class Consultas
    {
        //private static string preUri = "http://192.168.0.3:8099";
        private static string preUri = "http://172.16.24.35:8099";
        public User GetUserlogin(String user, String pass)
        {
            HttpClient client = new HttpClient();
            string Url = preUri+"/usuario/login/" + user + "/" + pass;
            var content = client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<User>(content.Result);
        }

        public User GetUserByNick(String user)
        {
            HttpClient client = new HttpClient();
            string Url = preUri + "/usuario/buscarnick/" + user;
            var content = client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<User>(content.Result);
        }

        public Cliente GetClienteByCedula(String cedula)
        {
            HttpClient client = new HttpClient();
            string Url = preUri + "/cliente/buscarcedula/" + cedula;
            var content = client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<Cliente>(content.Result);
        }
        
        public Cliente GetClienteByUser(int idUser)
        {
            HttpClient client = new HttpClient();
            string Url = preUri + "/cliente/buscarporusuario/" + idUser;
            var content = client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<Cliente>(content.Result);
        }

        public List<ReservaClass> GetReservaList()
        {
            HttpClient client = new HttpClient();
            string Url = preUri + "/reserva/listar";
            var content = client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<List<ReservaClass>>(content.Result);
        }

        public List<ReservaClass> GetReservaListCliente(int idCliente)
        {
            HttpClient client = new HttpClient();
            string Url = preUri + "/reserva/buscarporcliente/"+idCliente;
            var content = client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<List<ReservaClass>>(content.Result);
        }

        public Boolean GetUserGuardar(User user)
        {
            try
            {
                HttpClient client = new HttpClient();
                string Url = preUri + "/usuario/guardar";

                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var content = client.PostAsync(Url, httpContent).Result;
                if (content.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Boolean GetClientGuardar(Cliente cliente)
        {
            HttpClient client = new HttpClient();
            string Url = preUri + "/cliente/guardar";
            
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var content = client.PostAsync(Url, httpContent).Result;
            if (content.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public Boolean GetReservaGuardar(ReservaClass reserva)
        {
            HttpClient client = new HttpClient();
            string Url = preUri + "/reserva/guardar";

            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(reserva), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var content = client.PostAsync(Url, httpContent).Result;
            if (content.IsSuccessStatusCode){
                return true;
            }

            return false;
        }
    }
}
