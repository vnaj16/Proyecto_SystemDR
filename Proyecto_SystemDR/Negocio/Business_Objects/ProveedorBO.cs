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
        private List<Proveedor> listaProveedores;

        public ProveedorBO(IProveedorRepository proveedorRepository)
        {
            this.proveedorRepository = proveedorRepository;
            GetAll();
        }

        public List<Proveedor> GetAll() //READ
        {
            if (listaProveedores != null && listaProveedores.Count != 0)
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
                }*/

                return listaProveedores;
            }
        }

        public bool Registrar(Proveedor obj)
        {
            if (!String.IsNullOrWhiteSpace(obj.Ruc))//EVALUO CAMPOS OBLIGATORIOS
            {
                if (!listaProveedores.Exists(x => x.Ruc == obj.Ruc))//EVALUO SI YA EXISTE
                {
                    //Si tiene DNI, debe tener persona, sino tiene, la creo
                    if (!String.IsNullOrWhiteSpace(obj.DniRl))
                    {
                        if (obj.DniRlNavigation is null)
                        {
                            obj.DniRlNavigation = new Persona() { Dni = obj.DniRl };
                        }
                        else
                        {
                            obj.DniRlNavigation.Dni = obj.DniRl;
                        }
                    }

                    try
                    {
                        //Primero verifico si se agrego de manera correcta a la DB, luego lo agrego a la Lista in Memory
                        var Result = proveedorRepository.Insert(obj);

                        if (Result) listaProveedores.Add(obj);

                        return Result;
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
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
                Proveedor current = listaProveedores.FirstOrDefault(x => x.Ruc == obj.Ruc);
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
                                current.DniRlNavigation = new Persona() { Dni = current.DniRl, Tipo = "pro" };
                        }
                        else
                        {
                            if (current.DniRlNavigation is null)
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

                    try
                    {
                        //Lo agrego a la lista en memoria, luego a la DB
                        return proveedorRepository.Update(current);
                    }
                    catch (Exception)
                    {

                        throw;
                    }

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

        public bool Eliminar(string RUC)
        {
            if (!String.IsNullOrWhiteSpace(RUC))//EVALUO CAMPOS OBLIGATORIOS
            {
                if (listaProveedores.Exists(x => x.Ruc == RUC))
                {
                    try
                    {
                        var result = proveedorRepository.Delete(RUC);

                        if (result) listaProveedores.Remove(listaProveedores.FirstOrDefault(x => x.Ruc == RUC));

                        return result;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
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
    }

}
