using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio.Core;
using Entidades;
using System.Collections.ObjectModel;
using Prism.Commands;
using System.Windows.Input;
using Presentacion.Views.ClientesV;
using System.Windows;
using Presentacion.Helpers;

namespace Presentacion.ViewModels
{
    public class ClientesViewModel : BindableBase
    {
        #region Singleton
        private static ClientesViewModel instance = null;

        private ClientesViewModel()
        {
            listaClientes = new ObservableCollection<Cliente>(TransporteDR.ClienteBO.GetAll());

            ListaClientesAux = ListaClientes; //Este es otra referencia a la Lista que traigo de la DB, me sirve para cuando tenga que cambiar la lista que se muestra

            AgregarCommand = new DelegateCommand(Execute_AgregarCommand);
            ActualizarCommand = new DelegateCommand(Execute_ActualizarCommand, CanExecute_ActualizarCommand).ObservesProperty(() => CurrentCliente);
            DeleteCommand = new DelegateCommand(Execute_DeleteCommand, CanExecute_DeleteCommand).ObservesProperty(() => CurrentCliente);
        }
        public static ClientesViewModel Instance
        {
            get
            {
                if (instance is null)
                {
                    instance = new ClientesViewModel();
                }
                return instance;
            }
        }
        #endregion

        private Cliente currentCliente;

        public Cliente CurrentCliente
        {
            get { return currentCliente; }
            set { SetProperty(ref currentCliente, value); }
        }

        private ObservableCollection<Cliente> listaClientes;

        public ObservableCollection<Cliente> ListaClientes
        {
            get { return listaClientes; }
            set { SetProperty(ref listaClientes, value); }
        }


        #region COMMANDS
        public ICommand AgregarCommand { get; set; }

        private void Execute_AgregarCommand()
        {
            RegistrarClienteView registrarClienteView = new RegistrarClienteView();
            registrarClienteView.ShowDialog();

            if (registrarClienteView.isRegistered)
            {
                var newCliente = registrarClienteView.GetCliente();

                //Primero se lo paso a la capa negocio para que lo registre, si lo registra, lo pongo en la capa Presentacion
                if (TransporteDR.ClienteBO.Registrar(newCliente))
                {
                    ListaClientes.Add(newCliente);
                    CurrentCliente = newCliente;

                    MessageBox.Show($"{newCliente.RUC} Registrado con exito");
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
            RegistrarClienteView registrarClienteView = new RegistrarClienteView(true, CurrentCliente);
            registrarClienteView.ShowDialog();

            if (registrarClienteView.isUpdated)
            {
                //Primero se lo paso a la capa negocio para que lo registre, si lo registra, lo pongo en la capa Presentacion
                if (TransporteDR.ClienteBO.Actualizar(CurrentCliente))
                {
                    MessageBox.Show($"{CurrentCliente.RUC} Actualizado con exito");
                }
                else
                {
                    registrarClienteView.ToDefaultCliente(CurrentCliente);

                    MessageBox.Show("Algo ha ocurrido con el proceso de actualizacion, por favor intentar de nuevo o reiniciar el computador.\nSi el problema persiste, contactar con el encargado del Sistema");
                }
            }
        }

        private bool CanExecute_ActualizarCommand()
        {
            return !(CurrentCliente is null);
        }


        public ICommand DeleteCommand { get; set; }

        private bool CanExecute_DeleteCommand()
        {
            return !(CurrentCliente is null);
        }

        private void Execute_DeleteCommand()
        {//MORALEJA APRENDIDA: Eliminar primera todas las referencias al objeto actual, para que luego el GB lo recoja
            //CurrentPersona.Ciudad.Habitantes.Remove(CurrentPersona);
            var RUC = CurrentCliente.RUC;

            var result = MessageBox.Show("Por favor, confirmar que va a eliminar el cliente con RUC " + RUC, "Eliminar Cliente", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            switch (result)
            {
                case MessageBoxResult.OK:

                    switch (MessageBox.Show("Estás seguro?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Exclamation))
                    {
                        case MessageBoxResult.Yes:
                            if (TransporteDR.ClienteBO.Eliminar(CurrentCliente.RUC))
                            {
                                ListaClientes.Remove(CurrentCliente);

                                CurrentCliente = null;

                                MessageBox.Show($"{RUC} Eliminado con exito");
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
        ObservableCollection<Cliente> ListaClientesAux;
        public void ChangeCollection(string Filter, FilterTypeSearchCliente filterType)
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
                        ListaClientes = new ObservableCollection<Cliente>(ListaClientesAux.Where(x => x.Razon_Social.ToLower().StartsWith(Filter.ToLower())));
                        break;
                    case FilterTypeSearchCliente.DNI:
                        var listAux = new ObservableCollection<Cliente>();

                        foreach(var x in ListaClientesAux)
                        {
                            if(!(x.Persona is null))
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
        }
        #endregion
    }
}
