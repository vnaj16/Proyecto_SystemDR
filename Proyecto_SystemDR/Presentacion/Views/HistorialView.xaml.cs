using Presentacion.Helpers;
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
    /// Interaction logic for HistorialView.xaml
    /// </summary>
    public partial class HistorialView : UserControl
    {
        private static HistorialView instance;

        private FilterTypeSearchHistorial FilterType = 0;

        public HistorialView()
        {
            InitializeComponent();

            ComboBox_Filtros.ItemsSource = Enum.GetValues(typeof(FilterTypeSearchHistorial));

            try
            {
                this.DataContext = HistorialViewModel.Instance;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static HistorialView Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HistorialView();
                }

                return instance;
            }
        }

        #region CODIGO PARA EL TEXTBOX BUSCAR
        private void TextBox_Buscar_TextChanged(object sender, TextChangedEventArgs e)
        {//POR RENDIMIENTO, LUEGO MODIFICAR PARA QUE SOLO CAMBIE LUEGO DE QUE SE PRESIONE ENTER
            var Text = TextBox_Buscar.Text;
            if (Text != "Buscar")
            {
                HistorialViewModel.Instance.ChangeCollection(Text, FilterType);
            }
        }

        private void TextBox_Buscar_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Buscar.Text == "Buscar")
                TextBox_Buscar.Text = "";
        }

        private void TextBox_Buscar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TextBox_Buscar.Text))
                TextBox_Buscar.Text = "Buscar";
        }

        #endregion

        private void ComboBox_Filtros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Enum.TryParse<FilterTypeSearchHistorial>(ComboBox_Filtros.SelectedValue.ToString(), out FilterType);
        }
    }
}
