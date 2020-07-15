using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Helpers
{
    public enum FilterTypeSearchCliente { RUC, RazonSocial, DNI }; //Quiza busqueda tambien por representante
    public enum FilterTypeSearchProveedor { RUC, RazonSocial, DNI, Productos }; //Quiza busqueda tambien por representante
    public enum FilterTypeSearchConductor { DNI, Brevete, Nombre, Apellido };
    public enum FilterTypeSearchVehiculo { Placa, Marca };
    public enum FilterTypeSearchHistorial { DNI, Eventualidad, Lugar, Placa };

}
