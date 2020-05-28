using Datos.Interfaces;
//using Datos.Models;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        public bool Delete(Cliente obj)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    var cliente_db = db.Cliente.FirstOrDefault(x => x.RUC == obj.RUC);

                    if(cliente_db == null)
                    {
                        return false;
                    }

                    var persona_db = cliente_db.Persona;

                    if (persona_db != null)
                    {

                        db.Entry(persona_db).State = System.Data.Entity.EntityState.Deleted;
                    }

                    db.Entry(cliente_db).State = System.Data.Entity.EntityState.Deleted;

                 
                    db.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public IEnumerable<Cliente> GetAll()
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                return db.Cliente.Include("Persona.Telefono").ToList();
            }
        }

        public bool Insert(Cliente obj)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    if (!String.IsNullOrWhiteSpace(obj.RUC))
                    {
                        if (!db.Cliente.ToList().Exists(x => x.RUC == obj.RUC))
                        {
                            db.Cliente.Add(obj);

                            if (!String.IsNullOrWhiteSpace(obj.DNI))
                            {
                                if (obj.Persona != null)
                                    db.Persona.Add(obj.Persona);
                                else
                                    return false;
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

        public bool Update(Cliente obj)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    if (!String.IsNullOrWhiteSpace(obj.RUC))
                    {
                        var obj_db = db.Cliente.FirstOrDefault(x => x.RUC == obj.RUC);
                        if (!(obj_db is null))
                        {
                            obj_db.Razon_Social = obj.Razon_Social;
                            obj_db.Direccion = obj.Direccion;
                            obj_db.DNI = obj.DNI;

                            if (!String.IsNullOrWhiteSpace(obj.DNI))
                            {
                                if (obj.Persona != null)
                                {

                                    if(obj_db.Persona is null)
                                    {
                                        db.Persona.Add(obj.Persona);
                                        //obj_db.Persona = obj.Persona;
                                    }
                                    else
                                    {
                                        obj_db.Persona.Nombre = obj.Persona.Nombre;
                                        obj_db.Persona.Apellido = obj.Persona.Apellido;
                                        obj_db.Persona.Fecha_Nac = obj.Persona.Fecha_Nac;
                                        obj_db.Persona.Nacionalidad = obj.Persona.Nacionalidad;
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
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }



                }
                catch (Exception ex ) { return false; }
            }
        }
    }
}
