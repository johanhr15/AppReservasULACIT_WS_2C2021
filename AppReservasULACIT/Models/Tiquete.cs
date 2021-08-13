using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservasULACIT.Models
{
    public class Tiquete
    {
        public int TIQ_CODIGO { get; set; }
        public int AER_CODIGO { get; set; }
        public int ESC_CODIGO { get; set; }
        public int PAS_CODIGO { get; set; }
        public int VUE_CODIGO { get; set; }
        public int RVU_CODIGO { get; set; }
        public decimal TIQ_PRECIO { get; set; }
        public string TIQ_ALIMENTACION { get; set; }
        public string TIQ_DEVOLUCION { get; set; }
        public string TIQ_VISA_REQUERIDA { get; set; }
    }
}