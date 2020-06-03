using Datos.Interfaces;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositories
{
    public class TelefonoRepository : ITelefonoRepository
    {
        public bool Delete(string Numero)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    if (!String.IsNullOrWhiteSpace(Numero))
                    {
                        Telefono obj = db.Telefono.FirstOrDefault(x => x.Numero == Numero);
                        if (!(obj is null))
                        {
                            db.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

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
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public IEnumerable<Telefono> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 1. Verifico que venga tanto el numero como el DNI del dueño
        /// 2. Verifico si no existe en la DB
        /// 3. Lo agrego al contexto de la Db y luego lo guardo
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Insert(Telefono obj)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    if(!String.IsNullOrWhiteSpace(obj.Numero) && !String.IsNullOrWhiteSpace(obj.DNI))
                    {
                        if (!db.Telefono.ToList().Exists(x => x.Numero == obj.Numero))
                        {
                            db.Telefono.Add(obj);

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
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 1. Verifico que vengan los numeros y el DNI del owner
        /// 2. Elimino el numero anterior
        /// 3. Agrego el numero nuevo
        /// 4. Guardo
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Update(Telefono obj)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                try
                {
                    if (!String.IsNullOrWhiteSpace(obj.Numero) && !String.IsNullOrWhiteSpace(obj.DNI) && !String.IsNullOrWhiteSpace(obj.NumeroAntiguo))
                    {
                        //Telefono tlfAntiguo = db.Telefono.FirstOrDefault(x => x.Numero == obj.NumeroAntiguo);
                        if (Delete(obj.NumeroAntiguo))
                        {
                            return Insert(obj);
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
                catch (Exception ex)
                {
                    return false;
                }
            }
                        
        }
    }
}
