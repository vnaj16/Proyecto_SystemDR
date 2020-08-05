using Datos.Helpers;
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

        public ClienteBO(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }

        public List<Cliente> GetAll() //READ
        {
            return clienteRepository.GetAll().ToList();
        }

        public bool Registrar(Cliente obj)
        {
            bool RLExists = false;

            if (!String.IsNullOrWhiteSpace(obj.Ruc))//EVALUO CAMPOS OBLIGATORIOS
            {
                if (!clienteRepository.Exists(obj.Ruc))//EVALUO SI YA EXISTE
                {
                    //Si tiene DNI, debe tener persona
                    if (!String.IsNullOrWhiteSpace(obj.DniRl))
                    {
                        if (obj.DniRlNavigation is null)//si no tiene, la creo
                        {
                            obj.DniRlNavigation = new Persona() { Dni = obj.DniRl, Tipo = "cli" };
                        }
                        else
                        {
                            obj.DniRlNavigation.Dni = obj.DniRl;
                            obj.DniRlNavigation.Tipo = "cli";
                        }

                        PersonaRepository personaRepository = new PersonaRepository();
                        if (!personaRepository.Exists(obj.DniRlNavigation.Dni))
                        {
                            personaRepository.Insert(obj.DniRlNavigation);
                        }
                        else
                        {
                            RLExists = true;
                            obj.DniRl = null;
                            obj.DniRlNavigation = null;
                        }
                    }


                    //Primero verifico si se agrego de manera correcta a la DB
                    var Result = clienteRepository.Insert(obj);

                    if (RLExists == false)
                    {
                        return Result;
                    }
                    else
                    {
                        throw new Exception("Se registro al cliente de manera correcta, pero no se pudo registrar al Representante Legal ya que ya existe en la Base de Datos y está relacionado con otra empresa");
                    }
                }
                else
                {
                    throw new Exception(ExceptionMessageManager.ExceptionMessageCliente.AlreadyExists(obj.Ruc));
                }
            }
            else
            {
                throw new Exception(ExceptionMessageManager.ExceptionMessageCliente.KeyIsNull());
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
                if (clienteRepository.Exists(obj.Ruc))
                {
                    PersonaRepository personaRepository = new PersonaRepository();
                    #region PERSONAREPOSITORY
                    //Si tiene DNI, debe tener persona
                    if (!String.IsNullOrWhiteSpace(obj.DniRl))
                    {
                        if (obj.DniRlNavigation is null)//Si no tiene, la creo
                        {
                            obj.DniRlNavigation = new Persona() { Dni = obj.DniRl, Tipo = "cli" };
                        }
                        else
                        {
                            obj.DniRlNavigation.Dni = obj.DniRl;
                            obj.DniRlNavigation.Tipo = "cli";
                        }

                        if (clienteRepository.HasRepresent(obj.Ruc, out string Dni) && Dni == obj.DniRlNavigation.Dni)
                        {
                            personaRepository.Update(obj.DniRlNavigation);
                        }
                        else
                        {
                            if (personaRepository.Exists(obj.DniRlNavigation.Dni))
                            {
                                obj.DniRl = null;
                                obj.DniRlNavigation = null;
                                throw new Exception("Este representante legal ya se encuentra registrado");
                            }
                            else
                            {
                                personaRepository.Insert(obj.DniRlNavigation);
                            }
                        }
                        //Si este cliente tiene representante legal, actualiza, si no tiene, verificar que no exista ya ese cliente para registrarlo
                    }
                    #endregion
                    return clienteRepository.Update(obj);
                }
                else
                {
                    throw new Exception(ExceptionMessageManager.ExceptionMessageCliente.DoesNotExist(obj.Ruc));
                }
            }
            else
            {
                throw new Exception(ExceptionMessageManager.ExceptionMessageCliente.KeyIsNull());
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
                if (clienteRepository.Exists(Ruc))
                {
                    bool resultp = true;
                    PersonaRepository personaRepository = new PersonaRepository();
                    if (clienteRepository.HasRepresent(Ruc, out string DniRepresent))
                    {
                        resultp = personaRepository.Delete(DniRepresent);
                    }
                    var result = clienteRepository.Delete(Ruc);

                    //if (result) listaClientes.Remove(listaClientes.FirstOrDefault(x => x.Ruc == Ruc));

                    return result && resultp;
                }
                else
                {
                    throw new Exception(ExceptionMessageManager.ExceptionMessageCliente.DoesNotExist(obj.Ruc));
                }
            }
            else
            {
                throw new Exception(ExceptionMessageManager.ExceptionMessageCliente.KeyIsNull());
            }
        }
    }

}
