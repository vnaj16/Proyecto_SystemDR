using Entidades;
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

namespace Presentacion.Views.ConductoresV
{
    /// <summary>
    /// Interaction logic for RegistrarConductorView.xaml
    /// </summary>
    public partial class RegistrarConductorView : Window
    {
        private Conductor newConductor;
        private Conductor copyConductor_Updated;
        public bool isRegistered = false;
        private bool isUpdate;
        public bool isUpdated = false;

        public RegistrarConductorView(bool isUpdate = false, Conductor obj = null)
        {
            InitializeComponent();

            this.isUpdate = isUpdate;

            if (!isUpdate)
            {
                newConductor = new Conductor() { 
                    DniNavigation = new Persona()
                };
            }
            else
            {
                newConductor = obj;
                copyConductor_Updated = new Conductor();

                if (!(obj.DniNavigation is null))
                {
                    copyConductor_Updated.DniNavigation = new Persona();
                }

                CopyInstance(newConductor, copyConductor_Updated);
                //ButtonState = "Actualizar";*/
            }

            this.DataContext = newConductor;
            //ComboBox_Ciudades.ItemsSource = ciudades;
            //this.isUpdate = isUpdate;
        }

        public Conductor GetConductor()
        {
            if (!(newConductor is null) && isRegistered)
            {
                return newConductor;
            }

            return null;
        }

        public Conductor GetConductorBackup()
        {
            if (!(copyConductor_Updated is null) && isUpdated)
            {
                return copyConductor_Updated;
            }

            return null;
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            var MyTuple = MyValidator.TryValidateObject(newConductor);
            if (MyTuple.Item1)
            {
                if (!isUpdate)
                {
                    if (String.IsNullOrWhiteSpace(newConductor.DniNavigation.Dni))
                    {
                        newConductor.DniNavigation.Dni = newConductor.Dni;
                    }

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
                CopyInstance(copyConductor_Updated, newConductor);
            }
            this.Close();
        }


        private void CopyInstance(Conductor fuente, Conductor destino)
        {
            destino.Brevete = fuente.Brevete;
            destino.FechaInicio = fuente.FechaInicio;
            destino.Direccion = fuente.Direccion;
            destino.Dni = fuente.Dni;
            destino.GradoInstruccion = fuente.GradoInstruccion;
            destino.LugarNac = fuente.LugarNac;
            destino.DniNavigation.Nacionalidad = fuente.DniNavigation.Nacionalidad;

            if (!(fuente.DniNavigation is null))
            {
                destino.DniNavigation.Dni = fuente.DniNavigation?.Dni;
                destino.DniNavigation.Nombre = fuente.DniNavigation?.Nombre;
                destino.DniNavigation.Apellido = fuente.DniNavigation?.Apellido;
                destino.DniNavigation.FechaNac = fuente.DniNavigation?.FechaNac;
                destino.DniNavigation.Nacionalidad = fuente.DniNavigation?.Nacionalidad;
            }
        }

        public void ToDefaultConductor(Conductor conductor)
        {
            CopyInstance(copyConductor_Updated, conductor);
        }
    }
}
