using Datos.Interfaces;
//using Datos.Models;
using Entidades;
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
        /// <summary>
        /// 1. Obtiene la Entidad de la DB en base al obj recibido
        /// 2. Verifica que no sea null
        /// 3. Obtiene la Entidad Persona si es que tiene
        /// 4. Si es que tiene Entidad Persona (es diferente de null), pone el estado de esa entidad persona como eliminado
        /// 5. Verifico si tiene telefonos y los borro
        /// 6. Luego pone a la entidad principal su estado en eliminado
        /// 7. Guarda cambios y retorna true
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Delete(string RUC)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    var cliente_db = db.Cliente.FirstOrDefault(x => x.RUC == RUC);

                    if(cliente_db == null)
                    {
                        return false;
                    }

                    var persona_db = cliente_db.Persona;

                    if (persona_db != null)
                    {

                        db.Entry(persona_db).State = System.Data.Entity.EntityState.Deleted;

                        if(persona_db.Telefono != null)
                        {
                            var x = persona_db.Telefono;

                            db.Telefono.RemoveRange(x);
                        }
                    }

                    db.Entry(cliente_db).State = System.Data.Entity.EntityState.Deleted;

                 
                    db.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public IEnumerable<Cliente> GetAll()
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                return db.Cliente.Include("Persona.Telefono").ToList();
            }
        }

        /// <summary>
        /// 1. Verifico que el obj pasado tenga su PK
        /// 2. Verifico que ese obj con ese PK no exista en la DB, o sea que sea nuevo
        /// 3. Agrego el objeto al contexto del Cliente
        /// 4. Verifico si su FK DNI no este null o blanca, mejor dicho ver si tiene una entidad persona relacionada
        /// 5. Si es que tiene, verifico que su objeto persona no sea nulo y lo agrego al contexto de Persona
        /// 6. Guarda cambios y retorna true
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Insert(Cliente obj)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    if (!String.IsNullOrWhiteSpace(obj.RUC))
                    {
                        if (!db.Cliente.ToList().Exists(x => x.RUC == obj.RUC))
                        {
                            db.Cliente.Add(obj);

                            if (!String.IsNullOrWhiteSpace(obj.DNI))
                            {
                                if (obj.Persona != null)
                                {
                                    db.Persona.Add(obj.Persona);
                                }
                                else
                                {
                                    obj.Persona = new Persona() { DNI = obj.DNI };
                                    db.Persona.Add(obj.Persona);
                                }

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
        /// 2. Obtengo la Entidad con ese PK de la DB
        /// 3. si no es null (es decir si existe ese objeto en la db), mapeo la el obj pasado con la entidad obtenida de la db
        /// 4. Verifico si es que su PK DNI no esta vacia
        /// 5. Si el objeto persona es diferente de null, paso a verificar si la entidad traida de la DB tiene una entidad persona
        /// 6. Si no tiene, agrego el nuevo objeto persona al contexto. Si es que tiene, modifico los datos de esa entidad
        /// 7. Guarda cambios y retorna true
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Update(Cliente obj)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    if (!String.IsNullOrWhiteSpace(obj.RUC))
                    {
                        var obj_db = db.Cliente.FirstOrDefault(x => x.RUC == obj.RUC);
                        System.Threading.Thread.Sleep(500);
                        if (!(obj_db is null))
                        {
                            if (!String.IsNullOrWhiteSpace(obj.Direccion))
                            {
                                obj_db.Direccion = obj.Direccion;
                            }

                            if (!String.IsNullOrWhiteSpace(obj.Razon_Social))
                            {
                                obj_db.Razon_Social = obj.Razon_Social;
                            }

                            if (String.IsNullOrWhiteSpace(obj_db.DNI))
                            {
                                obj_db.DNI = obj.DNI;
                            }

                            if (!String.IsNullOrWhiteSpace(obj.DNI))
                            {
                                if (obj.Persona != null)
                                {

                                    if(obj_db.Persona is null)
                                    {
                                        db.Persona.Add(obj.Persona);
                                        //obj_db.Persona = obj.Persona;
                                    }
                                    else
                                    {//SI O SI TENGO QUE VERIFICAR PARA EVITAR LA SOBREESCRITURA Y PERDIDA DE INFORMACION, DIGAMOS QUE AL TRAER LA ENTIDAD, YA VENGA CON UN DATO QUE AL MOMENTO DE CARGAR
                                     //LA APP ERA NULL, PERO AHORA YA NO, CON LOS IF EVITO ESA PERDIDA DE INFO

                                        //MAPEO
                                        if (!String.IsNullOrWhiteSpace(obj.Persona.Nombre))
                                        {
                                            obj_db.Persona.Nombre = obj.Persona.Nombre;
                                        }

                                        if (!String.IsNullOrWhiteSpace(obj.Persona.Apellido))
                                        {
                                            obj_db.Persona.Apellido = obj.Persona.Apellido;
                                        }

                                        if (!String.IsNullOrWhiteSpace(obj.Persona.Fecha_Nac.ToString()))
                                        {
                                            obj_db.Persona.Fecha_Nac = obj.Persona.Fecha_Nac;
                                        }

                                        if (!String.IsNullOrWhiteSpace(obj.Persona.Nacionalidad))
                                        {
                                            obj_db.Persona.Nacionalidad = obj.Persona.Nacionalidad;
                                        }
                                    }
                                }
                                else
                                {
                                    return false;
                                }
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
                catch (Exception ex ) { return false; }
            }
        }
    }
}
