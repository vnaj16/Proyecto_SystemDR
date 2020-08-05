using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public partial class UnidadVehicular : BindableBase
    {
        public UnidadVehicular()
        {
            Historial = new HashSet<Historial>();
            Viaje = new HashSet<Viaje>();
        }

        private string placa;

        [Required(ErrorMessage = "El campo Placa es obligatorio")]
        [StringLength(15, ErrorMessage = "Máximo 15 caracteres")]
        public string Placa
        {
            get { return placa; }
            set { SetProperty(ref placa, value); }
        }

        private string marca;

        [StringLength(25, ErrorMessage = "Máximo 25 caracteres")]
        public string Marca
        {
            get { return marca; }
            set { SetProperty(ref marca, value); }
        }

        private string serieChasis;
        [StringLength(25, ErrorMessage = "Máximo 25 caracteres")]
        public string SerieChasis
        {
            get { return serieChasis; }
            set { SetProperty(ref serieChasis, value); }
        }

        private int? yFabricacion;
        
        [RegularExpression("^[0-9]+$", ErrorMessage = "Solo números")]
        public int? YFabricacion
        {
            get { return yFabricacion; }
            set { SetProperty(ref yFabricacion, value); }
        }

        private ICollection<Historial> historial;
        public virtual ICollection<Historial> Historial
        {
            get { return historial; }
            set { SetProperty(ref historial, value); }
        }

        private ICollection<Viaje> viaje;
        public virtual ICollection<Viaje> Viaje
        {
            get { return viaje; }
            set { SetProperty(ref viaje, value); }
        }
    }
}
