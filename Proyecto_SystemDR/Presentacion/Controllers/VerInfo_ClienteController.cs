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
        private PersonaModel modelPersona;

        public VerInfo_ClienteController(VerInfo_ClienteView vi, PersonaModel modelPersona)
        {
            VI_ClienteView = vi;
            this.modelPersona = modelPersona;
            VI_ClienteView.dataGridView1.DataSource = modelPersona.Personas;
            VI_ClienteView.Button_ShowM.Click += new EventHandler(ShowMessage);
        }

        private void ShowMessage(object sender, EventArgs e)
        {
            MessageBox.Show("Hola mundo!");
        }
    }
}
