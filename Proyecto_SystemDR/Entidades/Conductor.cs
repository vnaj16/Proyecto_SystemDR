using System;
using System.Collections.Generic;

namespace Entidades
{
    public partial class Conductor
    {
        public Conductor()
        {
            Historial = new HashSet<Historial>();
            Viaje = new HashSet<Viaje>();
        }

        public string Dni { get; set; }
        public DateTime? FechaInicio { get; set; }
        public string Brevete { get; set; }
        public string LugarNac { get; set; }
        public string GradoInstruccion { get; set; }
        public string Direccion { get; set; }
        public string Personalidad { get; set; }

        public virtual Persona DniNavigation { get; set; }
        public virtual ICollection<Historial> Historial { get; set; }
        public virtual ICollection<Viaje> Viaje { get; set; }
    }
}
