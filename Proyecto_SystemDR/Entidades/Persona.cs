using System;
using System.Collections.Generic;

namespace Entidades
{
    public partial class Persona
    {
        public Persona()
        {
            Cliente = new HashSet<Cliente>();
            Proveedor = new HashSet<Proveedor>();
            Telefono = new HashSet<Telefono>();
        }

        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNac { get; set; }
        public string Nacionalidad { get; set; }
        public string Tipo { get; set; }

        public virtual Conductor Conductor { get; set; }
        public virtual ICollection<Cliente> Cliente { get; set; }
        public virtual ICollection<Proveedor> Proveedor { get; set; }
        public virtual ICollection<Telefono> Telefono { get; set; }
    }
}
