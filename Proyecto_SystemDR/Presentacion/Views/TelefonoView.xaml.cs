using Entidades;
using Presentacion.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Presentacion.Views
{
    /// <summary>
    /// Interaction logic for TelefonoView.xaml
    /// </summary>
    public partial class TelefonoView : Window
    {
        public TelefonoView(Persona persona, string RZ = null)
        {
            InitializeComponent();

            this.DataContext = new TelefonoViewModel(persona, RZ);
        }
    }
}
