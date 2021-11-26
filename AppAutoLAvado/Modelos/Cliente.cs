using System;
using System.Collections.Generic;
using System.Text;

namespace AppAutoLAvado
{
    public class Cliente
    {
        public int idCliente { get; set; }
        public string apellido { get; set; }
        public string cedula { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public bool estadoCliente { get; set; }
        public string nombre { get; set; }
        public User usuario { get; set; }
    }
}
