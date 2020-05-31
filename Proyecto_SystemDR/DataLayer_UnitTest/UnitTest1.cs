using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Datos;
using Entidades;
using Datos.Repositories;

namespace DataLayer_UnitTest
{
    [TestClass]
    public class Cliente_UnitTest
    {
        [TestMethod]
        public void Create_TestMethod()
        {
            ClienteRepository clienteRepository = new ClienteRepository();

            //OBJETOS A PROBAR (3/3)
            //Cliente obj1 = new Cliente() { RUC = "1234", Razon_Social = "Mi Rz Sc 1", DNI = "71" };
            //Cliente obj1 = new Cliente() { RUC = "1235", Razon_Social = "Mi Rz Sc 1", DNI = "72", Persona = new Persona() { DNI = "72"} };
            Cliente obj1 = new Cliente() { RUC = "1236", Razon_Social = "Mi SC", Persona = new Persona() { DNI = "73" } };

            Assert.IsTrue(clienteRepository.Insert(obj1));
        }

        [TestMethod]
        public void Update_TestMethod()
        {
            ClienteRepository clienteRepository = new ClienteRepository();

            //OBJETOS A PROBAR (3/3)
            //Cliente obj1 = new Cliente() { RUC = "1234", Razon_Social = "Mi Rz Sc 1", DNI = "71" };
            Cliente obj1 = new Cliente() { RUC = "1235", Razon_Social = "Mi Rz Sc 1", DNI = "72", Persona = new Persona() { DNI = "72"} };


            Assert.IsTrue(clienteRepository.Update(obj1));
        }
    }
}
