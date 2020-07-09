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

namespace Presentacion.Views.UnidadesVehicularesV
{
    /// <summary>
    /// Interaction logic for RegistrarUnidadVehicularView.xaml
    /// </summary>
    public partial class RegistrarUnidadVehicularView : Window
    {
        private Unidad_Vehicular newUnidadVehicular;
        private Unidad_Vehicular copyUnidadVehicular_Updated;
        public bool isRegistered = false;
        private bool isUpdate;
        public bool isUpdated = false;

        public RegistrarUnidadVehicularView(bool isUpdate = false, Unidad_Vehicular obj = null)
        {
            InitializeComponent();

            this.isUpdate = isUpdate;

            if (!isUpdate)
            {
                newUnidadVehicular = new Unidad_Vehicular();
            }
            else
            {
                newUnidadVehicular = obj;
                copyUnidadVehicular_Updated = new Unidad_Vehicular();

                CopyInstance(newUnidadVehicular, copyUnidadVehicular_Updated);
                //ButtonState = "Actualizar";*/
            }

            this.DataContext = newUnidadVehicular;
            //ComboBox_Ciudades.ItemsSource = ciudades;
            //this.isUpdate = isUpdate;
        }

        public Unidad_Vehicular GetUnidadVehicular()
        {
            if (!(newUnidadVehicular is null) && isRegistered)
            {
                return newUnidadVehicular;
            }

            return null;
        }

        public Unidad_Vehicular GetUnidadVehicularBackup()
        {
            if (!(copyUnidadVehicular_Updated is null) && isUpdated)
            {
                return copyUnidadVehicular_Updated;
            }

            return null;
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            var MyTuple = MyValidator.TryValidateObject(newUnidadVehicular);
            if (MyTuple.Item1)
            {
                if (!isUpdate)
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
                CopyInstance(copyUnidadVehicular_Updated, newUnidadVehicular);
            }
            this.Close();
        }

        private void CopyInstance(Unidad_Vehicular fuente, Unidad_Vehicular destino)
        {
            destino.Marca = fuente.Marca;
            destino.Placa = fuente.Placa;
            destino.Serie_Chasis = fuente.Serie_Chasis;
            destino.Y_Fabricacion = fuente.Y_Fabricacion;
        }

        public void ToDefaultUnidadVehicular(Unidad_Vehicular UnidadVehicular)
        {
            CopyInstance(copyUnidadVehicular_Updated, UnidadVehicular);
        }
    }
}
