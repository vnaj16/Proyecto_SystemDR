//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class GastoV
    {
        public int ID { get; set; }
        public string Tipo { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<double> KM { get; set; }
        public string Documento { get; set; }
        public string Razon_Social { get; set; }
        public Nullable<double> Importe { get; set; }
        public string ID_Viaje { get; set; }
    
        public virtual Viaje Viaje { get; set; }
    }
}