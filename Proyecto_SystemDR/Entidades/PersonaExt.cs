using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public partial class Persona
    {
        [NotMapped]
        public string FullName { get { return Nombre + ' ' + Apellido; } }

        [NotMapped]
        public string ListTelefonos
        {
            get
            {
                StringBuilder x = new StringBuilder();
                int i = 0;

                foreach (var t in Telefono)
                {
                    if (i < 1)
                    {
                        x.Append($"{t.Numero} ");
                        i++;
                    }
                    else if (i < 2)
                    {
                        x.Append($"/ {t.Numero} / ");
                        i++;
                    }
                    else
                    {
                        x.Append("...");
                        break;
                    }
                }

                return x.ToString();
            }
        }
    }

    #region Backup Entity Persona

    /*
        public partial class Persona : BindableBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Persona()
        {
            this.Cliente = new HashSet<Cliente>();
            this.Proveedor = new HashSet<Proveedor>();
            this.Telefono = new HashSet<Telefono>();
        }


        private string dni;
        public string DNI
        {
            get => dni;
            set
            {
                SetProperty(ref dni, value);
            }
        }

        private string nombre;
        public string Nombre
        {
            get => nombre;
            set
            {
                SetProperty(ref nombre, value);
            }
        }

        private string apellido;
        public string Apellido
        {
            get => apellido;
            set
            {
                SetProperty(ref apellido, value);
            }
        }

        private Nullable<System.DateTime> fecha_Nac;
        public Nullable<System.DateTime> Fecha_Nac
        {
            get => fecha_Nac;
            set
            {
                SetProperty(ref fecha_Nac, value);
            }
        }

        private string nacionalidad;
        public string Nacionalidad
        {
            get => nacionalidad;
            set
            {
                SetProperty(ref nacionalidad, value);
            }
        }

        private string tipo;
        public string Tipo
        {
            get => tipo;
            set
            {
                SetProperty(ref tipo, value);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cliente> Cliente { get; set; }
        public virtual Conductor Conductor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Proveedor> Proveedor { get; set; }


        private ICollection<Telefono> telefono;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<Telefono> Telefono
        {
            get => telefono;
            set
            {
                SetProperty(ref telefono, value);
            }
        }
    }
     */

    //FECHA: 07/07/20
    #endregion
}
