    using Datos.Interfaces;
using Datos.Repositories;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Business_Objects
{
    public class ClienteBO
    {
        private IClienteRepository clienteRepository;
        private List<Cliente> listaClientes;

        public ClienteBO(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
            GetAll();
        }

        public List<Cliente> GetAll() //READ
        {
            if (listaClientes != null && listaClientes.Count != 0)
            {
                return listaClientes;
            }
            else
            {
                listaClientes = clienteRepository.GetAll().ToList();

                /*foreach (Cliente x in listaDB)
                {
                    ClienteDTO obj = new ClienteDTO();
                    MyMapper.Map(x, obj);
                    listaClienteDTO.Add(obj);
                }*/

                return listaClientes;
            }
        }

        public bool Registrar(Cliente obj)
        {
            if (!String.IsNullOrWhiteSpace(obj.Ruc))//EVALUO CAMPOS OBLIGATORIOS
            {
                if (!listaClientes.Exists(x => x.Ruc == obj.Ruc))//EVALUO SI YA EXISTE
                {
                    //Si tiene DNI, debe tener persona, sino tiene, la creo
                    if (!String.IsNullOrWhiteSpace(obj.DniRl))
                    {
                        if(obj.DniRlNavigation is null)
                        {
                            obj.DniRlNavigation = new Persona() { Dni = obj.DniRl };
                        }
                        else
                        {
                            obj.DniRlNavigation.Dni = obj.DniRl;
                        }
                    }

                    //Primero verifico si se agrego de manera correcta a la DB, luego lo agrego a la Lista in Memory
                    var Result = clienteRepository.Insert(obj);

                    if(Result) listaClientes.Add(obj);

                    return Result;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 1. Veo que la PK este
        /// 2. Obtengo el cliente con ese Ruc
        /// 3. Verifico que si exista ese cliente
        /// 4. Mapeo los datos de cliente (siempre y cuando el dato que venga no este vacio ni null, para no sobreescribir en caso la data existente)
        /// 5. Si el cliente actual no tiene DNI, se lo asigno del objeto nuevo
        /// 6 si el DNI del objeto nuevo no es vacio paso a verificar
        /// 7. si la persona es null, es decir no tiene, se la creo y le asigno el DNI
        /// 8. si la persona no es null, es decir que si hay una persona. Paso a hacer otra verificacion
        /// 9. si el curren.Persona es null, le referencio el obj.Persona que ha llegado
        /// 10. si no es null, es decir si existe persona, mapeo los datos de obj.Persona
        /// 11. El current persona lo mando al repository y retorno ese resultado
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Actualizar(Cliente obj)
        {
            if (!String.IsNullOrWhiteSpace(obj.Ruc))//EVALUO CAMPOS OBLIGATORIOS
            {
                Cliente current = listaClientes.FirstOrDefault(x => x.Ruc == obj.Ruc);
                if (!(current is null))//EVALUO SI YA EXISTE
                {

                    if (!String.IsNullOrWhiteSpace(obj.Direccion))
                    {
                        current.Direccion = obj.Direccion;
                    }

                    if (!String.IsNullOrWhiteSpace(obj.RazonSocial))
                    {
                        current.RazonSocial = obj.RazonSocial;
                    }


                    if (String.IsNullOrWhiteSpace(current.DniRl))
                    {
                        current.DniRl = obj.DniRl;
                    }


                    //Si tiene DNI, debe tener persona, sino tiene, la creo
                    if (!String.IsNullOrWhiteSpace(obj.DniRl))
                    {
                        if (obj.DniRlNavigation is null)
                        {
                            if (current.DniRlNavigation is null)
                                current.DniRlNavigation = new Persona() { Dni = current.DniRl, Tipo = "cli" };
                        }
                        else
                        {
                            if(current.DniRlNavigation is null)
                            {
                                current.DniRlNavigation = obj.DniRlNavigation;
                            }
                            else
                            {
                                //MAPEO
                                if (!String.IsNullOrWhiteSpace(obj.DniRlNavigation.Nombre))
                                {
                                    current.DniRlNavigation.Nombre = obj.DniRlNavigation.Nombre;
                                }

                                if (!String.IsNullOrWhiteSpace(obj.DniRlNavigation.Apellido))
                                {
                                    current.DniRlNavigation.Apellido = obj.DniRlNavigation.Apellido;
                                }

                                if (!String.IsNullOrWhiteSpace(obj.DniRlNavigation.FechaNac.ToString()))
                                {
                                    current.DniRlNavigation.FechaNac = obj.DniRlNavigation.FechaNac;
                                }

                                if (!String.IsNullOrWhiteSpace(obj.DniRlNavigation.Nacionalidad))
                                {
                                    current.DniRlNavigation.Nacionalidad = obj.DniRlNavigation.Nacionalidad;
                                }

                            }
                        }
                    }

                    //Lo agrego a la lista en memoria, luego a la DB
                    return clienteRepository.Update(current);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 1. Verifico que haya Ruc
        /// 2. Verifico que exista ese Ruc en la lista
        /// 3. Si existe, lo elimino de la lista in Memory
        /// 4. lo mando el ID a la DB para que lo borre
        /// </summary>
        /// <param name="Ruc"></param>
        /// <returns></returns>
        public bool Eliminar(string Ruc)
        {
            if (!String.IsNullOrWhiteSpace(Ruc))//EVALUO CAMPOS OBLIGATORIOS
            {
                if (listaClientes.Exists(x => x.Ruc == Ruc))
                {
                    var result = clienteRepository.Delete(Ruc);

                    if (result) listaClientes.Remove(listaClientes.FirstOrDefault(x => x.Ruc == Ruc));
               
                    return result;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }

}
