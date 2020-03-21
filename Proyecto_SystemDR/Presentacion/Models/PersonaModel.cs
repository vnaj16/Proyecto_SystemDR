using Negocio.Business_Objects;
using Negocio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Models
{
    public class PersonaModel
    {
        public List<PersonaDTO> Personas { get; set; }

        public PersonaModel()
        {
            UpdateSource();
        }

        /*public bool Registrar(PersonaDTO obj)
        {
            if (!PersonaBO.Exist(obj.DNI))//Crear en capa negocio el exist
            {
                Personas.Add(obj);
                return PersonaBO.Add(obj);
            }
            else
            {
                return false;
            }
        }*/

        public void UpdateSource()
        {
            Personas = PersonaBO.GetAll().ToList();
        }
    }
}
