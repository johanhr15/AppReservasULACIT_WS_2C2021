using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservasULACIT.Models
{
    public class Pasajero
    {
        public int PAS_CODIGO { get; set; }
        public string PAS_PASAPORTE { get; set; }
        public string PAS_NOMBRE { get; set; }
        public System.DateTime PAS_FEC_NACIMIENTO { get; set; }
        public string PAS_NACIONALIDAD { get; set; }
        public string PAS_CORREO { get; set; }
        public string PAS_TELEFONO { get; set; }
    }
}