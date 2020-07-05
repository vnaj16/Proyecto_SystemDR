using Entidades;
using Negocio.Core;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion.ViewModels
{
    public class ProveedoresViewModel : BindableBase
    {
        #region Singleton
        private static ProveedoresViewModel instance = null;

        private ProveedoresViewModel()
        {
            //listaProveedores = new ObservableCollection<Proveedor>(TransporteDR..GetAll());

            listaProveedores = new ObservableCollection<Proveedor>();

            //PRUEBA LOCAL
            listaProveedores.Add(new Proveedor()
            {
                RUC = "75412416",
                Razon_Social = "Meca Razon",
                Direccion = "OLOS",
                Persona = new Persona()
                {
                    DNI = "78454545",
                    Nombre = "Luis",
                    Apellido="Orteha"
                }
            });

            ListaProveedoresAux = ListaProveedores; //Este es otra referencia a la Lista que traigo de la DB, me sirve para cuando tenga que cambiar la lista que se muestra

            /*AgregarCommand = new DelegateCommand(Execute_AgregarCommand);
            ActualizarCommand = new DelegateCommand(Execute_ActualizarCommand, CanExecute_ActualizarCommand).ObservesProperty(() => CurrentCliente);
            DeleteCommand = new DelegateCommand(Execute_DeleteCommand, CanExecute_DeleteCommand).ObservesProperty(() => CurrentCliente);*/
        }
        public static ProveedoresViewModel Instance
        {
            get
            {
                if (instance is null)
                {
                    instance = new ProveedoresViewModel();
                }
                return instance;
            }
        }
        #endregion

        private Proveedor currentProveedor;

        public Proveedor CurrentProveedor
        {
            get { return currentProveedor; }
            set { SetProperty(ref currentProveedor, value); }
        }

        private ObservableCollection<Proveedor> listaProveedores;

        public ObservableCollection<Proveedor> ListaProveedores
        {
            get { return listaProveedores; }
            set { SetProperty(ref listaProveedores, value); }
        }

        #region METHODS
        ObservableCollection<Proveedor> ListaProveedoresAux;
        /*public void ChangeCollection(string Filter, FilterTypeSearchCliente filterType)
        {
            if (String.IsNullOrWhiteSpace(Filter))
            {
                ListaClientes = ListaClientesAux;
            }
            else
            {
                switch (filterType)
                {
                    case FilterTypeSearchCliente.RUC:
                        ListaClientes = new ObservableCollection<Cliente>(ListaClientesAux.Where(x => x.RUC.StartsWith(Filter)));
                        break;

                    case FilterTypeSearchCliente.RazonSocial:
                        ListaClientes = new ObservableCollection<Cliente>(ListaClientesAux.Where(x => x.Razon_Social.StartsWith(Filter)));
                        break;
                    case FilterTypeSearchCliente.DNI:
                        var listAux = new ObservableCollection<Cliente>();

                        foreach (var x in ListaClientesAux)
                        {
                            if (!(x.Persona is null))
                            {
                                if (x.Persona.DNI.StartsWith(Filter))
                                {
                                    listAux.Add(x);
                                }
                            }
                        }

                        ListaClientes = listAux;
                        break;
                    default:
                        break;
                }
            }
        }*/
        #endregion
    }
}
