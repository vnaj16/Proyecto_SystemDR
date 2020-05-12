using Datos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositories
{
    public class UnidadVehicularRepository
    {
        public IEnumerable<Unidad_Vehicular> GetAll()
        {
            using (TransporteDRContext db = new TransporteDRContext())
            {
                return db.Unidad_Vehicular.ToList();
            }
        }
    }
}
