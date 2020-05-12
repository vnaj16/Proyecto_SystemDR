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
    public class UnidadVehicularViewModel : IGeneric
    {
        public UnidadVehicularViewModel()
        {
            GetAll();
        }

        private UnidadVehicularModel Modelo_UnidadVehicular = new UnidadVehicularModel();

        #region VARIABLES
        private ObservableCollection<Unidad_VehicularDTO> lista_unidadesV= new ObservableCollection<Unidad_VehicularDTO>();

        public ObservableCollection<Unidad_VehicularDTO> Lista_UnidadesV
        {
            get
            {
                return lista_unidadesV;
            }

            set
            {
                lista_unidadesV = value;

                if (value != null && value.Count > 0)//SI LA LISTA NO ESTA VACIA
                {
                    Current_UnidadVehicular = value[0];
                }

                RaisePropertyChanged("Lista_UnidadesV");
            }
        }


        private Unidad_VehicularDTO current_unidadVehicular; //me dice que persona esta seleccionada actualmente

        public Unidad_VehicularDTO Current_UnidadVehicular
        {
            get { return current_unidadVehicular; }
            set
            {
                current_unidadVehicular = value;
                RaisePropertyChanged("Current_UnidadVehicular");
                RaisePropertyChanged("CanShowInfo");
            }
        }

        #endregion


        #region FUNCIONES Y PROPIEDADES QUE USAN LOS COMANDOS
        private void GetAll()
        {
            lista_unidadesV = Modelo_UnidadVehicular.UnidadesVehiculares;
        }
        #endregion


        private bool CanShowInfo
        {
            get { return Current_UnidadVehicular != null; }
        }
    }
}
