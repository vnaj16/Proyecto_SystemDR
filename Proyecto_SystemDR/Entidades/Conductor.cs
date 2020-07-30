using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public partial class Conductor : BindableBase
    {
        public Conductor()
        {
            Historial = new HashSet<Historial>();
            Viaje = new HashSet<Viaje>();
        }


        private string dni;

        [Required(ErrorMessage = "El campo DNI es obligatorio")]
        [StringLength(15, ErrorMessage = "Maximo son 15 caracteres")]
        public string Dni
        {
            get { return dni; }
            set { SetProperty(ref dni, value); }
        }


        private DateTime? fechaInicio;
        public DateTime? FechaInicio 
        {
            get { return fechaInicio; }
            set { SetProperty(ref fechaInicio, value); }
        }

        private string brevete;

        [StringLength(25, ErrorMessage = "Maximo son 25 caracteres")]
        public string Brevete {
            get { return brevete; }
            set { SetProperty(ref brevete, value); }
        }

        private string lugarNac;
        [StringLength(25, ErrorMessage = "Maximo son 25 caracteres")]
        public string LugarNac {
            get { return lugarNac; }
            set { SetProperty(ref lugarNac, value); }
        }

        private string gradoInstruccion;
        [StringLength(25, ErrorMessage = "Maximo son 25 caracteres")]
        public string GradoInstruccion {
            get { return gradoInstruccion; }
            set { SetProperty(ref gradoInstruccion, value); }
        }

        private string direccion;
        [StringLength(50, ErrorMessage = "Maximo son 50 caracteres")]
        public string Direccion {
            get { return direccion; }
            set { SetProperty(ref direccion, value); }
        }

        private string personalidad;
        [StringLength(150, ErrorMessage = "Maximo son 150 caracteres")]
        public string Personalidad {
            get { return personalidad; }
            set { SetProperty(ref personalidad, value); }
        }

        private Persona dniNavigation;
        public virtual Persona DniNavigation {
            get { return dniNavigation; }
            set { SetProperty(ref dniNavigation, value); }
        }

        private ICollection<Historial> historial;
        public virtual ICollection<Historial> Historial {
            get { return historial; }
            set { SetProperty(ref historial, value); }
        }

        private ICollection<Viaje> viaje;
        public virtual ICollection<Viaje> Viaje {
            get { return viaje; }
            set { SetProperty(ref viaje, value); }
        }
    }
}
