﻿using Entidades;
using Presentacion.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Presentacion.Views.HistorialV
{
    /// <summary>
    /// Interaction logic for RegistrarHistorialView.xaml
    /// </summary>
    public partial class RegistrarHistorialView : Window
    {
        private Historial newHistorial;
        private Historial copyHistorial_Updated;
        public bool isRegistered = false;
        private bool isUpdate;
        public bool isUpdated = false;

        private Conductor conductorSelected;
        private UnidadVehicular VehiculoSelected;

        public RegistrarHistorialView(bool isUpdate = false, Historial obj = null, IEnumerable<Conductor> listaConductores = null, IEnumerable<UnidadVehicular> listaVehiculos = null)
        {
            InitializeComponent();

            this.ComboBox_Conductor.ItemsSource = listaConductores;
            this.ComboBox_Conductor.DisplayMemberPath = "DniNavigation.FullName";
            this.ComboBox_Vehiculo.ItemsSource = listaVehiculos;
            this.ComboBox_Vehiculo.DisplayMemberPath = "FullName";


            this.isUpdate = isUpdate;

            if (!isUpdate)
            {
                newHistorial = new Historial();
            }
            else
            {
                newHistorial = obj;
                copyHistorial_Updated = new Historial();

                conductorSelected = listaConductores?.ToList().Find(x => x.Dni == newHistorial.DniConductorNavigation.Dni);
                VehiculoSelected = listaVehiculos?.ToList().Find(x => x.Placa == newHistorial.IdUnidadNavigation?.Placa);
                CopyInstance(newHistorial, copyHistorial_Updated);
                //ButtonState = "Actualizar";*/
            }

            this.DataContext = newHistorial;


            if (!(conductorSelected is null))
            {
                this.ComboBox_Conductor.SelectedItem = conductorSelected;
            }

            if (!(VehiculoSelected is null))
            {
                this.ComboBox_Vehiculo.SelectedItem = VehiculoSelected;
            }

            //ComboBox_Ciudades.ItemsSource = ciudades;
            //this.isUpdate = isUpdate;
        }

        public Historial GetHistorial()
        {
            if (!(newHistorial is null) && isRegistered)
            {
                return newHistorial;
            }

            return null;
        }

        public Historial GetHistorialBackup()
        {
            if (!(copyHistorial_Updated is null) && isUpdate)
            {
                return copyHistorial_Updated;
            }

            return null;
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            //Esto sirve para el binding, pero al estar probando todo online, parece que no es necesario
            /*if (!(conductorSelected is null))
            {
                newHistorial.DniConductorNavigation = conductorSelected;
                newHistorial.DniConductorNavigation.Historial.Add(newHistorial);
                newHistorial.DniConductor = newHistorial.DniConductorNavigation.Dni;
            }

            if (!(VehiculoSelected is null))
            {
                newHistorial.IdUnidadNavigation = VehiculoSelected;
                newHistorial.IdUnidadNavigation.Historial.Add(newHistorial);
                newHistorial.IdUnidad = newHistorial.IdUnidadNavigation.Placa;
            }*/

            var MyTuple = MyValidator.TryValidateObject(newHistorial);
            if (MyTuple.Item1)
            {
                if (!isUpdate && newHistorial.DniConductor !=null )
                {
                    isRegistered = true;
                }
                else
                {
                    isUpdated = true;
                }
                this.Close();
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

        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            if (isUpdate)
            {
                isUpdated = false;
                CopyInstance(copyHistorial_Updated, newHistorial);
            }
            this.Close();
        }

        private void CopyInstance(Historial fuente, Historial destino)
        {
            destino.Descripcion = fuente.Descripcion;
            destino.Eventualidad = fuente.Eventualidad;
            destino.Fecha = fuente.Fecha;
            destino.Lugar = fuente.Lugar;
            destino.DniConductor = fuente.DniConductor;
            destino.IdUnidad = fuente.IdUnidad;
        }

        public void ToDefaultHistorial(Historial Historial)
        {
            CopyInstance(copyHistorial_Updated, Historial);
        }

        private void ComboBox_Conductor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            conductorSelected = ComboBox_Conductor.SelectedItem as Conductor;
            newHistorial.DniConductor = conductorSelected.Dni;
        }

        private void ComboBox_Vehiculo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VehiculoSelected = ComboBox_Vehiculo.SelectedItem as UnidadVehicular;
            newHistorial.IdUnidad = VehiculoSelected.Placa;
        }
    }
}
