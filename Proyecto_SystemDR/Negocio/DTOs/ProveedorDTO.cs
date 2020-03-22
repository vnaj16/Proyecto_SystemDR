using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTOs
{
    public class ProveedorDTO
    {
        public string RUC { get; set; }
        public string Razon_Social { get; set; }
        public string Direccion { get; set; }
        public string Tipo { get; set; }
        public string Productos { get; set; }
        public string DNI { get; set; }

        public virtual PersonaDTO Persona { get; set; }
    }
}
