using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public partial class Telefono
    {
        [NotMapped]
        public string NumeroAntiguo { get; set; }

    }
    #region Backup Entity Telefono
    /*
         public partial class Telefono : BindableBase
    {
        private string numero;
        public string Numero
        {
            get { return numero; }
            set { SetProperty(ref numero, value); }
        }


        private string dni;
        public string DNI
        {
            get { return dni; }
            set { SetProperty(ref dni, value); }
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
    }

     */
    //FECHA: 17/07/20
    #endregion
}
