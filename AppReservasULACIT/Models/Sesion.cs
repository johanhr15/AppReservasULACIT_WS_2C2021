using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservasULACIT.Models
{
    public class Sesion
    {
        public int SES_CODIGO { get; set; }
        public int USU_CODIGO { get; set; }
        public System.DateTime SES_FEC_HORA_INICIO { get; set; }
        public System.DateTime SES_FEC_HORA_FIN { get; set; }
        public char SES_ESTADO { get; set; }
    }
}