using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTOs
{
    public class ConductorDTO
    {
        public ConductorDTO()
        {
            ImageUrl = "https://cdn.pixabay.com/photo/2018/05/11/23/45/highway-3392100__340.jpg";
        }

        public string DNI { get; set; }
        public Nullable<System.DateTime> Fecha_Inicio { get; set; }
        public string Brevete { get; set; }
        public string Lugar_Nac { get; set; }
        public string Grado_Instruccion { get; set; }
        public string Direccion { get; set; }
        public string Personalidad { get; set; }

        public string ImageUrl;

        public virtual PersonaDTO Persona { get; set; }

        public virtual IEnumerable<HistorialDTO> Historial { get; set; }
    }
}
