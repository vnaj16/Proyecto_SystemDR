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
        private static ClienteBO clienteBO;


        static TransporteDR()
        {
            Initialize();
        }

        public static void Initialize()
        {
            clienteBO = new ClienteBO(new ClienteRepository());
        }

        public static ClienteBO ClienteBO
        {
            get
            {
                if(clienteBO == null)
                {
                    clienteBO = new ClienteBO(new ClienteRepository());
                }

                return clienteBO;
            }
        }
    }
}
