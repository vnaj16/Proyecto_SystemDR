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
                try
                {
                    var conductor_db = db.Conductor.FirstOrDefault(x => x.Dni == Dni);

                    if (conductor_db == null)
                    {
                        return false;
                    }

                    var DniNavigation_db = conductor_db.DniNavigation;

                    if (DniNavigation_db is null)
                    {
                        return false;
                    }

                    var telefonos_db = DniNavigation_db.Telefono;

                    if (telefonos_db != null)
                    {
                        db.Telefono.RemoveRange(telefonos_db);
                    }

                    db.Persona.Remove(DniNavigation_db);
                    db.Conductor.Remove(conductor_db);

                    db.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public IEnumerable<Conductor> GetAll()
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                return db.Conductor.Include(x=>x.DniNavigation).ThenInclude(x=>x.Telefono).Include(x=>x.Historial).ToList();
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
                try
                {
                    if (!String.IsNullOrWhiteSpace(obj.Dni))
                    {
                        if (!db.Conductor.ToList().Exists(x => x.Dni == obj.Dni))
                        {
                            db.Conductor.Add(obj);

                            if (obj.DniNavigation != null)
                            {
                                db.Persona.Add(obj.DniNavigation);
                            }
                            else
                            {
                                obj.DniNavigation = new Persona() { Dni = obj.Dni, Tipo = "con" };
                                db.Persona.Add(obj.DniNavigation);
                            }

                            db.SaveChanges();

                            return true;

                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception ex) { return false; }
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
                try
                {
                    if (!String.IsNullOrWhiteSpace(obj.Dni))
                    {
                        var obj_db = db.Conductor.FirstOrDefault(x => x.Dni == obj.Dni);

                        if(obj_db is null) return false;

                        if (!String.IsNullOrWhiteSpace(obj.Brevete)) obj_db.Brevete = obj.Brevete;

                        if (!String.IsNullOrWhiteSpace(obj.Direccion)) obj_db.Direccion = obj.Direccion;
                        
                        if (!String.IsNullOrWhiteSpace(obj.Direccion)) obj_db.Direccion = obj.Direccion;

                        if (!String.IsNullOrWhiteSpace(obj.FechaInicio.ToString())) obj_db.FechaInicio = obj.FechaInicio;

                        if (!String.IsNullOrWhiteSpace(obj.GradoInstruccion)) obj_db.GradoInstruccion = obj.GradoInstruccion;

                        if (!String.IsNullOrWhiteSpace(obj.LugarNac)) obj_db.LugarNac = obj.LugarNac;

                        if (!String.IsNullOrWhiteSpace(obj.DniNavigation.Nacionalidad)) obj_db.DniNavigation.Nacionalidad = obj.DniNavigation.Nacionalidad;

                        //MAPEO Persona
                        if (!String.IsNullOrWhiteSpace(obj.DniNavigation.Nombre)) obj_db.DniNavigation.Nombre = obj.DniNavigation.Nombre;

                        if (!String.IsNullOrWhiteSpace(obj.DniNavigation.Apellido)) obj_db.DniNavigation.Apellido = obj.DniNavigation.Apellido;

                        if (!String.IsNullOrWhiteSpace(obj.DniNavigation.FechaNac.ToString())) obj_db.DniNavigation.FechaNac = obj.DniNavigation.FechaNac;

                        if (!String.IsNullOrWhiteSpace(obj.DniNavigation.Nacionalidad)) obj_db.DniNavigation.Nacionalidad = obj.DniNavigation.Nacionalidad;


                        db.SaveChanges();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex) { return false; }
            }

        }
    }
}