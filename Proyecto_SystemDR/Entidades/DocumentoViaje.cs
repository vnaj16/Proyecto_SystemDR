using System;
using System.Collections.Generic;

namespace Entidades
{
    public partial class DocumentoViaje
    {
        public int Id { get; set; }
        public string Grr { get; set; }
        public string Grt { get; set; }
        public string Ruc { get; set; }
        public string Factura { get; set; }
        public string IdViaje { get; set; }

        public virtual Viaje IdViajeNavigation { get; set; }
        public virtual Cliente RucNavigation { get; set; }
    }
}
