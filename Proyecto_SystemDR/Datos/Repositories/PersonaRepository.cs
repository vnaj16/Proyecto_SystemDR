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
    public class PersonaRepository : IPersonaRepository
    {
        public bool Delete(string DNI)
        {

            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                var persona_db = db.Persona.FirstOrDefault(x => x.Dni == DNI);

                if (persona_db == null)
                {
                    throw new Exception($"El representante legal con dni {DNI} no existe en la base de datos");
                }

                db.Persona.Remove(persona_db);

                db.SaveChanges();

                return true;

            }

        }

        public bool Exists(string DNI)
        {

            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {

                return db.Persona.ToList().Exists(x => x.Dni == DNI);

            }


        }

        public IEnumerable<Persona> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Insert(Persona obj)
        {
            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                List<Telefono> telefonoAux = null;
                if (!(obj.Telefono is null))
                {
                    telefonoAux = obj.Telefono.ToList();
                    obj.Telefono = null;
                }
                db.Persona.Add(obj);

                db.SaveChanges();

                obj.Telefono = telefonoAux;

                return true;
            }
        }

        public bool Update(Persona obj)
        {

            using (dbTransporteDRContext db = new dbTransporteDRContext())
            {
                var persona_db = db.Persona.FirstOrDefault(x => x.Dni == obj.Dni);
                if (!(persona_db is null))
                {
                    if (!String.IsNullOrWhiteSpace(obj.Nombre))
                    {
                        persona_db.Nombre = obj.Nombre;
                    }

                    if (!String.IsNullOrWhiteSpace(obj.Apellido))
                    {
                        persona_db.Apellido = obj.Apellido;
                    }

                    if (!String.IsNullOrWhiteSpace(obj.FechaNac.ToString()))
                    {
                        persona_db.FechaNac = obj.FechaNac;
                    }

                    if (!String.IsNullOrWhiteSpace(obj.Nacionalidad))
                    {
                        persona_db.Nacionalidad = obj.Nacionalidad;
                    }

                    db.SaveChanges();

                    return true;
                }
                else
                {
                    throw new Exception($"El representante legal con dni {obj.Dni} no existe en la Base de Datos");
                }
            }

        }
    }
}
