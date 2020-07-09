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
                    Persona = new Persona()
                };
            }
            else
            {
                newConductor = obj;
                copyConductor_Updated = new Conductor();

                if (!(obj.Persona is null))
                {
                    copyConductor_Updated.Persona = new Persona();
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
                    if (String.IsNullOrWhiteSpace(newConductor.Persona.DNI))
                    {
                        newConductor.Persona.DNI = newConductor.DNI;
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
            destino.Fecha_Inicio = fuente.Fecha_Inicio;
            destino.Direccion = fuente.Direccion;
            destino.DNI = fuente.DNI;
            destino.Grado_Instruccion = fuente.Grado_Instruccion;
            destino.Lugar_Nac = fuente.Lugar_Nac;
            destino.Personalidad = fuente.Personalidad;

            if (!(fuente.Persona is null))
            {
                destino.Persona.DNI = fuente.Persona?.DNI;
                destino.Persona.Nombre = fuente.Persona?.Nombre;
                destino.Persona.Apellido = fuente.Persona?.Apellido;
                destino.Persona.Fecha_Nac = fuente.Persona?.Fecha_Nac;
                destino.Persona.Nacionalidad = fuente.Persona?.Nacionalidad;
            }
        }

        public void ToDefaultConductor(Conductor conductor)
        {
            CopyInstance(copyConductor_Updated, conductor);
        }
    }
}
