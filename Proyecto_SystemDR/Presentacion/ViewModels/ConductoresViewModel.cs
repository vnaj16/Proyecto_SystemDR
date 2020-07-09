using Entidades;
using Negocio.Core;
using Presentacion.Views.ConductoresV;
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
    public class ConductoresViewModel : BindableBase
    {
        #region Singleton
        private static ConductoresViewModel instance = null;

        private ConductoresViewModel()
        {
            listaConductores = new ObservableCollection<Conductor>(TransporteDR.ConductorBO.GetAll());

            ListaConductoresAux = ListaConductores; //Este es otra referencia a la Lista que traigo de la DB, me sirve para cuando tenga que cambiar la lista que se muestra


            AgregarCommand = new DelegateCommand(Execute_AgregarCommand);
            ActualizarCommand = new DelegateCommand(Execute_ActualizarCommand, CanExecute_ActualizarCommand).ObservesProperty(() => CurrentConductor);
            DeleteCommand = new DelegateCommand(Execute_DeleteCommand, CanExecute_DeleteCommand).ObservesProperty(() => CurrentConductor);
        }
        public static ConductoresViewModel Instance
        {
            get
            {
                if (instance is null)
                {
                    instance = new ConductoresViewModel();
                }
                return instance;
            }
        }
        #endregion

        private Conductor currentConductor;

        public Conductor CurrentConductor
        {
            get { return currentConductor; }
            set { SetProperty(ref currentConductor, value); }
        }

        private ObservableCollection<Conductor> listaConductores;

        public ObservableCollection<Conductor> ListaConductores
        {
            get { return listaConductores; }
            set { SetProperty(ref listaConductores, value); }
        }

        #region COMMANDS
        public ICommand AgregarCommand { get; set; }

        private void Execute_AgregarCommand()
        {
            RegistrarConductorView registrarConductorView = new RegistrarConductorView();
            registrarConductorView.ShowDialog();

            if (registrarConductorView.isRegistered)
            {
                var newConductor = registrarConductorView.GetConductor();

                //Primero se lo paso a la capa negocio para que lo registre, si lo registra, lo pongo en la capa Presentacion
                if (TransporteDR.ConductorBO.Registrar(newConductor))
                {
                    ListaConductores.Add(newConductor);
                    CurrentConductor = newConductor;

                    MessageBox.Show($"{newConductor.DNI} Registrado con exito");
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
            RegistrarConductorView registrarConductorView = new RegistrarConductorView(true, CurrentConductor);
            registrarConductorView.ShowDialog();

            if (registrarConductorView.isUpdated)
            {
                //Primero se lo paso a la capa negocio para que lo registre, si lo registra, lo pongo en la capa Presentacion
                if (TransporteDR.ConductorBO.Actualizar(CurrentConductor))
                {
                    MessageBox.Show($"{CurrentConductor.DNI} Actualizado con exito");
                }
                else
                {
                    registrarConductorView.ToDefaultConductor(CurrentConductor);

                    MessageBox.Show("Algo ha ocurrido con el proceso de actualizacion, por favor intentar de nuevo o reiniciar el computador.\nSi el problema persiste, contactar con el encargado del Sistema");
                }
            }
        }

        private bool CanExecute_ActualizarCommand()
        {
            return !(CurrentConductor is null);
        }


        public ICommand DeleteCommand { get; set; }

        private bool CanExecute_DeleteCommand()
        {
            return !(CurrentConductor is null);
        }

        private void Execute_DeleteCommand()
        {//MORALEJA APRENDIDA: Eliminar primera todas las referencias al objeto actual, para que luego el GB lo recoja
            //CurrentPersona.Ciudad.Habitantes.Remove(CurrentPersona);
            var dni = CurrentConductor.DNI;

            var result = MessageBox.Show("Por favor, confirmar que va a eliminar el Conductor con DNI " + dni, "Eliminar Conductor", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            switch (result)
            {
                case MessageBoxResult.OK:

                    switch (MessageBox.Show("Estás seguro?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Exclamation))
                    {
                        case MessageBoxResult.Yes:
                            if (TransporteDR.ConductorBO.Eliminar(CurrentConductor.DNI))
                            {
                                ListaConductores.Remove(CurrentConductor);

                                CurrentConductor = null;

                                MessageBox.Show($"{dni} Eliminado con exito");
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
        ObservableCollection<Conductor> ListaConductoresAux;
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
