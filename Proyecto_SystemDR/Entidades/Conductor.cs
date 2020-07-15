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
    using Prism.Mvvm;
    using System;
    using System.Collections.Generic;

    public partial class Conductor : BindableBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Conductor()
        {
            this.Historial = new HashSet<Historial>();
            this.Viaje = new HashSet<Viaje>();
        }

        private string dni;
        public string DNI
        {
            get { return dni; }
            set { SetProperty(ref dni, value); }
        }

        public Nullable<System.DateTime> Fecha_Inicio { get; set; }
        public string Brevete { get; set; }
        public string Lugar_Nac { get; set; }
        public string Grado_Instruccion { get; set; }
        public string Direccion { get; set; }
        public string Personalidad { get; set; }

        public virtual Persona Persona { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Historial> Historial { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Viaje> Viaje { get; set; }
    }
}
