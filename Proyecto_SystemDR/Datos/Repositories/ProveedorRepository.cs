using Datos.Interfaces;
using Datos.ModelsEFCore;
using Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositories
{
    public class ProveedorRepository : IProveedorRepository
    {
        public bool Delete(string Ruc)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    var proveedor_db = db.Proveedor.FirstOrDefault(x => x.Ruc == Ruc);

                    if (proveedor_db == null)
                    {
                        throw new Exception("Este proveedor ya no se encuentra en la Base de Datos");
                    }

                    var DniRlNavigation_db = proveedor_db.DniRlNavigation;

                    if (DniRlNavigation_db != null)
                    {
                        var telefonos_db = DniRlNavigation_db.Telefono;

                        if (telefonos_db != null)
                        {
                            db.Telefono.RemoveRange(telefonos_db);
                        }

                        db.Persona.Remove(DniRlNavigation_db);
                    }

                    db.Proveedor.Remove(proveedor_db);


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
                return db.Proveedor.Include(x=>x.DniRlNavigation).ThenInclude(x=>x.Telefono).ToList();
            }
        }

        public bool Insert(Proveedor obj)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    if (!String.IsNullOrWhiteSpace(obj.Ruc))
                    {
                        if (!db.Proveedor.ToList().Exists(x => x.Ruc == obj.Ruc))
                        {
                            db.Proveedor.Add(obj);

                            if (!String.IsNullOrWhiteSpace(obj.DniRl))
                            {
                                if (obj.DniRlNavigation != null)
                                {
                                    db.Persona.Add(obj.DniRlNavigation);
                                }
                                else
                                {
                                    obj.DniRlNavigation = new Persona() { Dni = obj.DniRl, Tipo = "pro" };
                                    db.Persona.Add(obj.DniRlNavigation);
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
                        throw new Exception("No hay Ruc");
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
                    if (!String.IsNullOrWhiteSpace(obj.Ruc))
                    {
                        var obj_db = db.Proveedor.Include(x=>x.DniRlNavigation.Telefono).FirstOrDefault(x => x.Ruc == obj.Ruc);
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

                                    if (obj_db.DniRlNavigation is null)
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
                            throw new Exception("Ya existe ese proveedor");
                        }
                    }
                    else
                    {
                        throw new Exception("No hay Ruc");
                    }



                }
                catch (Exception ex) { throw; }
            }
        }
    }
}
