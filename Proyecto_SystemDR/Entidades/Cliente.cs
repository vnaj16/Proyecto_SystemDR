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
    
    public partial class Cliente
    {
        public string RUC { get; set; }
        public string Razon_Social { get; set; }
        public string Direccion { get; set; }
        public string Tipo { get; set; }
        public string DNI { get; set; }
    
        public virtual Persona Persona { get; set; }
    }
}
