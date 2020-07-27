//using Datos.Models;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        bool HasRepresent(string ID, out string IdRepresent);
    }
}
