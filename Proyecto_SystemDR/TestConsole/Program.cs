using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio.Business_Objects;

namespace TestConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            foreach (var item in ClienteBO.GetAll())
            {
                Console.WriteLine(item.Persona.Nombre);
            }

            Console.ReadKey();
        }
    }
}
