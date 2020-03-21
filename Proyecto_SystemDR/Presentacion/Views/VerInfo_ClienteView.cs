using Presentacion.Controllers;
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
    public partial class VerInfo_ClienteView : Form
    {
        private VerInfo_ClienteController VI_ClienteController;
        public VerInfo_ClienteView(PersonaModel modelPersona)
        {
            InitializeComponent();
            VI_ClienteController = new VerInfo_ClienteController(this,modelPersona);
        }


    }
}
