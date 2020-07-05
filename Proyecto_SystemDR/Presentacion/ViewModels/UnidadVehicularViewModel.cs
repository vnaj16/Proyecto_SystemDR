using Entidades;
using Negocio.Core;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.ViewModels
{
    public class UnidadVehicularViewModel : BindableBase
    {
        #region Singleton
        private static UnidadVehicularViewModel instance = null;

        private UnidadVehicularViewModel()
        {
            listaUnidadesVehiculares = new ObservableCollection<Unidad_Vehicular>(TransporteDR.UnidadVehicularBO.GetAll());

            ListaUnidadesVehicularesAux = ListaUnidadesVehiculares; //Este es otra referencia a la Lista que traigo de la DB, me sirve para cuando tenga que cambiar la lista que se muestra

            /*AgregarCommand = new DelegateCommand(Execute_AgregarCommand);
            ActualizarCommand = new DelegateCommand(Execute_ActualizarCommand, CanExecute_ActualizarCommand).ObservesProperty(() => CurrentCliente);
            DeleteCommand = new DelegateCommand(Execute_DeleteCommand, CanExecute_DeleteCommand).ObservesProperty(() => CurrentCliente);*/
        }
        public static UnidadVehicularViewModel Instance
        {
            get
            {
                if (instance is null)
                {
                    instance = new UnidadVehicularViewModel();
                }
                return instance;
            }
        }
        #endregion

        private Unidad_Vehicular currentUnidadVehicular;

        public Unidad_Vehicular CurrentUnidadVehicular
        {
            get { return currentUnidadVehicular; }
            set { SetProperty(ref currentUnidadVehicular, value); }
        }

        private ObservableCollection<Unidad_Vehicular> listaUnidadesVehiculares;

        public ObservableCollection<Unidad_Vehicular> ListaUnidadesVehiculares
        {
            get { return listaUnidadesVehiculares; }
            set { SetProperty(ref listaUnidadesVehiculares, value); }
        }

        #region METHODS
        ObservableCollection<Unidad_Vehicular> ListaUnidadesVehicularesAux;
        /*public void ChangeCollection(string Filter, FilterTypeSearchCliente filterType)
        {
            if (String.IsNullOrWhiteSpace(Filter))
            {
                ListaClientes = ListaClientesAux;
            }
            else
            {
                switch (filterType)
                {
                    case FilterTypeSearchCliente.RUC:
                        ListaClientes = new ObservableCollection<Cliente>(ListaClientesAux.Where(x => x.RUC.StartsWith(Filter)));
                        break;

                    case FilterTypeSearchCliente.RazonSocial:
                        ListaClientes = new ObservableCollection<Cliente>(ListaClientesAux.Where(x => x.Razon_Social.StartsWith(Filter)));
                        break;
                    case FilterTypeSearchCliente.DNI:
                        var listAux = new ObservableCollection<Cliente>();

                        foreach (var x in ListaClientesAux)
                        {
                            if (!(x.Persona is null))
                            {
                                if (x.Persona.DNI.StartsWith(Filter))
                                {
                                    listAux.Add(x);
                                }
                            }
                        }

                        ListaClientes = listAux;
                        break;
                    default:
                        break;
                }
            }
        }*/
        #endregion
    }


}
