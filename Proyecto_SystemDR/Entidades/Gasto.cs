using System;
using System.Collections.Generic;

namespace Entidades
{
    public partial class Gasto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
        public double? Importe { get; set; }
        public string IdViaje { get; set; }

        public virtual Viaje IdViajeNavigation { get; set; }
    }
}
