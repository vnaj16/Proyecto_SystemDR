using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public partial class Cliente
    {
        public string MyPropExtra { get; set; } = "Esta es una prop extra";

        public override string ToString()
        {
            return $"{RUC} {Razon_Social} / {DNI}";
        }
    }
}
