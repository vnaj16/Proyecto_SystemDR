using Datos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositories
{
    public class ConductorRepository
    {
        public IEnumerable<Conductor> GetAll()
        {
            using (TransporteDRContext db = new TransporteDRContext())
            {
                return db.Conductor.Include("Persona.Telefono").ToList();
            }
        }

    }
}
