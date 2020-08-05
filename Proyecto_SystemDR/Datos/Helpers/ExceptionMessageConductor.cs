using Datos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Helpers
{
    public class ExceptionMessageConductor : IExceptionMessage
    {
        public string AlreadyExists(string ID)
        {
            return $"Ya existe en la Base de Datos un conductor con el DNI {ID}";
        }

        public string DoesNotExist(string ID)
        {
            return $"No existe en la Base de Datos un conductor con el DNI {ID}";
        }

        public string KeyIsNull()
        {
            return "El DNI está vacio";
        }
    }
}
