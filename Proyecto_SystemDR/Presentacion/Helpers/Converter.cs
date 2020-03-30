using Negocio.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Helpers
{
    public static class Converter
    {
        public static void Convert(ObservableCollection<ClienteDTO> destination, List<ClienteDTO> source)
        {
            if (destination != null && source != null)
            {
                foreach (ClienteDTO c in source)
                {
                    destination.Add(c);
                }
            }
        }

    }
}
