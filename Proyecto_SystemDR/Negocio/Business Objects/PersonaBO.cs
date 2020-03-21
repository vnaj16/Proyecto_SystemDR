using Datos.Models;
using Datos.Repositories;
using Negocio.DTOs;
using Negocio.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Business_Objects
{
    public static class PersonaBO
    {
        public static PersonaRepository dbPersona;

        static PersonaBO()
        {
            dbPersona = new PersonaRepository();
        }

        public static IEnumerable<PersonaDTO> GetAll()
        {
            var listaDTO = new List<PersonaDTO>();
            var listaDB = dbPersona.GetAll();

            foreach (Persona x in listaDB)
            {
                PersonaDTO obj = new PersonaDTO();
                MyMapper.Map(x, obj);
                listaDTO.Add(obj);
            }

            return listaDTO.AsEnumerable();

        }

        /*public static bool Exist(string DNI)
        {
            using (TestPersonaContext db = new TestPersonaContext())
            {
                try
                {
                    return db.Persona.ToList().Exists(x => x.DNI == DNI);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }*/
    }
}
