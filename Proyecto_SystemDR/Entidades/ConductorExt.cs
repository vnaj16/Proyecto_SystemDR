using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public partial class Conductor: BindableBase
    {
        void p (){ Lugar_Nac}
    }
    #region Backup Entity Conductor
    /*public partial class Conductor : BindableBase
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
    }*/
    #endregion
}
