﻿using Entidades;
using Negocio.Core;
using Presentacion.Helpers;
using Presentacion.Views;
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
            LoadData();

            AgregarCommand = new DelegateCommand(Execute_AgregarCommand);
            ActualizarCommand = new DelegateCommand(Execute_ActualizarCommand, CanExecute_ActualizarCommand).ObservesProperty(() => CurrentConductor);
            DeleteCommand = new DelegateCommand(Execute_DeleteCommand, CanExecute_DeleteCommand).ObservesProperty(() => CurrentConductor);
            VerTelefonosCommand = new DelegateCommand(Execute_VerTelefonosCommand, CanExecute_VerTelefonosCommand).ObservesProperty(() => CurrentConductor);
        }

        public void LoadData()
        {
            ListaConductores = new ObservableCollection<Conductor>(TransporteDR.ConductorBO.GetAll());

            ListaConductoresAux = ListaConductores; //Este es otra referencia a la Lista que traigo de la DB, me sirve para cuando tenga que cambiar la lista que se muestra
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

                try
                {
                    //Primero se lo paso a la capa negocio para que lo registre, si lo registra, lo pongo en la capa Presentacion
                    if (TransporteDR.ConductorBO.Registrar(newConductor))
                    {
                        LoadData();
                        CurrentConductor = ListaConductores.FirstOrDefault(x => x.Dni == newConductor.Dni);


                        MessageBox.Show($"{newConductor.Dni} Registrado con exito");
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
            RegistrarConductorView registrarConductorView = new RegistrarConductorView(true, CurrentConductor);
            registrarConductorView.ShowDialog();

            if (registrarConductorView.isUpdated)
            {
                try
                {
                    if (TransporteDR.ConductorBO.Actualizar(CurrentConductor))
                    {
                        MessageBox.Show($"{CurrentConductor.Dni} Actualizado con exito");
                    }
                    else
                    {
                        registrarConductorView.ToDefaultConductor(CurrentConductor);

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
            return !(CurrentConductor is null);
        }


        public ICommand DeleteCommand { get; set; }

        private bool CanExecute_DeleteCommand()
        {
            return !(CurrentConductor is null);
        }

        private void Execute_DeleteCommand()
        {//MORALEJA APRENDIDA: Eliminar primera todas las referencias al objeto actual, para que luego el GB lo recoja
            //CurrentDniNavigation.Ciudad.Habitantes.Remove(CurrentDniNavigation);
            var Dni = CurrentConductor.Dni;

            var result = MessageBox.Show("Por favor, confirmar que va a eliminar el Conductor con Dni " + Dni, "Eliminar Conductor", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            switch (result)
            {
                case MessageBoxResult.OK:

                    switch (MessageBox.Show("Estás seguro?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Exclamation))
                    {
                        case MessageBoxResult.Yes:
                            try
                            {
                                if (TransporteDR.ConductorBO.Eliminar(CurrentConductor.Dni))
                                {
                                    LoadData();

                                    CurrentConductor = null;

                                    MessageBox.Show($"{Dni} Eliminado con exito");
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
            if (!(CurrentConductor is null))
            {
                return !(CurrentConductor.DniNavigation is null);
            }
            else
            {
                return false;
            }
        }

        private void Execute_VerTelefonosCommand()
        {
            try
            {
                TelefonoView telefonoView = new TelefonoView(CurrentConductor.DniNavigation);
                telefonoView.ShowDialog();
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

        #endregion

        #region METHODS
        ObservableCollection<Conductor> ListaConductoresAux;
        public void ChangeCollection(string Filter, FilterTypeSearchConductor filterType)
        {
            if (String.IsNullOrWhiteSpace(Filter))
            {
                ListaConductores = ListaConductoresAux;
            }
            else
            {
                switch (filterType)
                {
                    case FilterTypeSearchConductor.DNI:
                        var listAux = new ObservableCollection<Conductor>();

                        foreach (var x in ListaConductoresAux)
                        {
                            if (!(x.DniNavigation is null))
                            {
                                if (x.DniNavigation.Dni.StartsWith(Filter))
                                {
                                    listAux.Add(x);
                                }
                            }
                        }

                        ListaConductores = listAux;
                        break;

                    case FilterTypeSearchConductor.Brevete:
                        ListaConductores = new ObservableCollection<Conductor>(ListaConductoresAux.Where(x => x.Brevete.ToLower().StartsWith(Filter.ToLower())));
                        break;
                    case FilterTypeSearchConductor.Nombre:
                        var listAux1 = new ObservableCollection<Conductor>();

                        foreach (var x in ListaConductoresAux)
                        {
                            if (!(x.DniNavigation is null))
                            {
                                if (x.DniNavigation.Nombre.ToLower().StartsWith(Filter.ToLower()))
                                {
                                    listAux1.Add(x);
                                }
                            }
                        }

                        ListaConductores = listAux1;
                        break;
                    case FilterTypeSearchConductor.Apellido:
                        var listAux2 = new ObservableCollection<Conductor>();

                        foreach (var x in ListaConductoresAux)
                        {
                            if (!(x.DniNavigation is null))
                            {
                                if (x.DniNavigation.Apellido.ToLower().StartsWith(Filter.ToLower()))
                                {
                                    listAux2.Add(x);
                                }
                            }
                        }

                        ListaConductores = listAux2;
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
