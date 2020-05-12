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
        #region DB to App
        public static void Map(Persona source, PersonaDTO destination)
        {
            destination.DNI = source.DNI;
            destination.Nombre = source.Nombre;
            destination.Apellido = source.Apellido;
            destination.Fecha_Nac = source.Fecha_Nac;
            destination.Nacionalidad = source.Nacionalidad;
            destination.Tipo = source.Tipo;

            if (source.Telefono != null)
            {
                destination.Telefono = new List<TelefonoDTO>();
                foreach (var t in source.Telefono)
                {
                    TelefonoDTO telef_new = new TelefonoDTO();
                    Map(t, telef_new);
                    destination.Telefono.Add(telef_new);
                }
            }
        }

        public static void Map(Telefono source, TelefonoDTO destination)
        {
            destination.Numero = source.Numero;
            destination.DNI = source.DNI;
        }

        public static void Map(Cliente source, ClienteDTO destination)
        {
            destination.RUC = source.RUC;
            destination.Razon_Social = source.Razon_Social;
            destination.DNI = source.DNI;
            destination.Direccion = source.Direccion;
            destination.Tipo = source.Tipo;

            if (source.Persona != null)
            {
                destination.Persona = new PersonaDTO();
                Map(source.Persona, destination.Persona);
            }
        }

        public static void Map(Proveedor source, ProveedorDTO destination)
        {
            destination.RUC = source.RUC;
            destination.Razon_Social = source.Razon_Social;
            destination.Productos = source.Productos;
            destination.DNI = source.DNI;
            destination.Direccion = source.Direccion;
            destination.Tipo = source.Tipo;

            if (source.Persona != null)
             {
                 destination.Persona = new PersonaDTO();
                 Map(source.Persona, destination.Persona);
             }
        }

        public static void Map(Conductor source, ConductorDTO destination)
        {
            destination.DNI = source.DNI;
            destination.Fecha_Inicio = source.Fecha_Inicio;
            destination.Brevete = source.Brevete;
            destination.Lugar_Nac = source.Lugar_Nac;
            destination.Grado_Instruccion = source.Grado_Instruccion;
            destination.Direccion = source.Direccion;
            destination.Personalidad = source.Personalidad;

            if (source.Persona != null)
            {
                destination.Persona = new PersonaDTO();
                Map(source.Persona, destination.Persona);
            }
        }

        public static void Map(Unidad_Vehicular source, Unidad_VehicularDTO destination)
        {
            destination.Placa = source.Placa;
            destination.Marca = source.Marca;
            destination.Y_Fabricacion = source.Y_Fabricacion;
            destination.Serie_Chasis = source.Serie_Chasis;
        }

        #endregion


        #region App to DB
        public static void Map(PersonaDTO source, Persona destination)
        {
            destination.DNI = source.DNI;
            destination.Nombre = source.Nombre;
            destination.Apellido = source.Apellido;
            destination.Fecha_Nac = source.Fecha_Nac;
            destination.Nacionalidad = source.Nacionalidad;
            destination.Tipo = source.Tipo;

            if (source.Telefono != null)
            {
                destination.Telefono = new List<Telefono>();
                foreach(var t in source.Telefono)
                {
                    Telefono telef_new = new Telefono();
                    Map(t, telef_new);
                    destination.Telefono.Add(telef_new);
                }
            }
        }

        public static void Map(TelefonoDTO source, Telefono destination)
        {
            destination.Numero = source.Numero;
            destination.DNI = source.DNI;
        }

        public static void Map(ClienteDTO source, Cliente destination)
        {
            destination.RUC = source.RUC;
            destination.Razon_Social = source.Razon_Social;
            destination.DNI = source.DNI;
            destination.Direccion = source.Direccion;
            destination.Tipo = source.Tipo;

            if (source.Persona != null)
            {
                destination.Persona = new Persona();
                Map(source.Persona, destination.Persona);
            }
        }

        public static void Map(ProveedorDTO source, Proveedor destination)
        {
            destination.RUC = source.RUC;
            destination.Razon_Social = source.Razon_Social;
            destination.Productos = source.Productos;
            destination.DNI = source.DNI;
            destination.Direccion = source.Direccion;
            destination.Tipo = source.Tipo;

            if (source.Persona != null)
            {
                destination.Persona = new Persona();
                Map(source.Persona, destination.Persona);
            }
        }

        public static void Map(ConductorDTO source, Conductor destination)
        {
            destination.DNI = source.DNI;
            destination.Fecha_Inicio = source.Fecha_Inicio;
            destination.Brevete = source.Brevete;
            destination.Lugar_Nac = source.Lugar_Nac;
            destination.Grado_Instruccion = source.Grado_Instruccion;
            destination.Direccion = source.Direccion;
            destination.Personalidad = source.Personalidad;

            if (source.Persona != null)
            {
                destination.Persona = new Persona();
                Map(source.Persona, destination.Persona);
            }
        }

        public static void Map(Unidad_VehicularDTO source, Unidad_Vehicular destination)
        {
            destination.Placa = source.Placa;
            destination.Marca = source.Marca;
            destination.Y_Fabricacion = source.Y_Fabricacion;
            destination.Serie_Chasis = source.Serie_Chasis;
        }
        #endregion

    }
}
