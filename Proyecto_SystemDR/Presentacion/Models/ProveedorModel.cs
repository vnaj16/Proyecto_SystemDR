using Negocio.Business_Objects;
using Negocio.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Models
{
    public class ProveedorModel
    {
        public List<ProveedorDTO> Proveedores { get; set; }

        public ProveedorModel()
        {
            UpdateSource();
        }

        public void UpdateSource()
        {
            Proveedores = ProveedorBO.GetAll().ToList();
        }
    }
}
