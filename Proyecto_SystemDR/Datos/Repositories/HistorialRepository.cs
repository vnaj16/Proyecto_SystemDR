﻿using System;
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
            try
            {
                int Id = int.Parse(_Id);
                using (dbTransporteDRContext db = new dbTransporteDRContext())
                {
                    try
                    {
                        var historial_db = db.Historial.FirstOrDefault(x => x.Id == Id);

                        if (historial_db == null)
                        {
                            throw new Exception($"El historial con id {Id} no existe en la base de datos");
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
            catch (Exception)
            {
                throw;
            }

        }

        public bool Exists(string ID)
        {
            try
            {
                int Id = int.Parse(ID);
                using (dbTransporteDRContext db = new dbTransporteDRContext())
                {
                    return db.Historial.ToList().Exists(x => x.Id == Id);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Historial> GetAll()
        {
            try
            {
                using (dbTransporteDRContext db = new dbTransporteDRContext())
                {
                    return db.Historial.Include(x => x.DniConductorNavigation).ThenInclude(x => x.DniNavigation).Include(x => x.IdUnidadNavigation).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Insert(Historial obj)
        {
            try
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
            catch (Exception)
            {
                throw;
            }

        }

        //Quiza sea necesario actualizar las FK, ademas poner sus nuevos FKNavigation
        public bool Update(Historial obj)
        {
            try
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

                        if (!String.IsNullOrWhiteSpace(obj_db.Fecha.ToString()))
                        {
                            obj_db.Fecha = obj.Fecha;
                        }

                        if (!String.IsNullOrWhiteSpace(obj_db.Lugar))
                        {
                            obj_db.Lugar = obj.Lugar;
                        }

                        if (!String.IsNullOrWhiteSpace(obj_db.DniConductor))
                        {
                            obj_db.DniConductor = obj.DniConductor;
                        }

                        if (!String.IsNullOrWhiteSpace(obj_db.IdUnidad))
                        {
                            obj_db.IdUnidad = obj.IdUnidad;
                        }


                        db.SaveChanges();

                        return true;
                    }
                    else
                    {
                        throw new Exception($"El historial con id {obj.Id} no existe en la Base de Datos");
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
