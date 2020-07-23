using System;
using System.Collections.Generic;

namespace Entidades
{
    public partial class GastoV
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public DateTime? Fecha { get; set; }
        public double? Km { get; set; }
        public string Documento { get; set; }
        public string RazonSocial { get; set; }
        public double? Importe { get; set; }
        public string IdViaje { get; set; }

        public virtual Viaje IdViajeNavigation { get; set; }
    }
}
