using Negocio.DTOs;
using Presentacion.Core;
using Presentacion.Core.Commands;
using Presentacion.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentacion.ViewModels
{
    public class ClienteViewModel : IGeneric
    {
        public ClienteViewModel()
        {
            GetAll();
        }

        private ClienteModel Modelo_Cliente = new ClienteModel();

        #region VARIABLES
        private ObservableCollection<ClienteDTO> lista_clientes = new ObservableCollection<ClienteDTO>();

        public ObservableCollection<ClienteDTO> Lista_Clientes
        {
            get
            {
                return lista_clientes;
            }

            set
            {
                lista_clientes = value;

                if (value != null && value.Count > 0)//SI LA LISTA NO ESTA VACIA
                {
                    Current_Cliente = value[0];
                }

                RaisePropertyChanged("Lista_Clientes");
            }
        }


        private ClienteDTO current_cliente; //me dice que persona esta seleccionada actualmente

        public ClienteDTO Current_Cliente
        {
            get { return current_cliente; }
            set
            {
                current_cliente = value;
                RaisePropertyChanged("Current_Cliente");
                RaisePropertyChanged("CanShowInfo");
            }
        }

        private ObservableCollection<ClienteDTO> lista_cliente_filtrada = new ObservableCollection<ClienteDTO>();

        public ObservableCollection<ClienteDTO> Lista_Clientes_Filtrada
        {
            get
            {
                return lista_cliente_filtrada;
            }

            set
            {
                lista_cliente_filtrada = value;

                if (value != null && value.Count > 0)//SI LA LISTA NO ESTA VACIA
                {
                    Current_Cliente = value[0];
                }

                RaisePropertyChanged("Lista_Clientes_Filtrada");
            }
        }

        #endregion 

        #region DEFINICION DE LOS COMANDOS
        #endregion

        #region FUNCIONES Y PROPIEDADES QUE USAN LOS COMANDOS
        public void GetAll()
        {
            lista_clientes = Modelo_Cliente.Clientes;

            /*if (!(current_persona is null))
                MessageBox.Show(Current_Persona.Nombre);
            else
                MessageBox.Show("Es null");*/
        }
        #endregion

        //0: RUC, 1: DNI
        //FALTA IMPLEMENTAR LOS DEMAS FILTROS, ES SOLO UN COPIA Y PEGA, PARA ESTE, PROVEEDOR, CHOFERES, ETC
        public void GetByFilter(string texto, int filter)
        {
            switch (filter)
            {
                case 0:
                    Lista_Clientes_Filtrada = new ObservableCollection<ClienteDTO>(Lista_Clientes.Where(x => x.RUC.StartsWith(texto)));
                    break;
                case 1:
                    Lista_Clientes_Filtrada = new ObservableCollection<ClienteDTO>(Lista_Clientes.Where(x => x.DNI.StartsWith(texto)));
                    break;
            }
        }

        private bool CanShowInfo
        {
            get { return Current_Cliente != null; }
        }
    }
}
