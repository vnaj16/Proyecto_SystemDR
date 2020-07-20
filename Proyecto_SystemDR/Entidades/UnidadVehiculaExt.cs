using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public partial class Unidad_Vehicular
    {
        public string FullName
        {
            get
            {
                return Placa + ' ' + Marca;
            }
        }
    }

    #region Backup Entity Unidad_Vehicular
    /*
         public partial class Unidad_Vehicular : BindableBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Unidad_Vehicular()
        {
            this.Historial = new HashSet<Historial>();
            this.Viaje = new HashSet<Viaje>();
        }

        private string placa;

        public string Placa
        {
            get => placa;
            set => SetProperty(ref placa, value);
        }

        private string marca;

        public string Marca
        {
            get => marca;
            set => SetProperty(ref marca, value);
        }

        private string serie_Chasis;

        public string Serie_Chasis
        {
            get => serie_Chasis;
            set => SetProperty(ref serie_Chasis, value);
        }

        private Nullable<int> y_Fabricacion;

        public Nullable<int> Y_Fabricacion
        {
            get => y_Fabricacion;
            set => SetProperty(ref y_Fabricacion, value);
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
