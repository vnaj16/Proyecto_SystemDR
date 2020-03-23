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
    public partial class StartupView : Form
    {
        #region Modelos
        private ClienteModel modelCliente;
        private ProveedorModel modelProveedor;
        #endregion

        public StartupView()
        {
            modelCliente = new ClienteModel();
            modelProveedor = new ProveedorModel();
            this.LayoutMdi(MdiLayout.TileHorizontal);
            InitializeComponent();

            this.Size = Resources.size_startupView;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }

        VerInfo_ClienteView VI_ClienteView;
        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VI_ClienteView == null)
            {
                VI_ClienteView = new VerInfo_ClienteView(modelCliente);
                VI_ClienteView.MdiParent = this;
                VI_ClienteView.FormClosed += VI_ClienteView_FormClosed;
                VI_ClienteView.Show();
            }
            else
            {
                VI_ClienteView.Activate();
            }
        }

        private void VI_ClienteView_FormClosed(object sender, FormClosedEventArgs e)
        {
            VI_ClienteView = null;
            //throw new NotImplementedException();
        }


        VerInfo_ProveedorView VI_ProveedorView;
        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VI_ProveedorView == null)
            {
                VI_ProveedorView = new VerInfo_ProveedorView(modelProveedor);
                VI_ProveedorView.MdiParent = this;
                VI_ProveedorView.FormClosed += VI_ProveedorView_FormClosed;
                VI_ProveedorView.Show();
            }
            else
            {
                VI_ProveedorView.Activate();
            }
        }

        private void VI_ProveedorView_FormClosed(object sender, FormClosedEventArgs e)
        {
            VI_ProveedorView = null;
        }
    }
}
