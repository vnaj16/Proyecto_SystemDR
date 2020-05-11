using Datos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositories
{
    public class ClienteRepository
    {
        public IEnumerable<Cliente> GetAll()
        {
            using (TransporteDRContext db = new TransporteDRContext())
            {
                return db.Cliente.Include("Persona.Telefono").ToList();
            }
        }

        public bool Register(Cliente obj, out int state_code)
        {
            using (TransporteDRContext db = new TransporteDRContext())
            {
                try
                {
                    if (!db.Cliente.ToList().Exists(x=>x.RUC == obj.RUC))
                    {
                        db.Cliente.Add(obj);
                        db.SaveChanges();

                        state_code = 100;

                        return true;
                    }
                    else
                    {
                        state_code = 111;
                        return false;
                    }


                }
                catch(Exception ex) { state_code = 191; return false; }
            }
        }
    }
}

////////////CODIGOS DE ESTADO/////////////
/////Exito: 100
////Registro Duplicado: 111
////Error inesperado: 191

