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
        private UnidadVehicular newUnidadVehicular;
        private UnidadVehicular copyUnidadVehicular_Updated;
        public bool isRegistered = false;
        private bool isUpdate;
        public bool isUpdated = false;

        public RegistrarUnidadVehicularView(bool isUpdate = false, UnidadVehicular obj = null)
        {
            InitializeComponent();

            this.isUpdate = isUpdate;

            if (!isUpdate)
            {
                newUnidadVehicular = new UnidadVehicular();
            }
            else
            {
                TextBox_Placa.IsEnabled = false;

                newUnidadVehicular = obj;
                copyUnidadVehicular_Updated = new UnidadVehicular();

                CopyInstance(newUnidadVehicular, copyUnidadVehicular_Updated);
                //ButtonState = "Actualizar";*/
            }

            this.DataContext = newUnidadVehicular;
            //ComboBox_Ciudades.ItemsSource = ciudades;
            //this.isUpdate = isUpdate;
        }

        public UnidadVehicular GetUnidadVehicular()
        {
            if (!(newUnidadVehicular is null) && isRegistered)
            {
                return newUnidadVehicular;
            }

            return null;
        }

        public UnidadVehicular GetUnidadVehicularBackup()
        {
            if (!(copyUnidadVehicular_Updated is null) && isUpdate)
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
                    if (MessageBox.Show("La Placa ingresada es correcta? (luego no se podrá modificar)"
    , "Confirmación", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        isRegistered = true;
                        this.Close();
                    }
                }
                else
                {
                    isUpdated = true;
                    this.Close();
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

        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            if (isUpdate)
            {
                isUpdated = false;
                CopyInstance(copyUnidadVehicular_Updated, newUnidadVehicular);
            }
            this.Close();
        }

        private void CopyInstance(UnidadVehicular fuente, UnidadVehicular destino)
        {
            destino.Marca = fuente.Marca;
            destino.Placa = fuente.Placa;
            destino.SerieChasis = fuente.SerieChasis;
            destino.YFabricacion = fuente.YFabricacion;
        }

        public void ToDefaultUnidadVehicular(UnidadVehicular UnidadVehicular)
        {
            CopyInstance(copyUnidadVehicular_Updated, UnidadVehicular);
        }
    }
}
