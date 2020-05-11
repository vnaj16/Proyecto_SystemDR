using Presentacion.Helpers;
using Presentacion.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentacion.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ClienteViewModel c;
        private ProveedorViewModel p;
        private ViewManager viewManager;
        public MainWindow()
        {
            c = new ClienteViewModel();
            p = new ProveedorViewModel();

            //GridActivated = new Grid();
            InitializeComponent();

            viewManager = new ViewManager(ClientesGridView);

            //DG_Cliente.ItemsSource = c.Lista_Clientes;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TestView x = new TestView();
            x.ShowDialog();
        }

        private void VI_Cliente_Click(object sender, RoutedEventArgs e)
        {
            viewManager.Change_View(ClientesGridView);
        }

        private void VI_Proveedor_Click(object sender, RoutedEventArgs e)
        {
            viewManager.Change_View(ProveedoresGridView);
        }

        ///////BUSCADORES
        private void TextBox_Buscador_ProveedorGV_TextChanged(object sender, TextChangedEventArgs e)
        {
            if((sender as TextBox).Text == "")
            {
                DG_Proveedor.ItemsSource = p.Lista_Proveedores;
            }
            else
            {
                p.GetByFilter(TextBox_Buscador_ProveedorGV.Text, ComboBox_SearchFilter_ProveedorGV.SelectedIndex);
                DG_Proveedor.ItemsSource = p.Lista_Proveedores_Filtrada;
            }
        }

        private void TextBox_Buscador_ClienteGV_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                DG_Cliente.ItemsSource = c.Lista_Clientes;
            }
            else
            {
                c.GetByFilter(TextBox_Buscador_ClienteGV.Text, ComboBox_SearchFilter_ClienteGV.SelectedIndex);
                DG_Cliente.ItemsSource = c.Lista_Clientes_Filtrada;
            }
        }

        #region Views Emergentes
        private void Button_AgregarCliente_ClienteGV_Click(object sender, RoutedEventArgs e)
        {
            RegistrarClienteView registrarClienteView = new RegistrarClienteView(c);
            registrarClienteView.ShowDialog();
        }
        #endregion

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.F5)
            {
                MessageBox.Show("Actualizando...");
            }
        }
    }
}
