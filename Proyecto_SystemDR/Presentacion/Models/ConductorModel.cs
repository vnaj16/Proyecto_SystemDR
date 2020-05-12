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
    public class ConductorModel
    {
        public ObservableCollection<ConductorDTO> Conductores { get; set; }

        public ConductorModel()
        {
            UpdateSource();
        }

        private void UpdateSource()
        {
            Conductores = new ObservableCollection<ConductorDTO>(ConductorBO.GetAll());
        }
    }
}
