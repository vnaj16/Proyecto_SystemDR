//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class Mercaderia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Mercaderia()
        {
            this.Viaje1 = new HashSet<Viaje>();
        }
    
        public int ID { get; set; }
        public string Producto { get; set; }
        public string Bultos { get; set; }
        public Nullable<double> Peso { get; set; }
        public string Unidad { get; set; }
        public string ID_Viaje { get; set; }
    
        public virtual Viaje Viaje { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Viaje> Viaje1 { get; set; }
    }
}