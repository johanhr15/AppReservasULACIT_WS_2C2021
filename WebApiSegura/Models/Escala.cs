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
    
    public partial class Escala
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Escala()
        {
            this.Tiquete = new HashSet<Tiquete>();
        }
    
        public int ESC_CODIGO { get; set; }
        public int ESC_NUMERO_TERMINAL { get; set; }
        public int ESC_ARP_CODIGO { get; set; }
        public System.DateTime ESC_TIEMPO_ESPERA { get; set; }
        public string ESC_TRASBORDO { get; set; }
    
        public virtual Aeropuerto Aeropuerto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tiquete> Tiquete { get; set; }
    }
}
