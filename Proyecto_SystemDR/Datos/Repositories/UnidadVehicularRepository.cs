using Datos.Helpers;
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
                var vehiculo_db = db.UnidadVehicular.FirstOrDefault(x => x.Placa == Placa);

                if (vehiculo_db == null)
                {
                    throw new Exception(ExceptionMessageManager.ExceptionMessageVehiculo.DoesNotExist(Placa));
                }

                db.UnidadVehicular.Remove(vehiculo_db);

                db.SaveChanges();

                return true;
            }


        }

        public bool Exists(string ID)
        {

            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                return db.UnidadVehicular.ToList().Exists(x => x.Placa == ID);
            }

        }

        public IEnumerable<UnidadVehicular> GetAll()
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                return db.UnidadVehicular.Include(x => x.Historial).ToList();
            }
        }

        public bool Insert(UnidadVehicular obj)
        {

            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                var Historiales = obj.Historial.ToList();
                obj.Historial = null;

                db.UnidadVehicular.Add(obj);

                db.SaveChanges();

                obj.Historial = Historiales;

                return true;
            }


        }

        public bool Update(UnidadVehicular obj)
        {

            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {

                var obj_db = db.UnidadVehicular.FirstOrDefault(x => x.Placa == obj.Placa);

                if (obj_db is null)
                {
                    throw new Exception(ExceptionMessageManager.ExceptionMessageVehiculo.DoesNotExist(obj.Placa));
                }

                if (!String.IsNullOrWhiteSpace(obj.Marca)) obj_db.Marca = obj.Marca;

                if (!String.IsNullOrWhiteSpace(obj.SerieChasis)) obj_db.SerieChasis = obj.SerieChasis;

                if (!String.IsNullOrWhiteSpace(obj.YFabricacion.ToString())) obj_db.YFabricacion = obj.YFabricacion;


                db.SaveChanges();

                return true;
            }
        }
    }
}
