using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Interfaces;
using Datos.Repositories;
using Entidades;

namespace Negocio.Business_Objects
{
    public class ProveedorBO
    {
        private IProveedorRepository proveedorRepository;
        //private List<Proveedor> listaProveedores;

        public ProveedorBO(IProveedorRepository proveedorRepository)
        {
            this.proveedorRepository = proveedorRepository;
            //GetAll();
        }

        public List<Proveedor> GetAll() //READ
        {
            /*if (listaProveedores != null && listaProveedores.Count != 0)
            {
                return listaProveedores;
            }
            else
            {
                listaProveedores = proveedorRepository.GetAll().ToList();

                /*foreach (Cliente x in listaDB)
                {
                    ClienteDTO obj = new ClienteDTO();
                    MyMapper.Map(x, obj);
                    listaClienteDTO.Add(obj);
                }

                return listaProveedores;
            }*/

            return proveedorRepository.GetAll().ToList();
        }

        public bool Registrar(Proveedor obj)
        {
            if (!String.IsNullOrWhiteSpace(obj.Ruc))//EVALUO CAMPOS OBLIGATORIOS
            {
                if (!proveedorRepository.Exists(obj.Ruc))//EVALUO SI YA EXISTE
                {
                    //Si tiene DNI, debe tener persona
                    if (!String.IsNullOrWhiteSpace(obj.DniRl))
                    {
                        if (obj.DniRlNavigation is null)
                        {
                            obj.DniRlNavigation = new Persona() { Dni = obj.DniRl, Tipo = "pro" };
                        }
                        else
                        {
                            obj.DniRlNavigation.Dni = obj.DniRl;
                            obj.DniRlNavigation.Tipo = "pro";
                        }

                        PersonaRepository personaRepository = new PersonaRepository();
                        if (!personaRepository.Exists(obj.DniRlNavigation.Dni))
                        {
                            personaRepository.Insert(obj.DniRlNavigation);
                        }
                    }

                    //Primero verifico si se agrego de manera correcta a la DB, luego lo agrego a la Lista in Memory
                    var Result = proveedorRepository.Insert(obj);

                    return Result;

                }
                else
                {
                    throw new Exception("Ese Proveedor ya existe");
                }
            }
            else
            {
                throw new Exception("RUC vacio");
            }
        }

        public bool Actualizar(Proveedor obj)
        {
            if (!String.IsNullOrWhiteSpace(obj.Ruc))//EVALUO CAMPOS OBLIGATORIOS
            {
                if (proveedorRepository.Exists(obj.Ruc))
                {
                    PersonaRepository personaRepository = new PersonaRepository();
                    #region PERSONAREPOSITORY
                    //Si tiene DNI, debe tener persona
                    if (!String.IsNullOrWhiteSpace(obj.DniRl))
                    {
                        if (obj.DniRlNavigation is null)//Si no tiene, la creo
                        {
                            obj.DniRlNavigation = new Persona() { Dni = obj.DniRl, Tipo = "pro" };
                        }
                        else
                        {
                            obj.DniRlNavigation.Dni = obj.DniRl;
                            obj.DniRlNavigation.Tipo = "pro";
                        }

                        if (proveedorRepository.HasRepresent(obj.Ruc, out string Dni) && Dni == obj.DniRlNavigation.Dni)
                        {
                            personaRepository.Update(obj.DniRlNavigation);
                        }
                        else
                        {
                            if (personaRepository.Exists(obj.DniRlNavigation.Dni))
                            {
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

                    //Lo agrego a la lista en memoria, luego a la DB
                    return proveedorRepository.Update(obj);
                }
                else
                {
                    throw new Exception("Ese Proveedor no existe");
                }
            }
            else
            {
                throw new Exception("RUC vacio");
            }
        }

        public bool Eliminar(string Ruc)
        {
            if (!String.IsNullOrWhiteSpace(Ruc))//EVALUO CAMPOS OBLIGATORIOS
            {
                if (proveedorRepository.Exists(Ruc))
                {
                    bool resultp = true;
                    PersonaRepository personaRepository = new PersonaRepository();
                    if (proveedorRepository.HasRepresent(Ruc, out string DniRepresent))
                    {
                        resultp = personaRepository.Delete(DniRepresent);
                    }
                    var result = proveedorRepository.Delete(Ruc);

                    //if (result) listaClientes.Remove(listaClientes.FirstOrDefault(x => x.Ruc == Ruc));

                    return result && resultp;
                }
                else
                {
                    throw new Exception("No existe un proveedor con ese RUC");
                }
            }
            else
            {
                throw new Exception("El RUC esta vacio");
            }
        }
    }

}
