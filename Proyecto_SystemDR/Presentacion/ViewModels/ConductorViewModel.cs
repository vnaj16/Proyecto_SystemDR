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
    public class ConductorViewModel : IGeneric
    {

        public ConductorViewModel()
        {
            GetAll();
        }

        private ConductorModel Modelo_Conductor = new ConductorModel();

        #region VARIABLES
        private ObservableCollection<ConductorDTO> lista_conductores = new ObservableCollection<ConductorDTO>();

        public ObservableCollection<ConductorDTO> Lista_Conductores
        {
            get
            {
                return lista_conductores;
            }

            set
            {
                lista_conductores = value;

                if (value != null && value.Count > 0)//SI LA LISTA NO ESTA VACIA
                {
                    Current_Conductor = value[0];
                }

                RaisePropertyChanged("Lista_Conductores");
            }
        }


        private ConductorDTO current_conductor; //me dice que persona esta seleccionada actualmente

        public ConductorDTO Current_Conductor
        {
            get { return current_conductor; }
            set
            {
                current_conductor = value;
                RaisePropertyChanged("Current_Conductor");
                RaisePropertyChanged("CanShowInfo");
            }
        }

        #endregion


        #region FUNCIONES Y PROPIEDADES QUE USAN LOS COMANDOS
        private void GetAll()
        {
            lista_conductores = Modelo_Conductor.Conductores;
        }
        #endregion


        private bool CanShowInfo
        {
            get { return Current_Conductor != null; }
        }
    }
}
