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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel();
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            var LWItem = sender as ListViewItem;

            switch (LWItem.Name)
            {
                case "Clientes":
                    CC_MainWindow.Content = ClientesView.Instance;
                    break;
                case "Viajes":
                    CC_MainWindow.Content = ViajesView.Instance;
                    break;
                case "Proveedores":
                    CC_MainWindow.Content = ProveedoresView.Instance;
                    break;
                case "Conductores":
                    CC_MainWindow.Content = ConductoresView.Instance;
                    break;
                case "UnidadesVehiculares":
                    CC_MainWindow.Content = UnidadesVehicularesView.Instance;
                    break;
                case "Historial":
                    CC_MainWindow.Content = HistorialView.Instance;
                    break;
                default:
                    CC_MainWindow.Content = null;
                    break;

            }
        }

    }
}
