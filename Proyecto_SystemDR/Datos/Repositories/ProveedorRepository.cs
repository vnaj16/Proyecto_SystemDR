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
            try
            {
                using (dbTransporteDRContext db = new dbTransporteDRContext())
                {
                    var proveedor_db = db.Proveedor.FirstOrDefault(x => x.Ruc == Ruc);

                    if (proveedor_db == null)
                    {
                        throw new Exception($"El proveedor con ruc {Ruc} ya no se encuentra en la Base de Datos");
                    }

                    db.Proveedor.Remove(proveedor_db);

                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool Exists(string Ruc)
        {
            try
            {
                using (dbTransporteDRContext db = new dbTransporteDRContext())
                {
                    return db.Proveedor.ToList().Exists(x => x.Ruc == Ruc);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Proveedor> GetAll()
        {
            try
            {
                using (dbTransporteDRContext db = new dbTransporteDRContext())
                {
                    return db.Proveedor.Include(x => x.DniRlNavigation).ThenInclude(x => x.Telefono).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Insert(Proveedor obj)
        {
            try
            {
                using (dbTransporteDRContext db = new dbTransporteDRContext())
                {
                    Persona personaAux = null;
                    if (!(obj.DniRlNavigation is null))
                    {
                        personaAux = obj.DniRlNavigation;
                        obj.DniRlNavigation = null;
                    }

                    db.Proveedor.Add(obj);

                    db.SaveChanges();

                    obj.DniRlNavigation = personaAux;

                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(Proveedor obj)
        {
            try
            {
                using (dbTransporteDRContext db = new dbTransporteDRContext())
                {
                    var obj_db = db.Proveedor.FirstOrDefault(x => x.Ruc == obj.Ruc);
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

                        if (!String.IsNullOrWhiteSpace(obj.Productos))
                        {
                            obj_db.Productos = obj.Productos;
                        }

                        if (!String.IsNullOrWhiteSpace(obj.Tipo))
                        {
                            obj_db.Tipo = obj.Tipo;
                        }

                        if (String.IsNullOrWhiteSpace(obj_db.DniRl))
                        {
                            obj_db.DniRl = obj.DniRl;
                        }


                        db.SaveChanges();

                        return true;
                    }
                    else
                    {
                        throw new Exception($"El proveedor con ruc {obj.Ruc} no existe en la Base de Datos");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
