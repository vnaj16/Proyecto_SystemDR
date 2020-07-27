using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public partial class Proveedor
    {
    }
    #region Backup Entity Cliente
    /*
         public partial class Proveedor : BindableBase
    {
        private string ruc;

        [Required(ErrorMessage = "El campo RUC es obligatorio")]
        [StringLength(15, ErrorMessage = "Error en el formato del RUC", MinimumLength = 8)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Solo números")]
        public string Ruc
        {
            get { return ruc; }
            set { SetProperty(ref ruc, value); }
        }


        private string razonSocial;

        [StringLength(30, ErrorMessage = "Maximo son 30 caracteres")]
        public string RazonSocial
        {
            get { return razonSocial; }
            set { SetProperty(ref razonSocial, value); }
        }


        private string direccion;

        [StringLength(50, ErrorMessage = "Maximo son 50 caracteres")]
        public string Direccion
        {
            get { return direccion; }
            set { SetProperty(ref direccion, value); }
        }


        private string tipo;

        [StringLength(50, ErrorMessage = "Maximo son 50 caracteres")]
        public string Tipo
        {
            get { return tipo; }
            set { SetProperty(ref tipo, value); }
        }

        private string productos;

        [StringLength(100, ErrorMessage = "Maximo son 100 caracteres")]
        public string Productos
        {
            get { return productos; }
            set { SetProperty(ref productos, value); }
        }


        private string dniRl;
        [StringLength(15, ErrorMessage = "Maximo son 15 caracteres")]
        public string DniRl
        {
            get { return dniRl; }
            set { SetProperty(ref dniRl, value); }
        }


        private Persona dniRlNavigation;
        public virtual Persona DniRlNavigation
        {
            get { return dniRlNavigation; }
            set { SetProperty(ref dniRlNavigation, value); }
        }
    }
     */

    //FECHA: 26/07/20
    #endregion
}
