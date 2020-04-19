using Negocio.DTOs;
using Presentacion.Core;
using Presentacion.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.ViewModels
{
    public class ProveedorViewModel : IGeneric
    {
        public ProveedorViewModel()
        {
            GetAll();
        }

        private ProveedorModel Modelo_Proveedor = new ProveedorModel();

        #region VARIABLES
        private ObservableCollection<ProveedorDTO> lista_proveedores = new ObservableCollection<ProveedorDTO>();

        public ObservableCollection<ProveedorDTO> Lista_Proveedores
        {
            get
            {
                return lista_proveedores;
            }

            set
            {
                lista_proveedores = value;

                if (value != null && value.Count > 0)//SI LA LISTA NO ESTA VACIA
                {
                    Current_Proveedor = value[0];
                }

                RaisePropertyChanged("Lista_Proveedores");
            }
        }


        private ProveedorDTO current_proveedor; //me dice que persona esta seleccionada actualmente

        public ProveedorDTO Current_Proveedor
        {
            get { return current_proveedor; }
            set
            {
                current_proveedor = value;
                RaisePropertyChanged("Current_Proveedor");
                RaisePropertyChanged("CanShowInfo");
            }
        }

        private ObservableCollection<ProveedorDTO> lista_proveedores_filtrada = new ObservableCollection<ProveedorDTO>();

        public ObservableCollection<ProveedorDTO> Lista_Proveedores_Filtrada
        {
            get
            {
                return lista_proveedores_filtrada;
            }

            set
            {
                lista_proveedores_filtrada = value;

                if (value != null && value.Count > 0)//SI LA LISTA NO ESTA VACIA
                {
                    Current_Proveedor = value[0];
                }

                RaisePropertyChanged("Lista_Proveedores_Filtrada");
            }
        }
        #endregion

        #region DEFINICION DE LOS COMANDOS
        #endregion

        #region FUNCIONES Y PROPIEDADES QUE USAN LOS COMANDOS
        public void GetAll()
        {
            lista_proveedores = Modelo_Proveedor.Proveedores;

            /*if (!(current_persona is null))
                MessageBox.Show(Current_Persona.Nombre);
            else
                MessageBox.Show("Es null");*/
        }
        #endregion

        //0: RUC, 1: DNI
        public void GetByFilter(string texto, int filter)
        {
            switch (filter)
            {
                case 0:
                    Lista_Proveedores_Filtrada = new ObservableCollection<ProveedorDTO>(Lista_Proveedores.Where(x => x.RUC.StartsWith(texto)));
                    break;
                case 1:
                    Lista_Proveedores_Filtrada = new ObservableCollection<ProveedorDTO>(Lista_Proveedores.Where(x => x.DNI.StartsWith(texto)));
                    break;
            }
        }

        private bool CanShowInfo
        {
            get { return Current_Proveedor != null; }
        }
    }
}
