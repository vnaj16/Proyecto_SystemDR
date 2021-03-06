﻿using Entidades;
using Negocio.Core;
using Presentacion.Helpers;
using Presentacion.Views;
using Presentacion.Views.ProveedoresV;
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
    public class ProveedoresViewModel : BindableBase
    {
        #region Singleton
        private static ProveedoresViewModel instance = null;

        private ProveedoresViewModel()
        {
            LoadData();

            AgregarCommand = new DelegateCommand(Execute_AgregarCommand);
            ActualizarCommand = new DelegateCommand(Execute_ActualizarCommand, CanExecute_ActualizarCommand).ObservesProperty(() => CurrentProveedor);
            DeleteCommand = new DelegateCommand(Execute_DeleteCommand, CanExecute_DeleteCommand).ObservesProperty(() => CurrentProveedor);
            VerTelefonosCommand = new DelegateCommand(Execute_VerTelefonosCommand, CanExecute_VerTelefonosCommand).ObservesProperty(() => CurrentProveedor);
        }

        public void LoadData()
        {
            ListaProveedores = new ObservableCollection<Proveedor>(TransporteDR.ProveedorBO.GetAll());

            ListaProveedoresAux = ListaProveedores; //Este es otra referencia a la Lista que traigo de la DB, me sirve para cuando tenga que cambiar la lista que se muestra
        }
        public static ProveedoresViewModel Instance
        {
            get
            {
                if (instance is null)
                {
                    instance = new ProveedoresViewModel();
                }
                return instance;
            }
        }
        #endregion

        private Proveedor currentProveedor;

        public Proveedor CurrentProveedor
        {
            get { return currentProveedor; }
            set { SetProperty(ref currentProveedor, value); }
        }

        private ObservableCollection<Proveedor> listaProveedores;

        public ObservableCollection<Proveedor> ListaProveedores
        {
            get { return listaProveedores; }
            set { SetProperty(ref listaProveedores, value); }
        }

        #region COMMANDS
        public ICommand AgregarCommand { get; set; }

        private void Execute_AgregarCommand()
        {
            RegistrarProveedorView registrarProveedorView = new RegistrarProveedorView();
            registrarProveedorView.ShowDialog();

            if (registrarProveedorView.isRegistered)
            {
                var newProveedor = registrarProveedorView.GetProveedor();

                try
                {
                    //Primero se lo paso a la capa negocio para que lo registre, si lo registra, lo pongo en la capa Presentacion
                    if (TransporteDR.ProveedorBO.Registrar(newProveedor))
                    {
                        LoadData();
                        CurrentProveedor = ListaProveedores.FirstOrDefault(x => x.Ruc == newProveedor.Ruc);

                        /*ListaProveedores.Add(newProveedor);
                        CurrentProveedor = newProveedor;*/

                        MessageBox.Show($"{newProveedor.Ruc} Registrado con exito");
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
            /*if (registrarPersonaView.isRegistered)
            {
                listaPersonas.Add(registrarPersonaView.GetPersona());
            }*/

            //MessageBox.Show("Agregar persona View");
        }


        public ICommand ActualizarCommand { get; set; }

        private void Execute_ActualizarCommand()
        {
            RegistrarProveedorView registrarProveedorView = new RegistrarProveedorView(true, CurrentProveedor);
            registrarProveedorView.ShowDialog();

            if (registrarProveedorView.isUpdated)
            {
                try
                {
                    //Primero se lo paso a la capa negocio para que lo registre, si lo registra, lo pongo en la capa Presentacion
                    if (TransporteDR.ProveedorBO.Actualizar(CurrentProveedor))
                    {
                        MessageBox.Show($"{CurrentProveedor.Ruc} Actualizado con exito");
                    }
                    else
                    {
                        registrarProveedorView.ToDefaultProveedor(CurrentProveedor);

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
            return !(CurrentProveedor is null);
        }


        public ICommand DeleteCommand { get; set; }

        private bool CanExecute_DeleteCommand()
        {
            return !(CurrentProveedor is null);
        }

        private void Execute_DeleteCommand()
        {//MORALEJA APRENDIDA: Eliminar primera todas las referencias al objeto actual, para que luego el GB lo recoja
            //CurrentPersona.Ciudad.Habitantes.Remove(CurrentPersona);
            var RUC = CurrentProveedor.Ruc;

            var result = MessageBox.Show("Por favor, confirmar que va a eliminar el Proveedor con RUC " + RUC, "Eliminar Proveedor", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            switch (result)
            {
                case MessageBoxResult.OK:

                    switch (MessageBox.Show("Estás seguro?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Exclamation))
                    {
                        case MessageBoxResult.Yes:
                            try
                            {
                                if (TransporteDR.ProveedorBO.Eliminar(CurrentProveedor.Ruc))
                                {
                                    LoadData();
                                    //ListaProveedores.Remove(CurrentProveedor);

                                    CurrentProveedor = null;

                                    MessageBox.Show($"{RUC} Eliminado con exito");
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

        public ICommand VerTelefonosCommand { get; set; }

        private bool CanExecute_VerTelefonosCommand()
        {
            if (!(CurrentProveedor is null))
            {
                return !(CurrentProveedor.DniRlNavigation is null);
            }
            else
            {
                return false;
            }
        }

        private void Execute_VerTelefonosCommand()
        {
            TelefonoView telefonoView = new TelefonoView(CurrentProveedor.DniRlNavigation);
            telefonoView.ShowDialog();
        }

        #endregion

        #region METHODS
        ObservableCollection<Proveedor> ListaProveedoresAux;
        public void ChangeCollection(string Filter, FilterTypeSearchProveedor filterType)
        {
            if (String.IsNullOrWhiteSpace(Filter))
            {
                ListaProveedores = ListaProveedoresAux;
            }
            else
            {
                switch (filterType)
                {
                    case FilterTypeSearchProveedor.RUC:
                        ListaProveedores = new ObservableCollection<Proveedor>(ListaProveedoresAux.Where(x => x.Ruc.StartsWith(Filter)));
                        break;

                    case FilterTypeSearchProveedor.RazonSocial:
                        ListaProveedores = new ObservableCollection<Proveedor>(ListaProveedoresAux.Where(x => x.RazonSocial.ToLower().StartsWith(Filter.ToLower())));
                        break;
                    case FilterTypeSearchProveedor.DNI:
                        var listAux = new ObservableCollection<Proveedor>();

                        foreach (var x in ListaProveedoresAux)
                        {
                            if (!(x.DniRlNavigation is null))
                            {
                                if (x.DniRlNavigation.Dni.StartsWith(Filter))
                                {
                                    listAux.Add(x);
                                }
                            }
                        }

                        ListaProveedores = listAux;
                        break;
                    case FilterTypeSearchProveedor.Productos:
                        ListaProveedores = new ObservableCollection<Proveedor>(ListaProveedoresAux.Where(x => x.Productos.ToLower().Contains(Filter.ToLower())));
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
