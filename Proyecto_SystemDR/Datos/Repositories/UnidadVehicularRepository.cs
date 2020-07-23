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
    public class UnidadVehicularRepository : IUnidadVehicularRepository
    {
        public bool Delete(string Placa)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    var vehiculo_db = db.UnidadVehicular.FirstOrDefault(x => x.Placa == Placa);

                    if (vehiculo_db == null)
                    {
                        return false;
                    }

                    db.UnidadVehicular.Remove(vehiculo_db);

                    db.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public IEnumerable<UnidadVehicular> GetAll()
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                return db.UnidadVehicular.Include(x=>x.Historial).ToList();
            }
        }

        public bool Insert(UnidadVehicular obj)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    if (!String.IsNullOrWhiteSpace(obj.Placa))
                    {
                        if (!db.UnidadVehicular.ToList().Exists(x => x.Placa == obj.Placa))
                        {
                            db.UnidadVehicular.Add(obj);

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

        public bool Update(UnidadVehicular obj)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    if (!String.IsNullOrWhiteSpace(obj.Placa))
                    {
                        var obj_db = db.UnidadVehicular.FirstOrDefault(x => x.Placa == obj.Placa);

                        if (obj_db is null) return false;

                        if (!String.IsNullOrWhiteSpace(obj.Marca)) obj_db.Marca = obj.Marca;

                        if (!String.IsNullOrWhiteSpace(obj.SerieChasis)) obj_db.SerieChasis = obj.SerieChasis;

                        if (!String.IsNullOrWhiteSpace(obj.YFabricacion.ToString())) obj_db.YFabricacion = obj.YFabricacion;


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
