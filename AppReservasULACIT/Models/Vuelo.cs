using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservasULACIT.Models
{
    public class Vuelo
    {
        public int VUE_CODIGO { get; set; }
        public Nullable<int> AER_CODIGO { get; set; }
        public int VUE_ORI_CODIGO { get; set; }
        public int VUE_DES_CODIGO { get; set; }
        public string VUE_TERMINAL { get; set; }
        public string VUE_PUERTA { get; set; }
        public System.DateTime VUE_HORA_PARTIDA { get; set; }
        public System.DateTime VUE_HORA_LLEGADA { get; set; }
    }
}