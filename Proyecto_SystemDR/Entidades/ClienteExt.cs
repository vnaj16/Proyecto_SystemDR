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
        private string ruc;

        [Required(ErrorMessage ="El campo RUC es obligatorio")]
        [StringLength(maximumLength:20, MinimumLength =6,ErrorMessage ="El RUC debe tener 10 digitos")]
        [RegularExpression("^[0-9]+$")]
        public string RUC 
        {
            get { return ruc; }
            set { SetProperty(ref ruc, value); } 
        }


        public string Razon_Social { get; set; }
        public string Direccion { get; set; }
        public string Tipo { get; set; }

        [StringLength(15)]
        public string DNI { get; set; }
    
        public virtual Persona Persona { get; set; }
    }
     */

    //FECHA: 28/06/20
    #endregion
}
