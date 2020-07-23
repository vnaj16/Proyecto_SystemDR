using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Interfaces;
using Entidades;

using Datos.ModelsEFCore;
using Microsoft.EntityFrameworkCore;

namespace Datos.Repositories
{
    public class HistorialRepository : IHistorialRepository
    {
        public bool Delete(string _Id)
        {
            int Id = int.Parse(_Id);
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    var historial_db = db.Historial.FirstOrDefault(x => x.Id == Id);

                    if (historial_db == null)
                    {
                        return false;
                    }

                    db.Historial.Remove(historial_db);

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
                return db.Historial.Include(x => x.DniConductorNavigation).ThenInclude(x=>x.DniNavigation).Include(x => x.IdUnidadNavigation).ToList();
            }
        }

        public bool Insert(Historial obj)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    if (obj.Id >= 0)
                    {
                        if (!db.Historial.ToList().Exists(x => x.Id == obj.Id))
                        {
                            /*
                           Hago bakcup de Conductor y UV, para que el contexto no las trackee
                             */

                            var conductor = obj.DniConductorNavigation; obj.DniConductorNavigation = null;
                            var vehiculo = obj.IdUnidadNavigation; obj.IdUnidadNavigation = null;

                            db.Historial.Add(obj);

                            /*db.Entry(obj.Conductor).State = System.Data.Entity.EntityState.Unchanged;
                            db.Entry(obj.Conductor.Persona).State = System.Data.Entity.EntityState.Unchanged;
                            db.Entry(obj.UnIdad_Vehicular).State = System.Data.Entity.EntityState.Unchanged;*/

                            db.SaveChanges();

                            obj.IdUnidadNavigation = vehiculo;
                            obj.DniConductorNavigation = conductor;

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
                    if (obj.Id != 0)
                    {
                        var obj_db = db.Historial.FirstOrDefault(x => x.Id == obj.Id);
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
