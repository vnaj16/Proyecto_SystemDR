using System;
using System.Collections.Generic;

namespace Entidades
{
    public partial class CuentasXrendir
    {
        public int Id { get; set; }
        public string FleteEntregadoA { get; set; }
        public double? Cantidad { get; set; }
        public string Lugar { get; set; }
        public string IdViaje { get; set; }

        public virtual Viaje IdViajeNavigation { get; set; }
    }
}
