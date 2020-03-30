using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTOs
{
    public class Unidad_VehicularDTO
    {
        public string Placa { get; set; }
        public string Marca { get; set; }
        public Nullable<System.DateTime> Y_Fabricacion { get; set; }
        public string Serie_Chasis { get; set; }

        public virtual HashSet<HistorialDTO> Historial { get; set; }
    }
}
