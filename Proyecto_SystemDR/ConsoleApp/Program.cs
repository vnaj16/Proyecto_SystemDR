using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Repositories;
using Datos.Interfaces;
using Negocio.Core;
using Entidades;


///*namespace ConsoleApp
//{
//    /*class Program
//    {
//        /*
//                     Unidad_Vehicular Vehiculo1 = new Unidad_Vehicular()
//            {
//                Placa = "XY-39",
//                Marca = "Hyundai - X",
//                Serie_Chasis = "ALK-5",
//                Y_Fabricacion = new DateTime(2005, 12, 13)
//            };

//            Conductor Conductor1 = new Conductor
//            {
//                DNI = "7111111",
//                Brevete = "7A4D-C"
//            };

//            Gasto Gasto1 = new Gasto()
//            {
//                ID = 1,
//                Nombre = "Peajes",
//                Importe = 500,
//                ID_Viaje = "V1"
//            };

//            Gasto Gasto2 = new Gasto()
//            {
//                ID = 2,
//                Nombre = "Comision",
//                Importe = 700,
//                ID_Viaje = "V1"
//            };

//            Mercaderia Mercaderia1 = new Mercaderia()
//            {
//                ID = 1,
//                Producto = "Arroz",
//                Peso = 74,
//                ID_Viaje="V1"
//            };

//            Viaje Viaje1 = new Viaje()
//            {
//                ID = "V1",
//                Fecha_Inicio = new DateTime(2004, 8, 12),
//                Lugar_Origen = "Tumbes",
//                Lugar_Destino = "Lima",
//                KM_Origen = 50,
//                KM_Destino = 800,
//                Nota = "Se debe tanto a Pepe",
//                Gastos = new List<Gasto>() { Gasto1,Gasto2},
//                Mercaderia = Mercaderia1,
//                Conductor = Conductor1,
//                UnidadVehicular = Vehiculo1
//            };

//            Console.WriteLine(Viaje1.ToString());
//             //*/
//        static void Main(string[] args)
//        {
//            Viaje viaje = LoadData1();

//            var Mercaderia1 = new Mercaderia()
//            {
//                //ID = 1,
//                Producto = "Arroz Cascara",
//                Bultos = "250 sacos",
//                Peso = 450,
//                Unidad = "KG",
//                ID_Viaje = "ABC-1234"
//            };

//            using (var db = new dbTransporteDRContext())
//            {
//                #region Agregar
//                ///*
//                db.Viaje.Add(viaje); //Primero cargo la entidad padre a la DB

//                //db.Mercaderia.Add(Mercaderia1); //Luego todas las entidades relacionadas

//                db.SaveChanges();

//                //Luego de guardar, jalo las entidades relacionadas, ya que como tiene ID Auto., las traigo ya con ese ID generado
//                //Para luego modificarle al viaje (traido de la DB para el contexto) sus respectvias FK a esas entidades y por ultimo ya guardar

//                var merca = db.Mercaderia.Where(x => x.ID_Viaje == viaje.ID).FirstOrDefault();

//                var viajedb = db.Viaje.Find(viaje.ID);
//                viajedb.ID_Mercaderia = merca.ID;


//                db.SaveChanges();
//                //*/
//                #endregion

//                #region Eliminar
//                /*
//                var viajedb = db.Viaje.Find(viaje.ID);
//                db.Entry((viajedb)).State = System.Data.Entity.EntityState.Deleted;

//                db.SaveChanges();
//                //*/
//                #endregion

//                #region Leer
//                //*
//                //var viajedb = db.Viaje.Include("Mercaderia").ToList().Find(x => x.ID == viaje.ID);
//                var viajedb = db.Viaje.ToList().Find(x => x.ID == viaje.ID);

//                Console.WriteLine(viaje.ID);
//                //*/
//                #endregion


//            }

//            Console.ReadKey();
//        }

//        static List<Viaje> LoadData()
//        {
//            List<Viaje> output = new List<Viaje>();
//            output.Add(LoadData1());
//            output.Add(LoadData2());

//            return output;
//        }

//        static Viaje LoadData1()
//        {
//            Viaje output = new Viaje()
//            {
//                ID = "ABC-1234",
//                Fecha_Inicio = new DateTime(2020, 5, 16),
//                Fecha_Salida = new DateTime(2020, 5, 16),
//                Lugar_Origen = "Sullana",
//                KM_Origen = 500.2,
//                Fecha_Llegada = new DateTime(2020, 5, 19),
//                Lugar_Destino = "Arequipa",
//                KM_Destino = 852.9,
//                Nota = "El conductor tuvo una retraso de 3 horas",
//                Encargado = "Juan Alvarado",
//                //ID_Mercaderia = 1,
//            };

//            return output;
//        }

//        static Viaje LoadData2()
//        {
//            Viaje output = new Viaje();




//            return output;
//        }
//    }*/
//}
