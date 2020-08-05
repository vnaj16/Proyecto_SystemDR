using Datos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Helpers
{
    public class ExceptionMessageCliente : IExceptionMessage
    {
        public string AlreadyExists(string ID)
        {
            return $"Ya existe en la Base de Datos un cliente con el RUC {ID}";
        }

        public string DoesNotExist(string ID)
        {
            return $"No existe en la Base de Datos un cliente con el RUC {ID}";
        }

        public string KeyIsNull()
        {
            return "El RUC está vacio";
        }
    }
}
