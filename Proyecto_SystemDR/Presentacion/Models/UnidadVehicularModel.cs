using Negocio.Business_Objects;
using Negocio.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Models
{
    public class UnidadVehicularModel
    {
        public ObservableCollection<Unidad_VehicularDTO> UnidadesVehiculares { get; set; }

        public UnidadVehicularModel()
        {
            UpdateSource();
        }

        private void UpdateSource()
        {
            UnidadesVehiculares = new ObservableCollection<Unidad_VehicularDTO>(UnidadVehicularBO.GetAll());
        }
    }
}
