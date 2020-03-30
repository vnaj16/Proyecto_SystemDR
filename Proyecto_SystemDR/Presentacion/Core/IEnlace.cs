using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Core
{
    interface IEnlace : INotifyPropertyChanged //Se implementa esto para que cada entidad le notifique a la vista sobre sus cambios y los muestre automaticamente   
    {
    }
}
