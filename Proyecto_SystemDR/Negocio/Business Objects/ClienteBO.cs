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
    public static class ClienteBO
    {
        public static ClienteRepository dbCliente;
        private static List<ClienteDTO> listaClienteDTO;

        static ClienteBO()
        {
            dbCliente = new ClienteRepository();
            InitializeList();
        }

        private static void InitializeList()
        {
            listaClienteDTO = new List<ClienteDTO>();
            var listaDB = dbCliente.GetAll();

            foreach (Cliente x in listaDB)
            {
                ClienteDTO obj = new ClienteDTO();
                MyMapper.Map(x, obj);
                listaClienteDTO.Add(obj);
            }

            //Here link Cliente with Persona
            Linker.LinkClientePersona(listaClienteDTO, PersonaBO.GetAll().Where(x => x.Tipo == "cli").ToList());//ToList para acelerar la ejecucion diferida
        }
        public static IEnumerable<ClienteDTO> GetAll()
        {
            return listaClienteDTO;

            /*var listaDTO = new List<ClienteDTO>();
            var listaDB = dbCliente.GetAll();

            foreach (Cliente x in listaDB)
            {
                ClienteDTO obj = new ClienteDTO();
                MyMapper.Map(x, obj);
                listaDTO.Add(obj);
            }

            //Here link Cliente with Persona
            Linker.LinkClientePersona(listaDTO, PersonaBO.GetAll().Where(x => x.Tipo == "cli").ToList());//ToList para acelerar la ejecucion diferida

            return listaDTO.AsEnumerable();*/
        }

    }
}
