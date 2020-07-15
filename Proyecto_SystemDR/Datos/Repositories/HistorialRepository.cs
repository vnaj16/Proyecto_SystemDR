using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Interfaces;
using Entidades;
using System.Data.Entity;

namespace Datos.Repositories
{
    public class HistorialRepository : IHistorialRepository
    {
        public bool Delete(string _ID)
        {
            int ID = int.Parse(_ID);
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    var historial_db = db.Historial.FirstOrDefault(x => x.ID == ID);

                    if (historial_db == null)
                    {
                        return false;
                    }

                    db.Entry(historial_db).State = System.Data.Entity.EntityState.Deleted;

                    db.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public IEnumerable<Historial> GetAll()
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                return db.Historial.Include(x => x.Conductor.Persona).Include(x => x.Unidad_Vehicular).ToList();
            }
        }

        public bool Insert(Historial obj)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    if (obj.ID >= 0)
                    {
                        if (!db.Historial.ToList().Exists(x => x.ID == obj.ID))
                        {
                            /*
                           Hago bakcup de Conductor y UV, para que el contexto no las trackee
                             */

                            var conductor = obj.Conductor; obj.Conductor = null;
                            var vehiculo = obj.Unidad_Vehicular; obj.Unidad_Vehicular = null;

                            db.Historial.Add(obj);

                            /*db.Entry(obj.Conductor).State = System.Data.Entity.EntityState.Unchanged;
                            db.Entry(obj.Conductor.Persona).State = System.Data.Entity.EntityState.Unchanged;
                            db.Entry(obj.Unidad_Vehicular).State = System.Data.Entity.EntityState.Unchanged;*/

                            db.SaveChanges();

                            obj.Unidad_Vehicular = vehiculo;
                            obj.Conductor = conductor;

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
                catch (Exception ex) { throw; }

            }
        }

        public bool Update(Historial obj)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    if (obj.ID != 0)
                    {
                        var obj_db = db.Historial.FirstOrDefault(x => x.ID == obj.ID);
                        System.Threading.Thread.Sleep(500);
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

                            if (String.IsNullOrWhiteSpace(obj_db.Fecha.ToString()))
                            {
                                obj_db.Fecha = obj.Fecha;
                            }

                            if (String.IsNullOrWhiteSpace(obj_db.Lugar))
                            {
                                obj_db.Lugar = obj.Lugar;
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
    }
}
