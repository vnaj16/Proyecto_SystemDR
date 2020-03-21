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
    {   //https://www.youtube.com/watch?v=-4EYhC9xDHo
        private PersonaModel modelPersona;
        public StartupView()
        {
            modelPersona = new PersonaModel();
            InitializeComponent();
        }

        private void StartupView_Load(object sender, EventArgs e)
        {
            DGV_Persona.DataSource = modelPersona.Personas;
        }
    }
}
