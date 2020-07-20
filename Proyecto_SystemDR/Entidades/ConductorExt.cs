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
    }
    #region Backup Entity Conductor
    /*
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

        private Nullable<System.DateTime> fecha_Inicio;
        public Nullable<System.DateTime> Fecha_Inicio
        {
            get { return fecha_Inicio; }
            set { SetProperty(ref fecha_Inicio, value); }
        }

        private string brevete;
        public string Brevete
        {
            get { return brevete; }
            set { SetProperty(ref brevete, value); }
        }

        private string lugar_Nac;
        public string Lugar_Nac
        {
            get { return lugar_Nac; }
            set { SetProperty(ref lugar_Nac, value); }
        }

        private string grado_Instruccion;
        public string Grado_Instruccion
        {
            get { return grado_Instruccion; }
            set { SetProperty(ref grado_Instruccion, value); }
        }


        private string direccion;
        public string Direccion
        {
            get { return direccion; }
            set { SetProperty(ref direccion, value); }
        }

        private string personalidad;
        public string Personalidad
        {
            get { return personalidad; }
            set { SetProperty(ref personalidad, value); }
        }

        private Persona persona;
        public virtual Persona Persona
        {
            get { return persona; }
            set { SetProperty(ref persona, value); }
        }

        private ICollection<Historial> historial;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Historial> Historial
        {
            get { return historial; }
            set { SetProperty(ref historial, value); }
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Viaje> Viaje { get; set; }
    }
    */

    //FECHA: 17/07/20
    #endregion
}
