using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservasULACIT.Models
{
    public class Aeropuerto
    {
        public int ARP_CODIGO { get; set; }
        public string ARP_PAIS { get; set; }
        public string ARP_CIUDAD { get; set; }
        public string ARP_ZONA_HORARIA { get; set; }
        public string ARP_VISA { get; set; }
        public string ARP_CONTROL_VACUNAS { get; set; }
    }
}