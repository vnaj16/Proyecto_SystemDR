using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public partial class Cliente
    {
        public string MyPropExtra { get; set; } = "Esta es una prop extra";

        public override string ToString()
        {
            return $"{RUC} {Razon_Social}";
        }
    }

    #region Backup Entity Cliente
    /*
     public partial class Cliente : BindableBase
    {
                [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cliente()
        {
            this.DocumentoViaje = new HashSet<DocumentoViaje>();
        }

        private string ruc;

        [Required(ErrorMessage = "El campo RUC es obligatorio")]
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "El RUC debe tener 10 digitos")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Solo números")]
        public string RUC
        {
            get { return ruc; }
            set { SetProperty(ref ruc, value); }
        }



        private string razon_Social;

        public string Razon_Social
        {
            get => razon_Social;
            set => SetProperty(ref razon_Social, value);
        }


        private string direccion;
        public string Direccion 
        {
            get => direccion;
            set => SetProperty(ref direccion, value);
        }


        private string tipo;
        public string Tipo 
        {
            get => tipo;
            set => SetProperty(ref tipo, value);
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



        private Persona persona;
        public virtual Persona Persona
        {
            get => persona;
            set
            {
                SetProperty(ref persona, value);
            }
        }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DocumentoViaje> DocumentoViaje { get; set; }
    }
     */

    //FECHA: 07/07/20
    #endregion
}
