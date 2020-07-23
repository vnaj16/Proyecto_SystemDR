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

namespace Presentacion.Views.ProveedoresV
{
    /// <summary>
    /// Interaction logic for RegistrarProveedorView.xaml
    /// </summary>
    public partial class RegistrarProveedorView : Window
    {
        private Proveedor newProveedor;
        private Proveedor copyProveedor_Updated;
        public bool isRegistered = false;
        private bool isUpdate;
        public bool isUpdated = false;

        public RegistrarProveedorView(bool isUpdate = false, Proveedor obj = null)
        {
            InitializeComponent();

            this.isUpdate = isUpdate;

            if (!isUpdate)
            {
                newProveedor = new Proveedor();
            }
            else
            {
                newProveedor = obj;
                copyProveedor_Updated = new Proveedor();

                if (!(obj.DniRlNavigation is null))
                {
                    copyProveedor_Updated.DniRlNavigation = new Persona();
                }

                CopyInstance(newProveedor, copyProveedor_Updated);
                //ButtonState = "Actualizar";*/
            }

            this.DataContext = newProveedor;
            //ComboBox_Ciudades.ItemsSource = ciudades;
            //this.isUpdate = isUpdate;
        }

        public Proveedor GetProveedor()
        {
            if (!(newProveedor is null) && isRegistered)
            {
                return newProveedor;
            }

            return null;
        }

        public Proveedor GetProveedorBackup()
        {
            if (!(copyProveedor_Updated is null) && isUpdated)
            {
                return copyProveedor_Updated;
            }

            return null;
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            var MyTuple = MyValidator.TryValidateObject(newProveedor);
            if (MyTuple.Item1)
            {
                if (!isUpdate)
                {
                    if (String.IsNullOrWhiteSpace(newProveedor.DniRl))
                    {
                        newProveedor.DniRlNavigation = null;
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
                CopyInstance(copyProveedor_Updated, newProveedor);
            }
            this.Close();
        }

        private void TextBox_DNI_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(TextBox_DNI.Text/*newProveedor.DNI*/))
            {
                newProveedor.DniRl = TextBox_DNI.Text;
                if (newProveedor.DniRlNavigation is null)
                {
                    newProveedor.DniRlNavigation = new Persona() { Dni = newProveedor.DniRl };
                }
                else
                {
                    if (newProveedor.DniRlNavigation.Dni != newProveedor.DniRl)
                    {
                        newProveedor.DniRlNavigation.Dni = newProveedor.DniRl;
                    }
                }
            }

        }

        private void CopyInstance(Proveedor fuente, Proveedor destino)
        {
            destino.Ruc = fuente.Ruc;
            destino.RazonSocial = fuente.RazonSocial;
            destino.Direccion = fuente.Direccion;
            destino.DniRl = fuente.DniRl;
            destino.Tipo = fuente.Tipo;

            if (!(fuente.DniRlNavigation is null))
            {
                destino.DniRlNavigation.Dni = fuente.DniRlNavigation?.Dni;
                destino.DniRlNavigation.Nombre = fuente.DniRlNavigation?.Nombre;
                destino.DniRlNavigation.Apellido = fuente.DniRlNavigation?.Apellido;
                destino.DniRlNavigation.FechaNac = fuente.DniRlNavigation?.FechaNac;
                destino.DniRlNavigation.Nacionalidad = fuente.DniRlNavigation?.Nacionalidad;
            }
        }

        public void ToDefaultProveedor(Proveedor Proveedor)
        {
            CopyInstance(copyProveedor_Updated, Proveedor);
        }
    }
}
