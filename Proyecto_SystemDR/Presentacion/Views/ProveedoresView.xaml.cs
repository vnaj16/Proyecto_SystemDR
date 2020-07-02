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
    /// Interaction logic for ProveedoresView.xaml
    /// </summary>
    public partial class ProveedoresView : UserControl
    {
        private static ProveedoresView instance;

        public ProveedoresView()
        {
            InitializeComponent();
        }


        public static ProveedoresView Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProveedoresView();
                }

                return instance;
            }
        }
    }
}
