using Datos.Interfaces;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Business_Objects
{
    public class TelefonoBO
    {
        private ITelefonoRepository telefonoRepository;

        public TelefonoBO(ITelefonoRepository telefonoRepository)
        {
            this.telefonoRepository = telefonoRepository;
        }

        public bool Registrar(Telefono obj)
        {
            if (!String.IsNullOrWhiteSpace(obj.Numero) && !String.IsNullOrWhiteSpace(obj.DNI))
            {
                return telefonoRepository.Insert(obj);
            }
            else
            {
                return false;
            }
        }
    }
}
