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
                newCliente = obj;
                copyCliente_Updated = new Cliente()
                {
                    RUC = obj.RUC,
                    Razon_Social = obj.Razon_Social,
                    Direccion = obj.Direccion,
                    DNI = obj.DNI,
                    Tipo = obj.Tipo,
                    Persona = new Persona()
                    {
                        DNI = obj.Persona?.DNI,
                        Nombre = obj.Persona?.Nombre,
                        Apellido = obj.Persona?.Apellido,
                        Fecha_Nac = obj.Persona?.Fecha_Nac,
                        Nacionalidad = obj.Persona?.Nacionalidad
                    }
                };
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
                    if (String.IsNullOrWhiteSpace(newCliente.DNI))
                    {
                        newCliente.Persona = null;
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
                newCliente = copyCliente_Updated;
            }
            this.Close();
        }

        private void TextBox_DNI_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(TextBox_DNI.Text/*newCliente.DNI*/))
            {
                newCliente.DNI = TextBox_DNI.Text;
                if (newCliente.Persona is null)
                {
                    newCliente.Persona = new Persona() { DNI = newCliente.DNI };
                }
                else
                {
                    if(newCliente.Persona.DNI != newCliente.DNI)
                    {
                        newCliente.Persona.DNI = newCliente.DNI;
                    }
                }
            }

        }
    }
}
