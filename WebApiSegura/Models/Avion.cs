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
    
    public partial class Avion
    {
        public int AVI_CODIGO { get; set; }
        public int AER_CODIGO { get; set; }
        public int ASI_CODIGO { get; set; }
        public string AVI_MODELO { get; set; }
        public string AVI_TIPO_RUTA { get; set; }
        public int AVI_CAPACIDAD { get; set; }
        public string AVI_EQUIPAJE { get; set; }
    
        public virtual Aerolinea Aerolinea { get; set; }
        public virtual Asiento Asiento { get; set; }
    }
}
