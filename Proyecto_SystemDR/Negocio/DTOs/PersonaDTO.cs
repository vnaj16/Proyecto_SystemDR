using Negocio.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTOs
{
    public class PersonaDTO
    {
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Nullable<System.DateTime> Fecha_Nac { get; set; }
        public string Nacionalidad { get; set; }
        public string Tipo { get; set; }

        public virtual List<TelefonoDTO> Telefono { get; set; }

        public int Edad
        {
            get
            {
                return AgeCalculator.GetAge(Fecha_Nac.Value, DateTime.Now);
            }
            private set { }
        }

        public override string ToString()
        {
            return $"{DNI}: {Nombre}  ({Fecha_Nac.Value.ToShortDateString()} - {Edad} años)";
        }
    }


}
