using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Interfaces;
using Entidades;

namespace Datos.Repositories
{
    public class ConductorRepository : IConductorRepository
    {
        /// <summary>
        /// 1. Obtiene la Entidad de la DB en base al obj recibido
        /// 2. Verifica que no sea null
        /// 3. Obtiene la Entidad Persona
        /// 4. Pone el estado de esa entidad persona como eliminado
        /// 5. Verifico si tiene telefonos y los borro
        /// 6. Luego pone a la entidad principal su estado en eliminado
        /// 7. Guarda cambios y retorna true
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Delete(string DNI)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    var conductor_db = db.Conductor.FirstOrDefault(x => x.DNI == DNI);

                    if (conductor_db == null)
                    {
                        return false;
                    }

                    var persona_db = conductor_db.Persona;

                    if (persona_db is null)
                    {
                        return false;
                    }

                    var telefonos_db = persona_db.Telefono;

                    if (telefonos_db != null)
                    {
                        db.Telefono.RemoveRange(telefonos_db);
                    }

                    db.Entry(persona_db).State = System.Data.Entity.EntityState.Deleted;

                    db.Entry(conductor_db).State = System.Data.Entity.EntityState.Deleted;

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
                return db.Conductor.Include("Persona.Telefono").Include("Historial").ToList();
            }
        }

        /// <summary>
        /// 1. Verifico que el obj pasado tenga su PK
        /// 2. Verifico que ese obj con ese PK no exista en la DB, o sea que sea nuevo
        /// 3. Agrego el objeto al contexto del Conductor
        /// 4. Verifico que su objeto persona no sea nulo (si es nulo, creo uno) y lo agrego al contexto de Persona
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
                    if (!String.IsNullOrWhiteSpace(obj.DNI))
                    {
                        if (!db.Conductor.ToList().Exists(x => x.DNI == obj.DNI))
                        {
                            db.Conductor.Add(obj);

                            if (obj.Persona != null)
                            {
                                db.Persona.Add(obj.Persona);
                            }
                            else
                            {
                                obj.Persona = new Persona() { DNI = obj.DNI, Tipo = "con" };
                                db.Persona.Add(obj.Persona);
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
        /// 4. Obtengo la Entidad Persona con ese PK de la DB
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
                    if (!String.IsNullOrWhiteSpace(obj.DNI))
                    {
                        var obj_db = db.Conductor.FirstOrDefault(x => x.DNI == obj.DNI);

                        if(obj_db is null) return false;

                        if (!String.IsNullOrWhiteSpace(obj.Brevete)) obj_db.Brevete = obj.Brevete;

                        if (!String.IsNullOrWhiteSpace(obj.Direccion)) obj_db.Direccion = obj.Direccion;
                        
                        if (!String.IsNullOrWhiteSpace(obj.Direccion)) obj_db.Direccion = obj.Direccion;

                        if (!String.IsNullOrWhiteSpace(obj.Fecha_Inicio.ToString())) obj_db.Fecha_Inicio = obj.Fecha_Inicio;

                        if (!String.IsNullOrWhiteSpace(obj.Grado_Instruccion)) obj_db.Grado_Instruccion = obj.Grado_Instruccion;

                        if (!String.IsNullOrWhiteSpace(obj.Lugar_Nac)) obj_db.Lugar_Nac = obj.Lugar_Nac;

                        if (!String.IsNullOrWhiteSpace(obj.Personalidad)) obj_db.Personalidad = obj.Personalidad;

                        //MAPEO PERSONA
                        if (!String.IsNullOrWhiteSpace(obj.Persona.Nombre)) obj_db.Persona.Nombre = obj.Persona.Nombre;

                        if (!String.IsNullOrWhiteSpace(obj.Persona.Apellido)) obj_db.Persona.Apellido = obj.Persona.Apellido;

                        if (!String.IsNullOrWhiteSpace(obj.Persona.Fecha_Nac.ToString())) obj_db.Persona.Fecha_Nac = obj.Persona.Fecha_Nac;

                        if (!String.IsNullOrWhiteSpace(obj.Persona.Nacionalidad)) obj_db.Persona.Nacionalidad = obj.Persona.Nacionalidad;


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