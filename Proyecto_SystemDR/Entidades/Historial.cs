using System;
using System.Collections.Generic;

namespace Entidades
{
    public partial class Historial
    {
        public string DniConductor { get; set; }
        public DateTime? Fecha { get; set; }
        public string Eventualidad { get; set; }
        public string Lugar { get; set; }
        public string Descripcion { get; set; }
        public string IdUnidad { get; set; }
        public int Id { get; set; }

        public virtual Conductor DniConductorNavigation { get; set; }
        public virtual UnidadVehicular IdUnidadNavigation { get; set; }
    }
}
