using Negocio.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Helpers
{
    public static class Linker
    {
        public static void LinkPersonaTelefono(IEnumerable<PersonaDTO> ListaPersona, IEnumerable<TelefonoDTO> ListaTelefonos)
        {
            foreach (PersonaDTO p in ListaPersona)
            {
                p.Telefono = ListaTelefonos.Where(x => x.DNI == p.DNI).ToList();
            }
        }

        public static void LinkClientePersona(IEnumerable<ClienteDTO> ListaCliente, IEnumerable<PersonaDTO> ListaPersona)
        {
            foreach (ClienteDTO c in ListaCliente)
            {
                c.Persona = ListaPersona.Where(x => x.DNI == c.DNI).FirstOrDefault();
            }
        }

        public static void LinkProveedorPersona(IEnumerable<ProveedorDTO> ListaProveedor, IEnumerable<PersonaDTO> ListaPersona)
        {
            foreach (ProveedorDTO c in ListaProveedor)
            {
                c.Persona = ListaPersona.Where(x => x.DNI == c.DNI).FirstOrDefault();
            }
        }
    }
}
