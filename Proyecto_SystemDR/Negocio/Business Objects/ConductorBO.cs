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
    public class ConductorBO
    {
        public static ConductorRepository dbConductor;
        private static List<ConductorDTO> listaConductorDTO;

        static ConductorBO()
        {
            dbConductor = new ConductorRepository();
            InitializeList();
        }

        private static void InitializeList()
        {
            listaConductorDTO = new List<ConductorDTO>();
            var listaDB = dbConductor.GetAll();

            foreach (Conductor x in listaDB)
            {
                ConductorDTO obj = new ConductorDTO();
                MyMapper.Map(x, obj);
                listaConductorDTO.Add(obj);
            }
        }

        public static IEnumerable<ConductorDTO> GetAll()
        {
            return listaConductorDTO;
        }
    }
}
