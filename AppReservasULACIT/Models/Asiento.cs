using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservasULACIT.Models
{
    public class Asiento
    {
        public int ASI_CODIGO { get; set; }
        public string ASI_FILA { get; set; }
        public string ASI_LETRA { get; set; }
        public string ASI_DESCRIPCION { get; set; }
        public string ASI_CLASE { get; set; }
    }
}