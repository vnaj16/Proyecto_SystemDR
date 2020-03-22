using Presentacion.Models;
using Presentacion.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Controllers
{
    public class VerInfo_ProveedorController
    {
        public VerInfo_ProveedorView VI_ProveedorView;
        private ProveedorModel modelProveedor;

        public VerInfo_ProveedorController(VerInfo_ProveedorView vi, ProveedorModel modelProveedor)
        {
            VI_ProveedorView = vi;
            this.modelProveedor = modelProveedor;
            VI_ProveedorView.dataGridView1.DataSource = modelProveedor.Proveedores;
        }
    }
}
