using Presentacion.Models;
using Presentacion.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Controllers
{
    public class VerInfo_ClienteController
    {
        public VerInfo_ClienteView VI_ClienteView;
        private ClienteModel modelCliente;

        public VerInfo_ClienteController(VerInfo_ClienteView vi, ClienteModel modelCliente)
        {
            VI_ClienteView = vi;
            this.modelCliente = modelCliente;
            VI_ClienteView.dataGridView1.DataSource = modelCliente.Clientes;
            VI_ClienteView.Button_ShowM.Click += new EventHandler(ShowMessage);
        }

        private void ShowMessage(object sender, EventArgs e)
        {
            MessageBox.Show("Hola mundo!");
        }
    }
}
