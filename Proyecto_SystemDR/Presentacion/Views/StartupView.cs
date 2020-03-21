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
        private ClienteModel modelCliente;
        public StartupView()
        {
            modelCliente = new ClienteModel();
            InitializeComponent();
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

    }
}
