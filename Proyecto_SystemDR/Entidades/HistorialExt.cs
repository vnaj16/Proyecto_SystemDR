using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public partial class HistorialExt
    {
    }

    #region Backup Entity Historial
    /*
         public partial class Historial : BindableBase
    {
        private string dniConductor;

        [Required(ErrorMessage = "El campo dni es obligatorio")]
        [StringLength(15, ErrorMessage = "Error en el formato del dni", MinimumLength = 8)]
        public string DniConductor
        {
            get { return dniConductor; }
            set { SetProperty(ref dniConductor, value); }
        }

        private DateTime? fecha;
        public DateTime? Fecha
        {
            get { return fecha; }
            set { SetProperty(ref fecha, value); }
        }

        private string eventualidad;
        [StringLength(50, ErrorMessage = "Maximo son 50 caracteres")]
        public string Eventualidad
        {
            get { return eventualidad; }
            set { SetProperty(ref eventualidad, value); }
        }


        private string lugar;
        [StringLength(25, ErrorMessage = "Maximo son 25 caracteres")]
        public string Lugar
        {
            get { return lugar; }
            set { SetProperty(ref lugar, value); }
        }

        private string descripcion;
        [StringLength(150, ErrorMessage = "Maximo son 150 caracteres")]
        public string Descripcion
        {
            get { return descripcion; }
            set { SetProperty(ref descripcion, value); }
        }

        private string idUnidad;
        [StringLength(15, ErrorMessage = "Maximo son 15 caracteres")]
        public string IdUnidad
        {
            get { return idUnidad; }
            set { SetProperty(ref idUnidad, value); }
        }

        private int id;
        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        private Conductor dniConductorNavigation;
        public virtual Conductor DniConductorNavigation
        {
            get { return dniConductorNavigation; }
            set { SetProperty(ref dniConductorNavigation, value); }
        }

        private UnidadVehicular idUnidadNavigation;
        public virtual UnidadVehicular IdUnidadNavigation
        {
            get { return idUnidadNavigation; }
            set { SetProperty(ref idUnidadNavigation, value); }
        }
    }
    
    */

    //FECHA: 25/07/20
    #endregion
}
