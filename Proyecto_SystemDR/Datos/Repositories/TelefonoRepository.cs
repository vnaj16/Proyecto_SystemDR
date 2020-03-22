using Datos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositories
{
    public class TelefonoRepository
    {
        public IEnumerable<Telefono> GetAll()
        {
            using (TransporteDRContext db = new TransporteDRContext())
            {
                return db.Telefono.ToList();
            }
        }
    }
}
