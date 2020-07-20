using Datos.Interfaces;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositories
{
    public class UnidadVehicularRepository : IUnidadVehicularRepository
    {
        public bool Delete(string Placa)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    var vehiculo_db = db.Unidad_Vehicular.FirstOrDefault(x => x.Placa == Placa);

                    if (vehiculo_db == null)
                    {
                        return false;
                    }

                    db.Entry(vehiculo_db).State = System.Data.Entity.EntityState.Deleted;

                    db.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public IEnumerable<Unidad_Vehicular> GetAll()
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                return db.Unidad_Vehicular.Include("Historial").ToList();
            }
        }

        public bool Insert(Unidad_Vehicular obj)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    if (!String.IsNullOrWhiteSpace(obj.Placa))
                    {
                        if (!db.Unidad_Vehicular.ToList().Exists(x => x.Placa == obj.Placa))
                        {
                            db.Unidad_Vehicular.Add(obj);

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

        public bool Update(Unidad_Vehicular obj)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    if (!String.IsNullOrWhiteSpace(obj.Placa))
                    {
                        var obj_db = db.Unidad_Vehicular.FirstOrDefault(x => x.Placa == obj.Placa);

                        if (obj_db is null) return false;

                        if (!String.IsNullOrWhiteSpace(obj.Marca)) obj_db.Marca = obj.Marca;

                        if (!String.IsNullOrWhiteSpace(obj.Serie_Chasis)) obj_db.Serie_Chasis = obj.Serie_Chasis;

                        if (!String.IsNullOrWhiteSpace(obj.Y_Fabricacion.ToString())) obj_db.Y_Fabricacion = obj.Y_Fabricacion;


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
