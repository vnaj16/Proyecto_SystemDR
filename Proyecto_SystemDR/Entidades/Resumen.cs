using System;
using System.Collections.Generic;

namespace Entidades
{
    public partial class Resumen
    {
        public int Id { get; set; }
        public double? BolsaViaje { get; set; }
        public string BolsaViajeD { get; set; }
        public double? GastoTotal { get; set; }
        public double? SaldoFavor { get; set; }
        public string SaldoFavorD { get; set; }
        public double? DeudaAntFavor { get; set; }
        public string DeudaAntFavorD { get; set; }
        public string IdViaje { get; set; }

        public virtual Viaje IdViajeNavigation { get; set; }
    }
}
