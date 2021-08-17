using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservasULACIT.Models
{
    public class Error
    {
        public int ERR_CODIGO { get; set; }
        public int USU_CODIGO { get; set; }
        public System.DateTime ERR_FEC_HORA { get; set; }
        public string ERR_FUENTE { get; set; }
        public string ERR_NUMERO { get; set; }
        public string ERR_DESCRIPCION { get; set; }
        public string ERR_VISTA { get; set; }
        public string ERR_ACCION { get; set; }
    }
}