using System;
using System.Collections.Generic;

namespace Entidades
{
    public partial class Proveedor
    {
        public string Ruc { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Tipo { get; set; }
        public string Productos { get; set; }
        public string DniRl { get; set; }

        public virtual Persona DniRlNavigation { get; set; }
    }
}
