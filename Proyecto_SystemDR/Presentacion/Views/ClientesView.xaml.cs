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
    /// Interaction logic for ClientesView.xaml
    /// </summary>
    public partial class ClientesView : UserControl
    {
        private static ClientesView instance;

        private ClientesView()
        {
            InitializeComponent();

            try
            {
                this.DataContext = ClientesViewModel.Instance;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public static ClientesView Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ClientesView();
                }

                return instance;
            }
        }

        #region CODIGO PARA EL TEXTBOX BUSCAR
        private void TextBox_Buscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var Text = TextBox_Buscar.Text;
            if (Text != "Buscar")
            {
                if (String.IsNullOrWhiteSpace(Text))
                {
                    MessageBox.Show("Ya no hay nada, deberia mostrarse la lista normal");
                }
                else
                {
                    MessageBox.Show("Hay algo, deberia mostrar en el DataGrid lo relacionado a " + Text);
                }
            }
        }

        private void TextBox_Buscar_GotFocus(object sender, RoutedEventArgs e)
        {
            if(TextBox_Buscar.Text =="Buscar")
                TextBox_Buscar.Text = "";
        }

        private void TextBox_Buscar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TextBox_Buscar.Text))
                TextBox_Buscar.Text = "Buscar";
        }

        #endregion
    }
}
