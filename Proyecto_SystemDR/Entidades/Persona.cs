using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public partial class Persona : BindableBase
    {
        public Persona()
        {
            Cliente = new HashSet<Cliente>();
            Proveedor = new HashSet<Proveedor>();
            Telefono = new HashSet<Telefono>();
        }

        private string dni;
        [StringLength(15, ErrorMessage = "Error en el formato del DNI", MinimumLength = 8)]
        [Required(ErrorMessage = "DNI Requerido")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Solo números")]
        public string Dni
        {
            get { return dni; }
            set { SetProperty(ref dni, value); }
        }

        private string nombre;
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Nombre
        {
            get { return nombre; }
            set { SetProperty(ref nombre, value); }
        }

        private string apellido;
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Apellido
        {
            get { return apellido; }
            set { SetProperty(ref apellido, value); }
        }

        private DateTime? fechaNac;
        public DateTime? FechaNac
        {
            get { return fechaNac; }
            set { SetProperty(ref fechaNac, value); }
        }

        private string nacionalidad;
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Nacionalidad
        {
            get { return nacionalidad; }
            set { SetProperty(ref nacionalidad, value); }
        }


        private string tipo;
        [Required]
        [StringLength(3, ErrorMessage = "Error en el tipo")]
        public string Tipo
        {
            get { return tipo; }
            set { SetProperty(ref tipo, value); }
        }


        private Conductor conductor;
        public virtual Conductor Conductor
        {
            get { return conductor; }
            set { SetProperty(ref conductor, value); }
        }
        public virtual ICollection<Cliente> Cliente
        {
            get; set;
        }
        public virtual ICollection<Proveedor> Proveedor
        {
            get; set;
        }

        private ICollection<Telefono> telefono;
        public virtual ICollection<Telefono> Telefono
        {
            get { return telefono; }
            set { SetProperty(ref telefono, value); }
        }
    }
}
