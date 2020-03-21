using Datos.Models;
using Negocio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Helpers
{
    public static class MyMapper
    {
        public static void Map(PersonaDTO source, Persona destination)
        {
            destination.DNI = source.DNI;
            destination.Nombre = source.Nombre;
            destination.Apellido = source.Apellido;
            destination.Fecha_Nac = source.Fecha_Nac;
            destination.Nacionalidad = source.Nacionalidad;
            destination.Tipo = source.Tipo;
        }

        public static void Map(Persona source, PersonaDTO destination)
        {
            destination.DNI = source.DNI;
            destination.Nombre = source.Nombre;
            destination.Apellido = source.Apellido;
            destination.Fecha_Nac = source.Fecha_Nac;
            destination.Nacionalidad = source.Nacionalidad;
            destination.Tipo = source.Tipo;
        }

        public static void Map(Telefono source, TelefonoDTO destination)
        {
            destination.Numero = source.Numero;
            destination.DNI = source.DNI;
        }

        public static void Map(TelefonoDTO source, Telefono destination)
        {
            destination.Numero = source.Numero;
            destination.DNI = source.DNI;
        }

    }
}
