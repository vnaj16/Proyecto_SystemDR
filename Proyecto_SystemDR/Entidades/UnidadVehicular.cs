using System;
using System.Collections.Generic;

namespace Entidades
{
    public partial class UnidadVehicular
    {
        public UnidadVehicular()
        {
            Historial = new HashSet<Historial>();
            Viaje = new HashSet<Viaje>();
        }

        public string Placa { get; set; }
        public string Marca { get; set; }
        public string SerieChasis { get; set; }
        public int? YFabricacion { get; set; }

        public virtual ICollection<Historial> Historial { get; set; }
        public virtual ICollection<Viaje> Viaje { get; set; }
    }
}
