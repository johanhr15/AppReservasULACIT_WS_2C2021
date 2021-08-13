using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservasULACIT.Models
{
    public class Escala
    {
        public int ESC_CODIGO { get; set; }
        public int ESC_NUMERO_TERMINAL { get; set; }
        public int ESC_ARP_CODIGO { get; set; }
        public System.DateTime ESC_TIEMPO_ESPERA { get; set; }
        public string ESC_TRASBORDO { get; set; }
    }
}