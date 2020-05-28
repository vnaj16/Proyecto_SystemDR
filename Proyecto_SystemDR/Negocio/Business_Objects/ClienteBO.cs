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

        public bool Actualizar(Cliente obj)//EN TESTEO
        {
            if (!String.IsNullOrWhiteSpace(obj.RUC))//EVALUO CAMPOS OBLIGATORIOS
            {
                Cliente current = listaClientes.Find(x => x.RUC == obj.RUC);
                if (!(current is null))//EVALUO SI YA EXISTE
                {
                    


                    //Si tiene DNI, debe tener persona, sino tiene, la creo
                    if (!String.IsNullOrWhiteSpace(obj.DNI))
                    {
                        if (obj.Persona is null)
                        {
                            obj.Persona = new Persona() { DNI = obj.DNI };
                        }
                        else
                        {
                            obj.Persona.DNI = obj.DNI;
                        }
                    }

                    //Lo agrego a la lista en memoria, luego a la DB
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

    }

}
