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
    /// Interaction logic for RegistrarClienteView.xaml
    /// </summary>
    public partial class RegistrarClienteView : Window
    {
        private ClienteViewModel c;
        public RegistrarClienteView(ClienteViewModel c)
        {
            InitializeComponent();
            this.c = c;
        }

        private void Button_Registrar_RCV_Click(object sender, RoutedEventArgs e)
        {
            int state_code;
            if(c.Register(TextBox_RUC_RCV.Text, out state_code))
            {
                MessageBox.Show("Registrado correctamente");
            }
            else if (state_code == 111)
            {
                MessageBox.Show("Ya existe un cliente con ese RUC");
            }
            else
            {
                MessageBox.Show("Algo inesperado ocurrio al registrar");
            }
        }
    }
}
