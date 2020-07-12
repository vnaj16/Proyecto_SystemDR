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

        public HistorialView()
        {
            InitializeComponent();

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
    }
}
