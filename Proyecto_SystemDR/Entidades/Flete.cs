using System;
using System.Collections.Generic;

namespace Entidades
{
    public partial class Flete
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public double? Valor { get; set; }
        public double? Importe { get; set; }
        public double? Adelanto { get; set; }
        public double? Saldo { get; set; }
        public string IdViaje { get; set; }

        public virtual Viaje IdViajeNavigation { get; set; }
    }
}
