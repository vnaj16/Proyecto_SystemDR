using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Interfaces;
using Entidades;

using Datos.ModelsEFCore;
using Microsoft.EntityFrameworkCore;
using Datos.Helpers;

namespace Datos.Repositories
{
    public class HistorialRepository : IHistorialRepository
    {
        public bool Delete(string _Id)
        {

            int Id = int.Parse(_Id);
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                var historial_db = db.Historial.FirstOrDefault(x => x.Id == Id);

                if (historial_db == null)
                {
                    throw new Exception(ExceptionMessageManager.ExceptionMessageHistorial.DoesNotExist(_Id));
                }

                db.Historial.Remove(historial_db);

                db.SaveChanges();

                return true;
            }
        }

        public bool Exists(string ID)
        {

            int Id = int.Parse(ID);
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                return db.Historial.ToList().Exists(x => x.Id == Id);
            }

        }

        public IEnumerable<Historial> GetAll()
        {

            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                return db.Historial.Include(x => x.DniConductorNavigation).ThenInclude(x => x.DniNavigation).Include(x => x.IdUnidadNavigation).ToList();
            }

        }

        public bool Insert(Historial obj)
        {

            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                /*
               Hago bakcup de Conductor y UV, para que el contexto no las trackee
                 */
                var conductor = obj.DniConductorNavigation;
                obj.DniConductorNavigation = null;

                var vehiculo = obj.IdUnidadNavigation;
                obj.IdUnidadNavigation = null;


                db.Historial.Add(obj);

                db.SaveChanges();

                obj.IdUnidadNavigation = vehiculo;
                obj.DniConductorNavigation = conductor;

                return true;
            }


        }

        //Quiza sea necesario actualizar las FK, ademas poner sus nuevos FKNavigation
        public bool Update(Historial obj)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                var obj_db = db.Historial.FirstOrDefault(x => x.Id == obj.Id);
                if (!(obj_db is null))
                {
                    if (!String.IsNullOrWhiteSpace(obj.Descripcion))
                    {
                        obj_db.Descripcion = obj.Descripcion;
                    }

                    if (!String.IsNullOrWhiteSpace(obj.Eventualidad))
                    {
                        obj_db.Eventualidad = obj.Eventualidad;
                    }

                    if (!String.IsNullOrWhiteSpace(obj.Fecha.ToString()))
                    {
                        obj_db.Fecha = obj.Fecha;
                    }

                    if (!String.IsNullOrWhiteSpace(obj.Lugar))
                    {
                        obj_db.Lugar = obj.Lugar;
                    }

                    if (!String.IsNullOrWhiteSpace(obj.DniConductor))
                    {
                        obj_db.DniConductor = obj.DniConductor;
                    }

                    if (!String.IsNullOrWhiteSpace(obj.IdUnidad))
                    {
                        obj_db.IdUnidad = obj.IdUnidad;
                    }


                    db.SaveChanges();

                    return true;
                }
                else
                {
                    throw new Exception(ExceptionMessageManager.ExceptionMessageHistorial.DoesNotExist(obj.Id.ToString()));
                }
            }
        }
    }
}
