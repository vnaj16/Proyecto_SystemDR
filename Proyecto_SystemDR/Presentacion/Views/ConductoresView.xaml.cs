using Presentacion.Helpers;
using Presentacion.ViewModels;
using Presentacion.Views.ConductoresV;
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
    /// Interaction logic for ConductoresView.xaml
    /// </summary>
    public partial class ConductoresView : UserControl
    {
        private static ConductoresView instance;

        private FilterTypeSearchConductor FilterType = 0;

        private static bool DataLoaded = false;

        public ConductoresView()
        {
            InitializeComponent();

            ComboBox_Filtros.ItemsSource = Enum.GetValues(typeof(FilterTypeSearchConductor));

            try
            {
                this.DataContext = ConductoresViewModel.Instance;
                DataLoaded = true;
            }
            catch (Exception ex)
            {
                DataLoaded = false;
                MessageBox.Show(ex.Message);
                if (!(ex.InnerException is null))
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }
        }


        public static ConductoresView Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConductoresView();
                }


                if (DataLoaded == false)
                {
                    LoadData();
                }

                DataLoaded = false;


                return instance;
            }
        }

        private static void LoadData()
        {
            try
            {
                ConductoresViewModel.Instance.LoadData();//Para traer la data de la DB cada vez que inicie esta View
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (!(ex.InnerException is null))
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }

        }

        #region CODIGO PARA EL TEXTBOX BUSCAR
        private void TextBox_Buscar_TextChanged(object sender, TextChangedEventArgs e)
        {//POR RENDIMIENTO, LUEGO MODIFICAR PARA QUE SOLO CAMBIE LUEGO DE QUE SE PRESIONE ENTER
            var Text = TextBox_Buscar.Text;
            if (Text != "Buscar")
            {
                ConductoresViewModel.Instance.ChangeCollection(Text, FilterType);
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
            Enum.TryParse<FilterTypeSearchConductor>(ComboBox_Filtros.SelectedValue.ToString(), out FilterType);
        }

        private void Label_Eventualidades_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!(ConductoresViewModel.Instance.CurrentConductor is null))
            {
                //Crear una nueva ventana, que solo muestre esos Historiales
                CustomHistorialesView customHistorialesView = new CustomHistorialesView(ConductoresViewModel.Instance.CurrentConductor.Dni, FilterTypeSearchHistorial.DNI);
                customHistorialesView.ShowDialog();
            }
        }

        private void Button_Refresh_Click(object sender, RoutedEventArgs e)
        {
            ConductoresViewModel.Instance.LoadData();
        }
    }
}
