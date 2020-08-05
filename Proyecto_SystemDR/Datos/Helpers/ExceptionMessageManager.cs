using Datos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Helpers
{
    public static class ExceptionMessageManager
    {
        static ExceptionMessageManager()
        {
            ExceptionMessageCliente = new ExceptionMessageCliente();
            ExceptionMessageConductor = new ExceptionMessageConductor();
            ExceptionMessageProveedor = new ExceptionMessageProveedor();
            ExceptionMessageVehiculo = new ExceptionMessageVehiculo();
            ExceptionMessageHistorial = new ExceptionMessageHistorial();
        }

        public static IExceptionMessage ExceptionMessageCliente;
        public static IExceptionMessage ExceptionMessageConductor;
        public static IExceptionMessage ExceptionMessageProveedor;
        public static IExceptionMessage ExceptionMessageVehiculo;
        public static IExceptionMessage ExceptionMessageHistorial;
    }
}
