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
    
    public partial class Habitacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Habitacion()
        {
            this.Reserva = new HashSet<Reserva>();
        }
    
        public int HAB_CODIGO { get; set; }
        public int HOT_CODIGO { get; set; }
        public string HAB_NUMERO { get; set; }
        public int HAB_CAPACIDAD { get; set; }
        public string HAB_TIPO { get; set; }
        public string HAB_DESCRIPCION { get; set; }
        public string HAB_ESTADO { get; set; }
        public decimal HAB_PRECIO { get; set; }
    
        public virtual Hotel Hotel { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reserva> Reserva { get; set; }
    }
}
