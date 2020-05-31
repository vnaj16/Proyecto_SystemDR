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
            if (!String.IsNullOrWhiteSpace(obj.RUC))//EVALUO CAMPOS OBLIGATORIOS
            {
                if (!listaClientes.Exists(x => x.RUC == obj.RUC))//EVALUO SI YA EXISTE
                {
                    //Si tiene DNI, debe tener persona, sino tiene, la creo
                    if (!String.IsNullOrWhiteSpace(obj.DNI))
                    {
                        if(obj.Persona is null)
                        {
                            obj.Persona = new Persona() { DNI = obj.DNI };
                        }
                        else
                        {
                            obj.Persona.DNI = obj.DNI;
                        }
                    }

                    //Lo agrego a la lista en memoria, luego a la DB
                    listaClientes.Add(obj);
                    return clienteRepository.Insert(obj);
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
        /// 2. Obtengo el cliente con ese RUC
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
        public bool Actualizar(Cliente obj)//EN TESTEO
        {
            if (!String.IsNullOrWhiteSpace(obj.RUC))//EVALUO CAMPOS OBLIGATORIOS
            {
                Cliente current = listaClientes.Find(x => x.RUC == obj.RUC);
                if (!(current is null))//EVALUO SI YA EXISTE
                {

                    if (!String.IsNullOrWhiteSpace(obj.Direccion))
                    {
                        current.Direccion = obj.Direccion;
                    }

                    if (!String.IsNullOrWhiteSpace(obj.Razon_Social))
                    {
                        current.Razon_Social = obj.Razon_Social;
                    }


                    if (String.IsNullOrWhiteSpace(current.DNI))
                    {
                        current.DNI = obj.DNI;
                    }


                    //Si tiene DNI, debe tener persona, sino tiene, la creo
                    if (!String.IsNullOrWhiteSpace(obj.DNI))
                    {
                        if (obj.Persona is null)
                        {
                            if (current.Persona is null)
                                current.Persona = new Persona() { DNI = current.DNI, Tipo = "cli" };
                        }
                        else
                        {
                            if(current.Persona is null)
                            {
                                current.Persona = obj.Persona;
                            }
                            else
                            {
                                //MAPEO
                                if (!String.IsNullOrWhiteSpace(obj.Persona.Nombre))
                                {
                                    current.Persona.Nombre = obj.Persona.Nombre;
                                }

                                if (!String.IsNullOrWhiteSpace(obj.Persona.Apellido))
                                {
                                    current.Persona.Apellido = obj.Persona.Apellido;
                                }

                                if (!String.IsNullOrWhiteSpace(obj.Persona.Fecha_Nac.ToString()))
                                {
                                    current.Persona.Fecha_Nac = obj.Persona.Fecha_Nac;
                                }

                                if (!String.IsNullOrWhiteSpace(obj.Persona.Nacionalidad))
                                {
                                    current.Persona.Nacionalidad = obj.Persona.Nacionalidad;
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
        /// 1. Verifico que haya RUC
        /// 2. Verifico que exista ese RUC en la lista
        /// 3. Si existe, lo elimino de la lista in Memory
        /// 4. lo mando el ID a la DB para que lo borre
        /// </summary>
        /// <param name="RUC"></param>
        /// <returns></returns>
        public bool Eliminar(string RUC)
        {
            if (!String.IsNullOrWhiteSpace(RUC))//EVALUO CAMPOS OBLIGATORIOS
            {
                if (listaClientes.Exists(x => x.RUC == RUC))
                {
                    listaClientes.RemoveAll(x => x.RUC == RUC);

                    return clienteRepository.Delete(RUC);
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
