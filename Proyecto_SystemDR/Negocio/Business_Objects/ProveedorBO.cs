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
            if (!String.IsNullOrWhiteSpace(obj.RUC))//EVALUO CAMPOS OBLIGATORIOS
            {
                if (!listaProveedores.Exists(x => x.RUC == obj.RUC))//EVALUO SI YA EXISTE
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
            if (!String.IsNullOrWhiteSpace(obj.RUC))//EVALUO CAMPOS OBLIGATORIOS
            {
                Proveedor current = listaProveedores.FirstOrDefault(x => x.RUC == obj.RUC);
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
                                current.Persona = new Persona() { DNI = current.DNI, Tipo = "pro" };
                        }
                        else
                        {
                            if (current.Persona is null)
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
                if (listaProveedores.Exists(x => x.RUC == RUC))
                {
                    try
                    {
                        var result = proveedorRepository.Delete(RUC);

                        if (result) listaProveedores.Remove(listaProveedores.FirstOrDefault(x => x.RUC == RUC));

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
