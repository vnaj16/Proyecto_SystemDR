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
    public static class TelefonoBO
    {
        public static TelefonoRepository dbTelefono;
        private static List<TelefonoDTO> listaTelefonoDTO;


        static TelefonoBO()
        {
            dbTelefono = new TelefonoRepository();
            InitializeList();
        }

        private static void InitializeList()
        {
            listaTelefonoDTO = new List<TelefonoDTO>();
            var listaDB = dbTelefono.GetAll();

            foreach (Telefono x in listaDB)
            {
                TelefonoDTO obj = new TelefonoDTO();
                MyMapper.Map(x, obj);
                listaTelefonoDTO.Add(obj);
            }
        }
        public static IEnumerable<TelefonoDTO> GetAll()
        {
            return listaTelefonoDTO;

            /*var listaDTO = new List<TelefonoDTO>();
            var listaDB = dbTelefono.GetAll();

            foreach (Telefono x in listaDB)
            {
                TelefonoDTO obj = new TelefonoDTO();
                MyMapper.Map(x, obj);
                listaDTO.Add(obj);
            }

            //Here link Telefono with Persona
            Linker.LinkTelefonoPersona(listaDTO, PersonaBO.GetAll().Where(x => x.Tipo == "cli").ToList());//ToList para acelerar la ejecucion diferida

            return listaDTO.AsEnumerable();*/
        }
    }
}
