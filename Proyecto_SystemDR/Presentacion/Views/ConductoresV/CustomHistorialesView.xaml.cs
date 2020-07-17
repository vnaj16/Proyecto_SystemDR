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
using System.Windows.Shapes;

namespace Presentacion.Views.ConductoresV
{
    /// <summary>
    /// Interaction logic for CustomHistorialesView.xaml
    /// </summary>
    public partial class CustomHistorialesView : Window
    {
        public CustomHistorialesView(string Filter, FilterTypeSearchHistorial filterType)
        {
            InitializeComponent();

            CC_CustomHistorialesView.Content = HistorialView.Instance;

            HistorialViewModel.Instance.ChangeCollection(Filter, filterType);
        }
    }
}
