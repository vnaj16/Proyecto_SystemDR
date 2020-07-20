using Entidades;
using Negocio.Core;
using Presentacion.Helpers;
using Presentacion.Views.HistorialV;
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
    public class HistorialViewModel : BindableBase
    {
        #region Singleton
        private static HistorialViewModel instance = null;

        private HistorialViewModel()
        {
            listaHistoriales = new ObservableCollection<Historial>(TransporteDR.HistorialBO.GetAll());

            ListaHistorialesAux = listaHistoriales; //Este es otra referencia a la Lista que traigo de la DB, me sirve para cuando tenga que cambiar la lista que se muestra

            AgregarCommand = new DelegateCommand(Execute_AgregarCommand);
            ActualizarCommand = new DelegateCommand(Execute_ActualizarCommand, CanExecute_ActualizarCommand).ObservesProperty(() => CurrentHistorial);
            DeleteCommand = new DelegateCommand(Execute_DeleteCommand, CanExecute_DeleteCommand).ObservesProperty(() => CurrentHistorial);
        }
        public static HistorialViewModel Instance
        {
            get
            {
                if (instance is null)
                {
                    instance = new HistorialViewModel();
                }
                return instance;
            }
        }
        #endregion

        private Historial currentHistorial;

        public Historial CurrentHistorial
        {
            get { return currentHistorial; }
            set { SetProperty(ref currentHistorial, value); }
        }

        private ObservableCollection<Historial> listaHistoriales;

        public ObservableCollection<Historial> ListaHistoriales
        {
            get { return listaHistoriales; }
            set { SetProperty(ref listaHistoriales, value); }
        }

        #region COMMANDS
        public ICommand AgregarCommand { get; set; }

        private void Execute_AgregarCommand()
        {
            RegistrarHistorialView registrarHistorialView = new RegistrarHistorialView(false, null, ConductoresViewModel.Instance.ListaConductores, UnidadVehicularViewModel.Instance.ListaUnidadesVehiculares);
            registrarHistorialView.ShowDialog();

            if (registrarHistorialView.isRegistered)
            {
                var newHistorial = registrarHistorialView.GetHistorial();

                //newHistorial.ID = ListaHistoriales.

                try
                {
                    //Primero se lo paso a la capa negocio para que lo registre, si lo registra, lo pongo en la capa Presentacion
                    if (TransporteDR.HistorialBO.Registrar(newHistorial))
                    {
                        ListaHistoriales.Add(newHistorial);
                        CurrentHistorial = newHistorial;

                        MessageBox.Show($"{newHistorial.ID} Registrado con exito");
                    }
                    else
                    {
                        MessageBox.Show("Algo ha ocurrido con el proceso de registro, por favor intentar de nuevo o reiniciar el computador.\nSi el problema persiste, contactar con el encargado del Sistema");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show(ex.StackTrace);
                }
            }
            /*if (registrarPersonaView.isRegistered)
            {
                listaPersonas.Add(registrarPersonaView.GetPersona());
            }

            //MessageBox.Show("Agregar persona View");*/
        }


        public ICommand ActualizarCommand { get; set; }

        private void Execute_ActualizarCommand()
        {
            RegistrarHistorialView registrarHistorialView = new RegistrarHistorialView(true, CurrentHistorial,ConductoresViewModel.Instance.ListaConductores, UnidadVehicularViewModel.Instance.ListaUnidadesVehiculares);
            registrarHistorialView.ShowDialog();

            if (registrarHistorialView.isUpdated)
            {
                //Primero se lo paso a la capa negocio para que lo registre, si lo registra, lo pongo en la capa Presentacion
                if (TransporteDR.HistorialBO.Actualizar(CurrentHistorial))
                {
                    MessageBox.Show($"{CurrentHistorial.ID} Actualizado con exito");
                }
                else
                {
                    registrarHistorialView.ToDefaultHistorial(CurrentHistorial);

                    MessageBox.Show("Algo ha ocurrido con el proceso de actualizacion, por favor intentar de nuevo o reiniciar el computador.\nSi el problema persiste, contactar con el encargado del Sistema");
                }
            }
        }

        private bool CanExecute_ActualizarCommand()
        {
            return !(CurrentHistorial is null);
        }


        public ICommand DeleteCommand { get; set; }

        private bool CanExecute_DeleteCommand()
        {
            return !(CurrentHistorial is null);
        }

        private void Execute_DeleteCommand()
        {//MORALEJA APRENDIDA: Eliminar primera todas las referencias al objeto actual, para que luego el GB lo recoja
            //CurrentPersona.Ciudad.Habitantes.Remove(CurrentPersona);
            var ID = CurrentHistorial.ID;

            var result = MessageBox.Show("Por favor, confirmar que va a eliminar el historial con ID " + ID, "Eliminar Historial", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            switch (result)
            {
                case MessageBoxResult.OK:

                    switch (MessageBox.Show("Estás seguro?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Exclamation))
                    {
                        case MessageBoxResult.Yes:
                            if (TransporteDR.HistorialBO.Eliminar(CurrentHistorial.ID))
                            {
                                try
                                {
                                    ConductoresViewModel.Instance.ListaConductores.FirstOrDefault(x => x.DNI == CurrentHistorial.DNI).Historial.ToList().RemoveAll(x => x.ID == CurrentHistorial.ID);
                                    UnidadVehicularViewModel.Instance.ListaUnidadesVehiculares.FirstOrDefault(x => x.Placa == CurrentHistorial.ID_Unidad).Historial.ToList().RemoveAll(x => x.ID == CurrentHistorial.ID);
                                }
                                catch(Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                    MessageBox.Show(ex.InnerException.Message);
                                }
                                // CurrentHistorial.Conductor?.Historial.Remove(CurrentHistorial);
                                //CurrentHistorial.Unidad_Vehicular?.Historial.Remove(CurrentHistorial);

                                ListaHistoriales.Remove(CurrentHistorial);

                                CurrentHistorial = null;

                                MessageBox.Show($"Historial {ID} Eliminado con exito");
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
        ObservableCollection<Historial> ListaHistorialesAux;
        public void ChangeCollection(string Filter, FilterTypeSearchHistorial filterType)
        {
            if (String.IsNullOrWhiteSpace(Filter))
            {
                ListaHistoriales = ListaHistorialesAux;
            }
            else
            {
                switch (filterType)
                {
                    case FilterTypeSearchHistorial.DNI:
                        var listAux = new ObservableCollection<Historial>();

                        foreach (var x in ListaHistorialesAux)
                        {
                            if (!(x.Conductor is null))
                            {
                                if (x.Conductor.DNI.StartsWith(Filter))
                                {
                                    listAux.Add(x);
                                }
                            }
                        }

                        ListaHistoriales = listAux;
                        break;

                    case FilterTypeSearchHistorial.Eventualidad:
                        ListaHistoriales = new ObservableCollection<Historial>(ListaHistorialesAux.Where(x => x.Eventualidad.ToLower().StartsWith(Filter.ToLower())));
                        break;

                    case FilterTypeSearchHistorial.Lugar:
                        ListaHistoriales = new ObservableCollection<Historial>(ListaHistorialesAux.Where(x => x.Lugar.ToLower().StartsWith(Filter.ToLower())));
                        break;
                    case FilterTypeSearchHistorial.Placa:
                        var listAux1 = new ObservableCollection<Historial>();

                        foreach (var x in ListaHistorialesAux)
                        {
                            if (!(x.Unidad_Vehicular is null))
                            {
                                if (x.Unidad_Vehicular.Placa.ToLower().StartsWith(Filter.ToLower()))
                                {
                                    listAux1.Add(x);
                                }
                            }
                        }

                        ListaHistoriales = listAux1;
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

    }
}
