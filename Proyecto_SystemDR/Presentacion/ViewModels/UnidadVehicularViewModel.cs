using Entidades;
using Negocio.Core;
using Presentacion.Views.UnidadesVehicularesV;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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

            AgregarCommand = new DelegateCommand(Execute_AgregarCommand);
            ActualizarCommand = new DelegateCommand(Execute_ActualizarCommand, CanExecute_ActualizarCommand).ObservesProperty(() => CurrentUnidadVehicular);
            DeleteCommand = new DelegateCommand(Execute_DeleteCommand, CanExecute_DeleteCommand).ObservesProperty(() => CurrentUnidadVehicular);
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

        #region COMMANDS
        public ICommand AgregarCommand { get; set; }

        private void Execute_AgregarCommand()
        {
            RegistrarUnidadVehicularView registrarUnidadVehicularView = new RegistrarUnidadVehicularView();
            registrarUnidadVehicularView.ShowDialog();

            if (registrarUnidadVehicularView.isRegistered)
            {
                var newUnidadVehicular = registrarUnidadVehicularView.GetUnidadVehicular();

                //Primero se lo paso a la capa negocio para que lo registre, si lo registra, lo pongo en la capa Presentacion
                if (TransporteDR.UnidadVehicularBO.Registrar(newUnidadVehicular))
                {
                    ListaUnidadesVehiculares.Add(newUnidadVehicular);
                    CurrentUnidadVehicular = newUnidadVehicular;

                    MessageBox.Show($"{newUnidadVehicular.Placa} Registrado con exito");
                }
                else
                {
                    MessageBox.Show("Algo ha ocurrido con el proceso de registro, por favor intentar de nuevo o reiniciar el computador.\nSi el problema persiste, contactar con el encargado del Sistema");
                }



            }
            /*if (registrarPersonaView.isRegistered)
            {
                listaPersonas.Add(registrarPersonaView.GetPersona());
            }*/

            //MessageBox.Show("Agregar persona View");
        }


        public ICommand ActualizarCommand { get; set; }

        private void Execute_ActualizarCommand()
        {
            RegistrarUnidadVehicularView registrarUnidadVehicularView = new RegistrarUnidadVehicularView(true, CurrentUnidadVehicular);
            registrarUnidadVehicularView.ShowDialog();

            if (registrarUnidadVehicularView.isUpdated)
            {
                //Primero se lo paso a la capa negocio para que lo registre, si lo registra, lo pongo en la capa Presentacion
                if (TransporteDR.UnidadVehicularBO.Actualizar(CurrentUnidadVehicular))
                {
                    MessageBox.Show($"{CurrentUnidadVehicular.Placa} Actualizado con exito");
                }
                else
                {
                    registrarUnidadVehicularView.ToDefaultUnidadVehicular(CurrentUnidadVehicular);

                    MessageBox.Show("Algo ha ocurrido con el proceso de actualizacion, por favor intentar de nuevo o reiniciar el computador.\nSi el problema persiste, contactar con el encargado del Sistema");
                }
            }
        }

        private bool CanExecute_ActualizarCommand()
        {
            return !(CurrentUnidadVehicular is null);
        }


        public ICommand DeleteCommand { get; set; }

        private bool CanExecute_DeleteCommand()
        {
            return !(CurrentUnidadVehicular is null);
        }

        private void Execute_DeleteCommand()
        {//MORALEJA APRENDIDA: Eliminar primera todas las referencias al objeto actual, para que luego el GB lo recoja
            //CurrentPersona.Ciudad.Habitantes.Remove(CurrentPersona);
            var Placa = CurrentUnidadVehicular.Placa;

            var result = MessageBox.Show("Por favor, confirmar que va a eliminar la Unidad Vehicular con Placa " + Placa, "Eliminar UnidadVehicular", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            switch (result)
            {
                case MessageBoxResult.OK:

                    switch (MessageBox.Show("Estás seguro?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Exclamation))
                    {
                        case MessageBoxResult.Yes:
                            if (TransporteDR.UnidadVehicularBO.Eliminar(CurrentUnidadVehicular.Placa))
                            {
                                ListaUnidadesVehiculares.Remove(CurrentUnidadVehicular);

                                CurrentUnidadVehicular = null;

                                MessageBox.Show($"{Placa} Eliminado con exito");
                            }
                            else
                            {
                                MessageBox.Show("Algo ha ocurrido con el proceso de eliminacion, por favor intentar de nuevo o reiniciar el computador.\nSi el problema persiste, contactar con el encargado del Sistema");
                            }
                            break;
                        case MessageBoxResult.No:
                            break;
                    }

                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        #endregion

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
