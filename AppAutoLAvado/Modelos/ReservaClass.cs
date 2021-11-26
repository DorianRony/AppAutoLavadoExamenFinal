using System;
using System.Collections.Generic;
using System.Text;

namespace AppAutoLAvado.Modelos
{
    public class ReservaClass
    {
        public int idReserva { get; set; }
        public string tipoLavado { get; set; }
        public bool cambioAceite { get; set; }
        public DateTime fecha { get; set; }
        public bool estadoReserva { get; set; }
        public double valor { get; set; }
        public Cliente cliente { get; set; }
    }
}
