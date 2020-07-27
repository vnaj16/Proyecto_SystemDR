using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public partial class Cliente
    {
        [NotMapped]
        public int Edad { get; set; }

        /*[NotMapped]
        public string RucNuevo { get; set; }*/

        public override string ToString()
        {
            return $"{Ruc} {RazonSocial}";
        }
    }

    #region Backup Entity Cliente
    /*public partial class Cliente : BindableBase
    {
        public Cliente()
        {
            DocumentoViaje = new HashSet<DocumentoViaje>();
        }

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

        [StringLength(25, ErrorMessage = "Maximo son 25 caracteres")]
        public string Tipo
        {
            get { return tipo; }
            set { SetProperty(ref tipo, value); }
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
        public virtual ICollection<DocumentoViaje> DocumentoViaje { get; set; }
    }*/

    //FECHA: 25/07/20
    #endregion
}
