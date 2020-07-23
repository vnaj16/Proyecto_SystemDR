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
                try
                {
                    var cliente_db = db.Cliente.FirstOrDefault(x => x.Ruc == Ruc);

                    if(cliente_db == null)
                    {
                        return false;
                    }

                    var DniRlNavigation_db = cliente_db.DniRlNavigation;

                    if (DniRlNavigation_db != null)
                    {
                        var telefonos_db = DniRlNavigation_db.Telefono;

                        if (telefonos_db != null)
                        {
                            db.Telefono.RemoveRange(telefonos_db);
                        }

                        db.Persona.Remove(DniRlNavigation_db);
                    }

                    db.Cliente.Remove(cliente_db);

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
        {//Aca podria ir un contador y determinar si se consulta varias veces, aplicar singleton para simular cache
            try
            {
                using (dbTransporteDRContext db = new dbTransporteDRContext())
                {
                    return db.Cliente.Include(x=>x.DniRlNavigation).ThenInclude(x=>x.Telefono).ToList();
                }
            }
            catch (Exception)
            {
                throw;
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
                try
                {
                    if (!String.IsNullOrWhiteSpace(obj.Ruc))
                    {
                        if (!db.Cliente.ToList().Exists(x => x.Ruc == obj.Ruc))
                        {
                            db.Cliente.Add(obj);

                            if (!String.IsNullOrWhiteSpace(obj.DniRl))
                            {
                                if (obj.DniRlNavigation != null)
                                {
                                    db.Persona.Add(obj.DniRlNavigation);
                                }
                                else
                                {
                                    obj.DniRlNavigation = new Persona() { Dni = obj.DniRl, Tipo="cli"};
                                    db.Persona.Add(obj.DniRlNavigation);
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
                try
                {
                    if (!String.IsNullOrWhiteSpace(obj.Ruc))
                    {
                        var obj_db = db.Cliente.FirstOrDefault(x => x.Ruc == obj.Ruc);
                        System.Threading.Thread.Sleep(500);
                        if (!(obj_db is null))
                        {
                            if (!String.IsNullOrWhiteSpace(obj.Direccion))
                            {
                                obj_db.Direccion = obj.Direccion;
                            }

                            if (!String.IsNullOrWhiteSpace(obj.RazonSocial))
                            {
                                obj_db.RazonSocial = obj.RazonSocial;
                            }

                            if (String.IsNullOrWhiteSpace(obj_db.DniRl))
                            {
                                obj_db.DniRl = obj.DniRl;
                            }

                            if (!String.IsNullOrWhiteSpace(obj.DniRl))
                            {
                                if (obj.DniRlNavigation != null)
                                {

                                    if(obj_db.DniRlNavigation is null)
                                    {
                                        db.Persona.Add(obj.DniRlNavigation);
                                        //obj_db.DniRlNavigation = obj.DniRlNavigation;
                                    }
                                    else
                                    {//SI O SI TENGO QUE VERIFICAR PARA EVITAR LA SOBREESCRITURA Y PERDIDA DE INFORMACION, DIGAMOS QUE AL TRAER LA ENTIDAD, YA VENGA CON UN DATO QUE AL MOMENTO DE CARGAR
                                     //LA APP ERA NULL, PERO AHORA YA NO, CON LOS IF EVITO ESA PERDIDA DE INFO

                                        //MAPEO
                                        if (!String.IsNullOrWhiteSpace(obj.DniRlNavigation.Nombre))
                                        {
                                            obj_db.DniRlNavigation.Nombre = obj.DniRlNavigation.Nombre;
                                        }

                                        if (!String.IsNullOrWhiteSpace(obj.DniRlNavigation.Apellido))
                                        {
                                            obj_db.DniRlNavigation.Apellido = obj.DniRlNavigation.Apellido;
                                        }

                                        if (!String.IsNullOrWhiteSpace(obj.DniRlNavigation.FechaNac.ToString()))
                                        {
                                            obj_db.DniRlNavigation.FechaNac = obj.DniRlNavigation.FechaNac;
                                        }

                                        if (!String.IsNullOrWhiteSpace(obj.DniRlNavigation.Nacionalidad))
                                        {
                                            obj_db.DniRlNavigation.Nacionalidad = obj.DniRlNavigation.Nacionalidad;
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
