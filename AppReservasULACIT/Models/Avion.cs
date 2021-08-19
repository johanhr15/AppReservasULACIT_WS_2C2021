using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservasULACIT.Models
{
    public class Avion
    {
        public int AVI_CODIGO { get; set; }
        public int AER_CODIGO { get; set; }
        public int ASI_CODIGO { get; set; }
        public string AVI_MODELO { get; set; }
        public string AVI_TIPO_RUTA { get; set; }
        public int AVI_CAPACIDAD { get; set; }
        public string AVI_EQUIPAJE { get; set; }
    }
}