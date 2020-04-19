using Negocio.Business_Objects;
using Negocio.DTOs;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Models
{
    public class ProveedorModel
    {
        public ObservableCollection<ProveedorDTO> Proveedores { get; set; }

        public ProveedorModel()
        {
            UpdateSource();
        }

        public void UpdateSource()
        {
            Proveedores = new ObservableCollection<ProveedorDTO>(ProveedorBO.GetAll());
        }
    }
}
