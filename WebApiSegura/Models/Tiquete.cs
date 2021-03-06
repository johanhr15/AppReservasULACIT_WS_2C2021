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
    
    public partial class Tiquete
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
    
        public virtual Aerolinea Aerolinea { get; set; }
        public virtual Escala Escala { get; set; }
        public virtual Pasajero Pasajero { get; set; }
        public virtual Reserva_Vuelo Reserva_Vuelo { get; set; }
    }
}
