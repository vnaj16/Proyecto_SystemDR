using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Repositories;
using Datos.Interfaces;
using Negocio.Core;
using Entidades;
using ConsoleApp.EntidadesPrueba;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Unidad_Vehicular Vehiculo1 = new Unidad_Vehicular()
            {
                Placa = "XY-39",
                Marca = "Hyundai - X",
                Serie_Chasis = "ALK-5",
                Y_Fabricacion = new DateTime(2005, 12, 13)
            };

            Conductor Conductor1 = new Conductor
            {
                DNI = "7111111",
                Brevete = "7A4D-C"
            };

            Gasto Gasto1 = new Gasto()
            {
                ID = 1,
                Nombre = "Peajes",
                Importe = 500,
                ID_Viaje = "V1"
            };

            Gasto Gasto2 = new Gasto()
            {
                ID = 2,
                Nombre = "Comision",
                Importe = 700,
                ID_Viaje = "V1"
            };

            Mercaderia Mercaderia1 = new Mercaderia()
            {
                ID = 1,
                Producto = "Arroz",
                Peso = 74,
                ID_Viaje="V1"
            };

            Viaje Viaje1 = new Viaje()
            {
                ID = "V1",
                Fecha_Inicio = new DateTime(2004, 8, 12),
                Lugar_Origen = "Tumbes",
                Lugar_Destino = "Lima",
                KM_Origen = 50,
                KM_Destino = 800,
                Nota = "Se debe tanto a Pepe",
                Gastos = new List<Gasto>() { Gasto1,Gasto2},
                Mercaderia = Mercaderia1,
                Conductor = Conductor1,
                UnidadVehicular = Vehiculo1
            };

            Console.WriteLine(Viaje1.ToString());

            Console.ReadKey();
        }
    }
}
