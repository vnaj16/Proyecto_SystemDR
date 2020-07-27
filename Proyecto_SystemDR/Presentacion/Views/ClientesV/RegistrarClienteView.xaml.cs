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

namespace Presentacion.Views.ClientesV
{
    /// <summary>
    /// Interaction logic for RegistrarClienteView.xaml
    /// </summary>
    public partial class RegistrarClienteView : Window
    {
        private Cliente newCliente;
        private Cliente copyCliente_Updated;
        public bool isRegistered = false;
        private bool isUpdate;
        public bool isUpdated = false;


        public RegistrarClienteView(bool isUpdate = false, Cliente obj = null)
        {
            InitializeComponent();

            this.isUpdate = isUpdate;

            if (!isUpdate)
            {
                newCliente = new Cliente();
            }
            else
            {
                TextBox_RUC.IsEnabled = false;
                newCliente = obj;
                copyCliente_Updated = new Cliente();

                if (!(obj.DniRlNavigation is null))//Si es que tiene persona
                {
                    copyCliente_Updated.DniRlNavigation = new Persona();
                    TextBox_DNI.IsEnabled = false;
                }

                CopyInstance(newCliente, copyCliente_Updated);
                //ButtonState = "Actualizar";*/
            }

            this.DataContext = newCliente;
            //ComboBox_Ciudades.ItemsSource = ciudades;
            //this.isUpdate = isUpdate;
        }

        public Cliente GetCliente()
        {
            if (!(newCliente is null) && isRegistered)
            {
                return newCliente;
            }

            return null;
        }

        public Cliente GetClienteBackup()
        {
            if (!(copyCliente_Updated is null) && isUpdated)
            {
                return copyCliente_Updated;
            }

            return null;
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            var MyTuple = MyValidator.TryValidateObject(newCliente);
            if (MyTuple.Item1)
            {
                if (!isUpdate)
                {
                    if (String.IsNullOrWhiteSpace(newCliente.DniRl))
                    {
                        newCliente.DniRlNavigation = null;
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
                CopyInstance(copyCliente_Updated, newCliente);
            }
            this.Close();
        }

        private void TextBox_DNI_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(TextBox_DNI.Text/*newCliente.Dni*/))
            {
                newCliente.DniRl = TextBox_DNI.Text;
                if (newCliente.DniRlNavigation is null)
                {
                    newCliente.DniRlNavigation = new Persona() { Dni = newCliente.DniRl, Tipo ="cli" };
                }
                else
                {
                    if (newCliente.DniRlNavigation.Dni != newCliente.DniRl)
                    {
                        newCliente.DniRlNavigation.Dni = newCliente.DniRl;
                    }
                }
            }

        }

        private void CopyInstance(Cliente fuente, Cliente destino)
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

        public void ToDefaultCliente(Cliente cliente)
        {
            CopyInstance(copyCliente_Updated, cliente);
        }
    }
}
