using Datos.Interfaces;
using Datos.ModelsEFCore;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositories
{
    public class TelefonoRepository : ITelefonoRepository
    {
        public bool Delete(string Numero)
        {

            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {

                Telefono telefono_db = db.Telefono.FirstOrDefault(x => x.Numero == Numero);

                if (telefono_db == null)
                {
                    throw new Exception($"El telefono con numero {Numero} no existe en la base de datos");
                }

                db.Telefono.Remove(telefono_db);

                db.SaveChanges();

                return true;
            }

        }

        public bool Exists(string Numero)
        {

            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                return db.Telefono.ToList().Exists(x => x.Numero == Numero);
            }

        }

        public IEnumerable<Telefono> GetAll()
        {

            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                return db.Telefono.ToList();
            }
        }



        /// <summary>
        /// 1. Verifico que venga tanto el numero como el DNI del dueño
        /// 2. Verifico si no existe en la DB
        /// 3. Lo agrego al contexto de la Db y luego lo guardo
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Insert(Telefono obj)
        {

            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                Persona personaAux = null;

                if (!(obj.DniNavigation is null))
                {
                    personaAux = obj.DniNavigation;
                    obj.DniNavigation = null;
                }

                db.Telefono.Add(obj);

                db.SaveChanges();

                obj.DniNavigation = personaAux;

                return true;
            }



        }

        /// <summary>
        /// 1. Verifico que vengan los numeros y el DNI del owner
        /// 2. Elimino el numero anterior
        /// 3. Agrego el numero nuevo
        /// 4. Guardo
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Update(Telefono obj)
        {

            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                Telefono tlfAntiguo = db.Telefono.FirstOrDefault(x => x.Numero == obj.NumeroAntiguo);
                if (Delete(obj.NumeroAntiguo))
                {
                    return Insert(obj);
                }
                else
                {
                    throw new Exception("Ocurrio un error al actualizar telefono" + Environment.NewLine
                        + "Se recomienda mejor eliminar el número y agregar el nuevo");
                }
            }

        }
    }
}

