using Negocio.Business_Objects;
using Negocio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Models
{
    public class ClienteModel
    {
        public List<ClienteDTO> Clientes { get; set; }

        public ClienteModel()
        {
            UpdateSource();
        }

        public void UpdateSource()
        {
            Clientes = ClienteBO.GetAll().ToList();
        }
    }
}
