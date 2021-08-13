using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservasULACIT.Models
{
    public class ReservaVuelo
    {
        public int RVU_CODIGO { get; set; }
        public int USU_CODIGO { get; set; }
        public int AGE_CODIGO { get; set; }
        public string RVU_MONEDA { get; set; }
        public decimal RVU_PRECIO_TOTAL { get; set; }
        public System.DateTime RVU_FECHA { get; set; }
    }
}

