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
            Telefono obj1 = new Telefono() { Numero = "784512451236999", DNI = "71" };


            if (TransporteDR.TelefonoBO.Registrar(obj1))
            {
                Console.WriteLine("Registrado");
            }
            else
            {
                Console.WriteLine("Error");
            }


           Console.ReadKey();
        }
    }
}
