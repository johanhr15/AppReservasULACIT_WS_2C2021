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
    
    public partial class Pasajero
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pasajero()
        {
            this.Tiquete = new HashSet<Tiquete>();
        }
    
        public int PAS_CODIGO { get; set; }
        public string PAS_PASAPORTE { get; set; }
        public string PAS_NOMBRE { get; set; }
        public System.DateTime PAS_FEC_NACIMIENTO { get; set; }
        public string PAS_NACIONALIDAD { get; set; }
        public string PAS_CORREO { get; set; }
        public string PAS_TELEFONO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tiquete> Tiquete { get; set; }
    }
}
