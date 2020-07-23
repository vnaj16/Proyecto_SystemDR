using System;
using System.Collections.Generic;

namespace Entidades
{
    public partial class Viaje
    {
        public Viaje()
        {
            Gasto = new HashSet<Gasto>();
            GastoV = new HashSet<GastoV>();
        }

        public string Id { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaSalida { get; set; }
        public string LugarOrigen { get; set; }
        public double? KmOrigen { get; set; }
        public DateTime? FechaLlegada { get; set; }
        public string LugarDestino { get; set; }
        public double? KmDestino { get; set; }
        public string Nota { get; set; }
        public string Encargado { get; set; }
        public string PlacaVehiculo { get; set; }
        public string DniConductor { get; set; }

        public virtual Conductor DniConductorNavigation { get; set; }
        public virtual UnidadVehicular PlacaVehiculoNavigation { get; set; }
        public virtual CuentasXrendir CuentasXrendir { get; set; }
        public virtual DocumentoViaje DocumentoViaje { get; set; }
        public virtual Flete Flete { get; set; }
        public virtual Mercaderia Mercaderia { get; set; }
        public virtual Resumen Resumen { get; set; }
        public virtual ICollection<Gasto> Gasto { get; set; }
        public virtual ICollection<GastoV> GastoV { get; set; }
    }
}
