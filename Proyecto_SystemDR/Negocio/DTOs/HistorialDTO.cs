using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.DTOs
{
    public class HistorialDTO
    {
        public string DNI { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Eventualidad { get; set; }
        public string Lugar { get; set; }
        public string Descripcion { get; set; }
        public string ID_Unidad { get; set; }
        public int ID { get; set; }

        public virtual ConductorDTO Conductor { get; set; }
        public virtual Unidad_VehicularDTO Unidad_Vehicular { get; set; }
    }
}
