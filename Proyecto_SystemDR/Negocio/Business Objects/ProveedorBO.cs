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
    public static class ProveedorBO
    {
        public static ProveedorRepository dbProveedor;
        private static List<ProveedorDTO> listaProveedorDTO;

        static ProveedorBO()
        {
            dbProveedor = new ProveedorRepository();
            InitializeList();
        }

        private static void InitializeList()
        {
            listaProveedorDTO = new List<ProveedorDTO>();
            var listaDB = dbProveedor.GetAll();

            foreach (Proveedor x in listaDB)
            {
                ProveedorDTO obj = new ProveedorDTO();
                MyMapper.Map(x, obj);
                listaProveedorDTO.Add(obj);
            }

            //Here link Cliente with Persona
            Linker.LinkProveedorPersona(listaProveedorDTO, PersonaBO.GetAll().Where(x => x.Tipo == "pro").ToList());//ToList para acelerar la ejecucion diferida
        }

        public static IEnumerable<ProveedorDTO> GetAll()
        {
            return listaProveedorDTO;
        }
    }
}
