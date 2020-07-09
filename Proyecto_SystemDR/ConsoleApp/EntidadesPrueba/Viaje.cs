using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.EntidadesPrueba
{
    public class Viaje
    {
        public string ID { get; set; }

        public DateTime Fecha_Inicio { get; set; }

        public DateTime Fecha_Salida { get; set; }

        public string Lugar_Origen { get; set; }

        public float KM_Origen { get; set; }

        public DateTime Fecha_Llegada { get; set; }

        public string Lugar_Destino { get; set; }

        public float KM_Destino { get; set; }

        public string Nota { get; set; }

        public List<Gasto> Gastos { get; set; }

        public Mercaderia Mercaderia { get; set; }

        public Documentos Documento { get; set; }

        public Unidad_Vehicular UnidadVehicular { get; set; }
        public Conductor Conductor { get; set; }

        public override string ToString()
        {
            return $"Viaje {ID}\n" +
                $"Mercaderia {Mercaderia.Producto}\n";
        }

    }
}
