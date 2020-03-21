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


        static ClienteBO()
        {
            dbCliente = new ClienteRepository();
        }

        public static IEnumerable<ClienteDTO> GetAll()
        {
            var listaDTO = new List<ClienteDTO>();
            var listaDB = dbCliente.GetAll();

            foreach (Cliente x in listaDB)
            {
                ClienteDTO obj = new ClienteDTO();
                MyMapper.Map(x, obj);
                listaDTO.Add(obj);
            }

            //Here link Cliente with Persona
            Linker.LinkClientePersona(listaDTO, PersonaBO.GetAll().Where(x => x.Tipo == "cli").ToList());//ToList para acelerar la ejecucion diferida

            return listaDTO.AsEnumerable();
        }

    }
}
