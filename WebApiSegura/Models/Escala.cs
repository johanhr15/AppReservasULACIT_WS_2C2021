//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
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
