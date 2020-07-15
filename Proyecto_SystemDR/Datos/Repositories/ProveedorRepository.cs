using Datos.Interfaces;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Datos.Repositories
{
    public class ProveedorRepository : IProveedorRepository
    {
        public bool Delete(string RUC)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    var proveedor_db = db.Proveedor.FirstOrDefault(x => x.RUC == RUC);

                    if (proveedor_db == null)
                    {
                        throw new Exception("Este proveedor ya no se encuentra en la Base de Datos");
                    }

                    var persona_db = proveedor_db.Persona;

                    if (persona_db != null)
                    {
                        var telefonos_db = persona_db.Telefono;

                        if (telefonos_db != null)
                        {
                            db.Telefono.RemoveRange(telefonos_db);
                        }

                        db.Entry(persona_db).State = System.Data.Entity.EntityState.Deleted;
                    }

                    db.Entry(proveedor_db).State = System.Data.Entity.EntityState.Deleted;


                    db.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public IEnumerable<Proveedor> GetAll()
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                return db.Proveedor.Include(x=>x.Persona.Telefono).ToList();
            }
        }

        public bool Insert(Proveedor obj)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    if (!String.IsNullOrWhiteSpace(obj.RUC))
                    {
                        if (!db.Proveedor.ToList().Exists(x => x.RUC == obj.RUC))
                        {
                            db.Proveedor.Add(obj);

                            if (!String.IsNullOrWhiteSpace(obj.DNI))
                            {
                                if (obj.Persona != null)
                                {
                                    db.Persona.Add(obj.Persona);
                                }
                                else
                                {
                                    obj.Persona = new Persona() { DNI = obj.DNI, Tipo = "pro" };
                                    db.Persona.Add(obj.Persona);
                                }

                            }

                            db.SaveChanges();

                            return true;
                        }
                        else
                        {
                            throw new Exception("Ya existe ese proveedor");
                        }
                    }
                    else
                    {
                        throw new Exception("No hay RUC");
                    }



                }
                catch (Exception ex) { throw; }
            }
        }

        public bool Update(Proveedor obj)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    if (!String.IsNullOrWhiteSpace(obj.RUC))
                    {
                        var obj_db = db.Proveedor.Include(x=>x.Persona.Telefono).FirstOrDefault(x => x.RUC == obj.RUC);
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

                                    if (obj_db.Persona is null)
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
                            throw new Exception("Ya existe ese proveedor");
                        }
                    }
                    else
                    {
                        throw new Exception("No hay RUC");
                    }



                }
                catch (Exception ex) { throw; }
            }
        }
    }
}
