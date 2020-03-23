using Presentacion.Controllers;
using Presentacion.Helpers;
using Presentacion.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Views
{
    public partial class VerInfo_ProveedorView : Form
    {
        private VerInfo_ProveedorController VI_ProveedorController;
        public VerInfo_ProveedorView(ProveedorModel modelProveedor)
        {
            InitializeComponent();
            this.Size = Resources.size_verinfoView;
            this.LayoutMdi(MdiLayout.ArrangeIcons);
            VI_ProveedorController = new VerInfo_ProveedorController(this, modelProveedor);
        }
    }
}
