using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Repositories;
using Datos.Interfaces;
using Negocio.Core;
using Entidades;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Unidad_Vehicular obj1 = new Unidad_Vehicular()
            {
                Placa = "XY-39",
                Marca = "Hyundai - X",
                Serie_Chasis = "ALK-5",
                Y_Fabricacion = new DateTime(2005, 12, 13)
            };

            if (TransporteDR.UnidadVehicularBO.Registrar(obj1))
            {
                Console.WriteLine("Exito");
            }
            else
            {
                Console.WriteLine("Error");
            }


            foreach(var y in TransporteDR.UnidadVehicularBO.GetAll())
            {
                Console.WriteLine(y.Placa + " " + y.Y_Fabricacion);
            }

            Console.ReadKey();
        }
    }
}
