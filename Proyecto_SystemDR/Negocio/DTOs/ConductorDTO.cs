using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTOs
{
    public class ConductorDTO
    {
        public string DNI { get; set; }
        public Nullable<System.DateTime> Fecha_Inicio { get; set; }
        public string Brevete { get; set; }
        public string Lugar_Nac { get; set; }
        public string Grado_Instruccion { get; set; }
        public string Direccion { get; set; }
        public string Personalidad { get; set; }

        public virtual PersonaDTO Persona { get; set; }

        public virtual ICollection<HistorialDTO> Historial { get; set; }
    }
}
