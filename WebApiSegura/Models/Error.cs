//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApiSegura.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Error
    {
        public int ERR_CODIGO { get; set; }
        public int USU_CODIGO { get; set; }
        public System.DateTime ERR_FEC_HORA { get; set; }
        public string ERR_FUENTE { get; set; }
        public string ERR_NUMERO { get; set; }
        public string ERR_DESCRIPCION { get; set; }
        public string ERR_VISTA { get; set; }
        public string ERR_ACCION { get; set; }
    
        public virtual Usuario Usuario { get; set; }
    }
}
