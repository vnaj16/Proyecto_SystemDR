//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Datos.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Historial
    {
        public string DNI { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Eventualidad { get; set; }
        public string Lugar { get; set; }
        public string Descripcion { get; set; }
        public string ID_Unidad { get; set; }
        public int ID { get; set; }
    
        public virtual Conductor Conductor { get; set; }
        public virtual Unidad_Vehicular Unidad_Vehicular { get; set; }
    }
}
