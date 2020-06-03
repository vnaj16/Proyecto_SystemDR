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
            ConductorRepository x = new ConductorRepository();

            Conductor obj1 = new Conductor() { DNI = "00758693", Brevete = "AC-865", Direccion = "Los Horizontes", Lugar_Nac = "Jaen", Persona = new Persona() { DNI = "00758693", Nombre = "Pepe", Apellido = "Santana" },Fecha_Inicio = new DateTime(2010, 12, 12), Personalidad = "Calmado" };

            var obj2 = x.GetAll().FirstOrDefault(y => y.DNI == "00758693");

            if (x.Update(obj1))
            {
                Console.WriteLine("Updated");
            }
            else
            {
                Console.WriteLine("Error");
            }



            foreach(var y in x.GetAll())
            {
                Console.WriteLine(y.DNI + " " + y.Persona.Nombre);
            }

            Console.ReadKey();
        }
    }
}
