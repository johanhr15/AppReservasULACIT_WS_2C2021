using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservasULACIT.Models
{
    public class Estadistica
    {
        public int EST_CODIGO { get; set; }
        public int USU_CODIGO { get; set; }
        public System.DateTime EST_FEC_HORA { get; set; }
        public string EST_NAVEGADOR { get; set; }
        public string EST_PLATAFORMA_DISPOSITIVO { get; set; }
        public string EST_FABRICANTE_DISPOSITIVO { get; set; }
        public string EST_VISTA { get; set; }
        public string EST_ACCION { get; set; }
    }
}