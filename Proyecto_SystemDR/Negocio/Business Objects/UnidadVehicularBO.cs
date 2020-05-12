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
    public class UnidadVehicularBO
    {
        public static UnidadVehicularRepository dbUnidadVehicular;
        private static List<Unidad_VehicularDTO> listaUnidadVehicularDTO;

        static UnidadVehicularBO()
        {
            dbUnidadVehicular = new UnidadVehicularRepository();
            InitializeList();
        }

        private static void InitializeList()
        {
            listaUnidadVehicularDTO = new List<Unidad_VehicularDTO>();
            var listaDB = dbUnidadVehicular.GetAll();

            foreach (Unidad_Vehicular x in listaDB)
            {
                Unidad_VehicularDTO obj = new Unidad_VehicularDTO();
                MyMapper.Map(x, obj);
                listaUnidadVehicularDTO.Add(obj);
            }
        }

        public static IEnumerable<Unidad_VehicularDTO> GetAll()
        {
            return listaUnidadVehicularDTO;
        }
    }
}
