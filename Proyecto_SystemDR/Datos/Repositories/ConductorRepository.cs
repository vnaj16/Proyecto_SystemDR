using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Interfaces;
using Datos.ModelsEFCore;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace Datos.Repositories
{
    public class ConductorRepository : IConductorRepository
    {
        /// <summary>
        /// 1. Obtiene la Entidad de la DB en base al obj recibido
        /// 2. Verifica que no sea null
        /// 3. Obtiene la Entidad DniNavigation
        /// 4. Pone el estado de esa entidad DniNavigation como eliminado
        /// 5. Verifico si tiene telefonos y los borro
        /// 6. Luego pone a la entidad principal su estado en eliminado
        /// 7. Guarda cambios y retorna true
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Delete(string Dni)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {

                var conductor_db = db.Conductor.FirstOrDefault(x => x.Dni == Dni);

                if (conductor_db == null)
                {
                    throw new Exception($"El conductor con dni {Dni} no existe en la base de datos");
                }

                db.Conductor.Remove(conductor_db);

                db.SaveChanges();

                return true;
            }
        }

        public bool Exists(string Dni)
        {

            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                return db.Conductor.ToList().Exists(x => x.Dni == Dni);
            }


        }

        public IEnumerable<Conductor> GetAll()
        {

            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                var Lista = db.Conductor.Include(x => x.DniNavigation).ThenInclude(x => x.Telefono).Include(x => x.Historial).ToList();

                return Lista;
            }


        }

        /// <summary>
        /// 1. Verifico que el obj pasado tenga su PK
        /// 2. Verifico que ese obj con ese PK no exista en la DB, o sea que sea nuevo
        /// 3. Agrego el objeto al contexto del Conductor
        /// 4. Verifico que su objeto DniNavigation no sea nulo (si es nulo, creo uno) y lo agrego al contexto de DniNavigation
        /// 5. Guarda cambios y retorna true
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Insert(Conductor obj)
        {

            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                Persona personaAux = null;
                if (!(obj.DniNavigation is null))
                {
                    personaAux = obj.DniNavigation;
                    obj.DniNavigation = null;
                }

                db.Conductor.Add(obj);

                db.SaveChanges();


                obj.DniNavigation = personaAux;

                return true;
            }


        }

        /// <summary>
        /// 1. Verifico que el obj pasado tenga su PK
        /// 2. Obtengo la Entidad Conductor con ese PK de la DB
        /// 3. si no es null (es decir si existe ese objeto en la db), mapeo la el obj pasado con la entidad obtenida de la db
        /// 4. Obtengo la Entidad DniNavigation con ese PK de la DB
        /// 5. mapeo la el obj pasado con la entidad obtenida de la db
        /// 6. Guarda cambios y retorna true
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Update(Conductor obj)
        {

            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                var obj_db = db.Conductor.FirstOrDefault(x => x.Dni == obj.Dni);

                if (obj_db is null)
                {
                    throw new Exception($"El conductor con Dni {obj.Dni} no existe en la Base de Datos");
                }

                if (!String.IsNullOrWhiteSpace(obj.Brevete)) obj_db.Brevete = obj.Brevete;

                if (!String.IsNullOrWhiteSpace(obj.Direccion)) obj_db.Direccion = obj.Direccion;

                if (!String.IsNullOrWhiteSpace(obj.Direccion)) obj_db.Direccion = obj.Direccion;

                if (!String.IsNullOrWhiteSpace(obj.FechaInicio.ToString())) obj_db.FechaInicio = obj.FechaInicio;

                if (!String.IsNullOrWhiteSpace(obj.GradoInstruccion)) obj_db.GradoInstruccion = obj.GradoInstruccion;

                if (!String.IsNullOrWhiteSpace(obj.LugarNac)) obj_db.LugarNac = obj.LugarNac;

                db.SaveChanges();

                return true;
            }

        }
    }
}