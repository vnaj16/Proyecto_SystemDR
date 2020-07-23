using System;
using System.Collections.Generic;

namespace Entidades
{
    public partial class Telefono
    {
        public string Numero { get; set; }
        public string Dni { get; set; }

        public virtual Persona DniNavigation { get; set; }
    }
}
