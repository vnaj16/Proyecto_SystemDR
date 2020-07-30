using Entidades;
using Negocio.Core;
using Presentacion.Helpers;
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
            LoadData();

            AgregarCommand = new DelegateCommand(Execute_AgregarCommand);
            ActualizarCommand = new DelegateCommand(Execute_ActualizarCommand, CanExecute_ActualizarCommand).ObservesProperty(() => CurrentUnidadVehicular);
            DeleteCommand = new DelegateCommand(Execute_DeleteCommand, CanExecute_DeleteCommand).ObservesProperty(() => CurrentUnidadVehicular);
        }

        public void LoadData()
        {
            ListaUnidadesVehiculares = new ObservableCollection<UnidadVehicular>(TransporteDR.UnidadVehicularBO.GetAll());

            ListaUnidadesVehicularesAux = ListaUnidadesVehiculares; //Este es otra referencia a la Lista que traigo de la DB, me sirve para cuando tenga que cambiar la lista que se muestra
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

        private UnidadVehicular currentUnidadVehicular;

        public UnidadVehicular CurrentUnidadVehicular
        {
            get { return currentUnidadVehicular; }
            set { SetProperty(ref currentUnidadVehicular, value); }
        }

        private ObservableCollection<UnidadVehicular> listaUnidadesVehiculares;

        public ObservableCollection<UnidadVehicular> ListaUnidadesVehiculares
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

                try
                {
                    if (TransporteDR.UnidadVehicularBO.Registrar(newUnidadVehicular))
                    {
                        LoadData();
                        CurrentUnidadVehicular = ListaUnidadesVehiculares.FirstOrDefault(x => x.Placa == newUnidadVehicular.Placa);

                        MessageBox.Show($"{newUnidadVehicular.Placa} Registrado con exito");
                    }
                    else
                    {
                        MessageBox.Show("Algo ha ocurrido con el proceso de registro, por favor intentar de nuevo o reiniciar el computador.\nSi el problema persiste, contactar con el encargado del Sistema");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    if (!(ex.InnerException is null))
                    {
                        MessageBox.Show(ex.InnerException.Message);
                    }
                }
            }
        }


        public ICommand ActualizarCommand { get; set; }

        private void Execute_ActualizarCommand()
        {
            RegistrarUnidadVehicularView registrarUnidadVehicularView = new RegistrarUnidadVehicularView(true, CurrentUnidadVehicular);
            registrarUnidadVehicularView.ShowDialog();

            if (registrarUnidadVehicularView.isUpdated)
            {
                try
                {
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
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    if (!(ex.InnerException is null))
                    {
                        MessageBox.Show(ex.InnerException.Message);
                    }
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
                            try
                            {
                                if (TransporteDR.UnidadVehicularBO.Eliminar(CurrentUnidadVehicular.Placa))
                                {
                                    LoadData();

                                    CurrentUnidadVehicular = null;

                                    MessageBox.Show($"{Placa} Eliminado con exito");
                                }
                                else
                                {
                                    MessageBox.Show("Algo ha ocurrido con el proceso de eliminacion, por favor intentar de nuevo o reiniciar el computador.\nSi el problema persiste, contactar con el encargado del Sistema");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                if (!(ex.InnerException is null))
                                {
                                    MessageBox.Show(ex.InnerException.Message);
                                }
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
        ObservableCollection<UnidadVehicular> ListaUnidadesVehicularesAux;
        public void ChangeCollection(string Filter, FilterTypeSearchVehiculo filterType)
        {
            if (String.IsNullOrWhiteSpace(Filter))
            {
                ListaUnidadesVehiculares = ListaUnidadesVehicularesAux;
            }
            else
            {
                switch (filterType)
                {
                    case FilterTypeSearchVehiculo.Marca:
                        ListaUnidadesVehiculares = new ObservableCollection<UnidadVehicular>(ListaUnidadesVehicularesAux.Where(x => x.Marca.ToLower().StartsWith(Filter.ToLower())));
                        break;

                    case FilterTypeSearchVehiculo.Placa:
                        ListaUnidadesVehiculares = new ObservableCollection<UnidadVehicular>(ListaUnidadesVehicularesAux.Where(x => x.Placa.ToLower().StartsWith(Filter.ToLower())));
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }


}
