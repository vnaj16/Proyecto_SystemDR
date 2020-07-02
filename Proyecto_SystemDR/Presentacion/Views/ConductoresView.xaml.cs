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

        public ConductoresView()
        {
            InitializeComponent();
        }


        public static ConductoresView Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConductoresView();
                }

                return instance;
            }
        }
    }
}
