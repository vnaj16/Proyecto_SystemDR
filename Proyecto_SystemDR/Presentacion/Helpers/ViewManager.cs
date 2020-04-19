using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Presentacion.Helpers
{
    public class ViewManager
    {
        Grid Grid_Selected; //Grid seleccionada actualmente

        public ViewManager(Grid CurrentGrid)
        {
            Grid_Selected = CurrentGrid;
        }

        public void Change_View(Grid newGrid)
        {
            Grid_Selected.Visibility = System.Windows.Visibility.Hidden;
            Grid_Selected = newGrid;
            Grid_Selected.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
