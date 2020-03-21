using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTOs
{
    public class TelefonoDTO
    {
        public string Numero { get; set; }
        public string DNI { get; set; }

        public virtual PersonaDTO Persona { get; set; }
    }
}
