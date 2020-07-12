using Datos.Repositories;
using Negocio.Business_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Core
{
    public static class TransporteDR
    {
        public static int x = 0;

        private static ClienteBO clienteBO;
        private static TelefonoBO telefonoBO;
        private static ConductorBO conductorBO;
        private static UnidadVehicularBO unidadVehicularBO;
        private static HistorialBO historialBO;

        static TransporteDR()
        {
            Initialize();
        }

        private static void Initialize()
        {
            clienteBO = new ClienteBO(new ClienteRepository());
            telefonoBO = new TelefonoBO(new TelefonoRepository());
            conductorBO = new ConductorBO(new ConductorRepository());
            unidadVehicularBO = new UnidadVehicularBO(new UnidadVehicularRepository());
            historialBO = new HistorialBO(new HistorialRepository());
        }

        public static ClienteBO ClienteBO
        {
            get
            {
                if (clienteBO == null)
                {
                    clienteBO = new ClienteBO(new ClienteRepository());
                }

                return clienteBO;
            }
        }

        public static TelefonoBO TelefonoBO
        {
            get
            {
                if (telefonoBO == null)
                {
                    telefonoBO = new TelefonoBO(new TelefonoRepository());
                }

                return telefonoBO;
            }
        }

        public static ConductorBO ConductorBO
        {
            get
            {
                if (conductorBO == null)
                {
                    conductorBO = new ConductorBO(new ConductorRepository());
                }

                return conductorBO;
            }
        }

        public static UnidadVehicularBO UnidadVehicularBO
        {
            get
            {
                if (unidadVehicularBO == null)
                {
                    unidadVehicularBO = new UnidadVehicularBO(new UnidadVehicularRepository());
                }

                return unidadVehicularBO;
            }
        }

        public static HistorialBO HistorialBO
        {
            get
            {
                if (historialBO == null)
                {
                    historialBO = new HistorialBO(new HistorialRepository());
                }

                return historialBO;
            }
        }
    }
}
