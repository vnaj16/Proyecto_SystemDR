using Datos.Interfaces;
using Datos.ModelsEFCore;
using Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Datos.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        /*data source=ARTHUR\VNAJ_DB01;initial catalog=dbTransporteDR;integrated security=True;*/
        /// <summary>
        /// 1. Obtiene la Entidad de la DB en base al obj recibido
        /// 2. Verifica que no sea null
        /// 3. Obtiene la Entidad DniRlNavigation si es que tiene
        /// 4. Si es que tiene Entidad DniRlNavigation (es diferente de null), pone el estado de esa entidad DniRlNavigation como eliminado
        /// 5. Verifico si tiene telefonos y los borro
        /// 6. Luego pone a la entidad principal su estado en eliminado
        /// 7. Guarda cambios y retorna true
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Delete(string Ruc)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {

                var cliente_db = db.Cliente.FirstOrDefault(x => x.Ruc == Ruc);

                if (cliente_db == null)
                {
                    throw new Exception($"El cliente con ruc {Ruc} no existe en la base de datos");
                }

                db.Cliente.Remove(cliente_db);

                db.SaveChanges();

                return true;
            }
        }

        public bool Exists(string Ruc)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                return db.Cliente.ToList().Exists(x => x.Ruc == Ruc);
            }
        }

        public IEnumerable<Cliente> GetAll()
        {//Aca podria ir un contador y determinar si se consulta varias veces, aplicar singleton para simular cache
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                return db.Cliente.Include(x => x.DniRlNavigation).ThenInclude(x => x.Telefono).ToList();
            }
        }

        public bool HasRepresent(string RUC, out string IdRepresent)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                var representante = db.Cliente.Include(x => x.DniRlNavigation).FirstOrDefault(x => x.Ruc == RUC).DniRlNavigation;

                if (representante is null)
                {
                    IdRepresent = null;
                    return false;
                }
                IdRepresent = representante.Dni;

                return true;
            }
        }

        /// <summary>
        /// 1. Verifico que el obj pasado tenga su PK
        /// 2. Verifico que ese obj con ese PK no exista en la DB, o sea que sea nuevo
        /// 3. Agrego el objeto al contexto del Cliente
        /// 4. Verifico si su FK DNI no este null o blanca, mejor dicho ver si tiene una entidad DniRlNavigation relacionada
        /// 5. Si es que tiene, verifico que su objeto DniRlNavigation no sea nulo y lo agrego al contexto de DniRlNavigation
        /// 6. Guarda cambios y retorna true
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Insert(Cliente obj)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                Persona personaAux = null;
                if (!(obj.DniRlNavigation is null))
                {
                    personaAux = obj.DniRlNavigation;
                    obj.DniRlNavigation = null;
                }

                db.Cliente.Add(obj);

                db.SaveChanges();

                obj.DniRlNavigation = personaAux;

                return true;
            }
        }

        /// <summary>
        /// 1. Verifico que el obj pasado tenga su PK
        /// 2. Obtengo la Entidad con ese PK de la DB
        /// 3. si no es null (es decir si existe ese objeto en la db), mapeo la el obj pasado con la entidad obtenida de la db
        /// 4. Verifico si es que su PK DNI no esta vacia
        /// 5. Si el objeto DniRlNavigation es diferente de null, paso a verificar si la entidad traida de la DB tiene una entidad DniRlNavigation
        /// 6. Si no tiene, agrego el nuevo objeto DniRlNavigation al contexto. Si es que tiene, modifico los datos de esa entidad
        /// 7. Guarda cambios y retorna true
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Update(Cliente obj)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                var cliente_db = db.Cliente.FirstOrDefault(x => x.Ruc == obj.Ruc);
                if (!(cliente_db is null))
                {
                    if (!String.IsNullOrWhiteSpace(obj.Direccion))
                    {
                        cliente_db.Direccion = obj.Direccion;
                    }
                    if (!String.IsNullOrWhiteSpace(obj.RazonSocial))
                    {
                        cliente_db.RazonSocial = obj.RazonSocial;
                    }
                    if (!String.IsNullOrWhiteSpace(obj.Tipo))
                    {
                        cliente_db.Tipo = obj.Tipo;
                    }
                    if (!String.IsNullOrWhiteSpace(obj.DniRl))
                    {
                        cliente_db.DniRl = obj.DniRl;
                    }


                    db.SaveChanges();

                    return true;
                }
                else
                {
                    throw new Exception($"El cliente con ruc {obj.Ruc} no existe en la Base de Datos");
                }
            }
        }
    }
}
