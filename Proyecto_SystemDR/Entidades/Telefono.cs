using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public partial class Telefono : BindableBase
    {
        private string numero;
        [Required(ErrorMessage = "El campo Numero es obligatorio")]
        [StringLength(20, ErrorMessage = "Maximo 20 digitos")]
        [Phone]
        public string Numero {
            get { return numero; }
            set { SetProperty(ref numero, value); }
        }

        private string dni;
        [Required(ErrorMessage = "El campo dni es obligatorio")]
        [StringLength(15, ErrorMessage = "Maximo 15 digitos")]
        public string Dni {
            get { return dni; }
            set { SetProperty(ref dni, value); }
        }

        private Persona dniNavigation;
        [Required(ErrorMessage = "No hay referencia a Persona")]
        public virtual Persona DniNavigation {
            get { return dniNavigation; }
            set { SetProperty(ref dniNavigation, value); }
        }
    }
}
