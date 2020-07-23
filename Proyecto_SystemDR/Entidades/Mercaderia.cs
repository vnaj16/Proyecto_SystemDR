using System;
using System.Collections.Generic;

namespace Entidades
{
    public partial class Mercaderia
    {
        public int Id { get; set; }
        public string Producto { get; set; }
        public string Bultos { get; set; }
        public double? Peso { get; set; }
        public string Unidad { get; set; }
        public string IdViaje { get; set; }

        public virtual Viaje IdViajeNavigation { get; set; }
    }
}
