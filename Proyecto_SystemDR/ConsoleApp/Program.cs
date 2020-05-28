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
            IClienteRepository clienteRepository = new ClienteRepository();

            foreach(var x in clienteRepository.GetAll())
            {
                Console.WriteLine(x.RUC);//DECLARAR COMO PARTIAL LAS ENTIDADES, PARA AGREGAR COMPONENTES (EDAD Y OTROS DATOS)
            }



            /*foreach (var x in TransporteDR.ClienteBO.GetAll())
            {
                Console.WriteLine(x.ToString());//DECLARAR COMO PARTIAL LAS ENTIDADES, PARA AGREGAR COMPONENTES (EDAD Y OTROS DATOS)
            }*/

            Cliente obj1 = new Cliente() { RUC = "789456124", Razon_Social = "VNAJ inc 3", Direccion="Los angeles", DNI = "71222465", Persona = new Persona() { DNI = "71222465", Nombre="Pepe jose" } };

            /*if (TransporteDR.ClienteBO.Registrar(obj1))
            {
                Console.WriteLine("Registrado");
            }
            else
            {
                Console.WriteLine("Algo malo ocurrio");
            }*/

            //IClienteRepository clienteRepository = new ClienteRepository();

            /*if (clienteRepository.Update(obj1))
            {
                Console.WriteLine("Actualizado");
            }
            else
            {
                Console.WriteLine("Algo malo ocurrio");
            }*/

            /*if (clienteRepository.Delete(obj1))
            {
                Console.WriteLine("Eliminado");
            }
            else
            {
                Console.WriteLine("Algo malo ocurrio");
            }*/


            Console.ReadKey();
        }
    }
}
