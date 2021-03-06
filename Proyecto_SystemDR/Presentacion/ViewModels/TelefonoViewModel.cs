﻿using Entidades;
using Negocio.Core;
using Presentacion.Helpers;
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
    public class TelefonoViewModel : BindableBase
    {
        private string textHidden;
        public string TextHidden
        {
            get { return textHidden; }
            set { SetProperty(ref textHidden, value); }
        }

        public TelefonoViewModel(Persona DniNavigation, string RazonSocial = null)
        {
            if (!(RazonSocial is null))
            {
                TextHidden = $"Empresa: {RazonSocial}";
            }

            CurrentDniNavigation = DniNavigation;

            if (CurrentDniNavigation.Telefono is null)
            {
                CurrentDniNavigation.Telefono = new List<Telefono>();
            }

            newTelefono = new Telefono() { Dni = DniNavigation.Dni, DniNavigation = DniNavigation };

            ListaTelefonos = new ObservableCollection<Telefono>(CurrentDniNavigation.Telefono);

            //AgregarCommand = new DelegateCommand(Execute_AgregarCommand, CanExecute_AgregarCommand).ObservesProperty(()=>NewTelefono.Numero);
            AgregarCommand = new DelegateCommand(Execute_AgregarCommand);
            DeleteCommand = new DelegateCommand(Execute_DeleteCommand, CanExecute_DeleteCommand).ObservesProperty(() => CurrentTelefono);
        }

        private Persona currentDniNavigation;

        public Persona CurrentDniNavigation
        {
            get { return currentDniNavigation; }
            set { SetProperty(ref currentDniNavigation, value); }
        }

        private Telefono newTelefono;

        public Telefono NewTelefono
        {
            get { return newTelefono; }
            set { SetProperty(ref newTelefono, value); }
        }

        private Telefono currentTelefono;

        public Telefono CurrentTelefono
        {
            get { return currentTelefono; }
            set { SetProperty(ref currentTelefono, value); }
        }

        private ObservableCollection<Telefono> listaTelefonos;

        public ObservableCollection<Telefono> ListaTelefonos
        {
            get { return listaTelefonos; }
            set { SetProperty(ref listaTelefonos, value); }
        }

        #region COMMANDS
        public ICommand AgregarCommand { get; set; }

        private void Execute_AgregarCommand()
        {
            try
            {
                if (!(String.IsNullOrWhiteSpace(NewTelefono.Numero)))
                {
                    var MyTuple = MyValidator.TryValidateObject(NewTelefono);
                    if (MyTuple.Item1)
                    {
                        if (TransporteDR.TelefonoBO.Registrar(NewTelefono))
                        {
                            ListaTelefonos.Add(NewTelefono);

                            MessageBox.Show($"{NewTelefono.Numero} Registrado");

                            NewTelefono = new Telefono() { Dni = CurrentDniNavigation.Dni, DniNavigation = CurrentDniNavigation };
                        }
                        else
                        {
                            MessageBox.Show("Algo inesperado ocurrio");
                        }
                    }
                    else
                    {
                        string Messages = "";
                        foreach (var x in MyTuple.Item2)
                        {
                            Messages += x.ErrorMessage + "\n";
                        }

                        MessageBox.Show(Messages);
                    }
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


        public ICommand DeleteCommand { get; set; }

        private bool CanExecute_DeleteCommand()
        {
            return !(CurrentTelefono is null);
        }

        private void Execute_DeleteCommand()
        {//MORALEJA APRENDIDA: Eliminar primera todas las referencias al objeto actual, para que luego el GB lo recoja
            //CurrentDniNavigation.Ciudad.Habitantes.Remove(CurrentDniNavigation);
            var Numero = CurrentTelefono.Numero;

            var result = MessageBox.Show("Por favor, confirmar que va a eliminar el numero" + Numero, "Eliminar Telefono", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            switch (result)
            {
                case MessageBoxResult.OK:

                    switch (MessageBox.Show("Estás seguro?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Exclamation))
                    {
                        case MessageBoxResult.Yes:
                            try
                            {
                                if (TransporteDR.TelefonoBO.Eliminar(CurrentTelefono))
                                {
                                    ListaTelefonos.Remove(CurrentTelefono);

                                    CurrentTelefono = null;
                                    NewTelefono = new Telefono() { Dni = CurrentDniNavigation.Dni, DniNavigation = CurrentDniNavigation };

                                    MessageBox.Show($"{Numero} Eliminado con exito");
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


    }
}
