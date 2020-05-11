using Negocio.Business_Objects;
using Negocio.DTOs;
using Presentacion.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.Models
{
    public class ClienteModel
    {
        public ObservableCollection<ClienteDTO> Clientes { get; set; }

        public ClienteModel()
        {

            //Cliente
            UpdateSource();
        }

        public void UpdateSource()
        {//CREAR UN MAPEADOR DE LIST<T> A OBSERVABLECOLLECTION<T> PARA TRABAJAR EN LA CAPA PRESENTACION CON ESA ESTRUCTURA POR CUESTIONES DE BINDING
         //Guiarse de Test_WPF_MVVM
            Clientes = new ObservableCollection<ClienteDTO>(ClienteBO.GetAll());
        }

        public bool Register(ClienteDTO obj, out int state_code)
        {
            return ClienteBO.Register(obj, out state_code);
        }
    }
}
