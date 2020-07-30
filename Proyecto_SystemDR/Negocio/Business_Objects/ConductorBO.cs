using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Interfaces;
using Datos.Repositories;
using Entidades;
using Negocio.Core;

namespace Negocio.Business_Objects
{
    public class ConductorBO
    {
        private IConductorRepository conductorRepository;
        //private List<Conductor> listaConductores;

        public ConductorBO(IConductorRepository conductorRepository)
        {
            this.conductorRepository = conductorRepository;
            //GetAll();
        }

        public List<Conductor> GetAll()
        {
            /*if (listaConductores != null && listaConductores.Count != 0)
            {
                return listaConductores;
            }
            else
            {
                listaConductores = conductorRepository.GetAll().ToList();

                return listaConductores;
            }*/

            return conductorRepository.GetAll().ToList();
        }

        public bool Registrar(Conductor obj)
        {
            if (!String.IsNullOrWhiteSpace(obj.Dni))
            {
                if (!conductorRepository.Exists(obj.Dni))
                {
                    //Si tiene DNI, debe tener persona
                    if (!String.IsNullOrWhiteSpace(obj.Dni))
                    {
                        if (obj.DniNavigation is null)//si no tiene, la creo
                        {
                            obj.DniNavigation = new Persona() { Dni = obj.Dni, Tipo = "con" };
                        }
                        else
                        {
                            obj.DniNavigation.Dni = obj.Dni;
                            obj.DniNavigation.Tipo = "con";
                        }

                        PersonaRepository personaRepository = new PersonaRepository();
                        if (!personaRepository.Exists(obj.DniNavigation.Dni))
                        {
                            personaRepository.Insert(obj.DniNavigation);
                        }
                    }

                    //Primero verifico si se agrego de manera correcta a la DB, luego lo agrego a la Lista in Memory
                    var Result = conductorRepository.Insert(obj);

                    return Result;
                }
                else
                {
                    throw new Exception("Ya existe un conductor con ese DNI");
                }
            }
            else
            {
                throw new Exception("El DNI esta vacio");
            }
        }

        public bool Actualizar(Conductor obj)
        {
            if (!String.IsNullOrWhiteSpace(obj.Dni))
            {
                if (conductorRepository.Exists(obj.Dni))
                {
                    PersonaRepository personaRepository = new PersonaRepository();
                    #region PERSONAREPOSITORY
                    //Si tiene DNI, debe tener persona
                    if (!String.IsNullOrWhiteSpace(obj.Dni))
                    {
                        if (obj.DniNavigation is null)//Si no tiene, la creo
                        {
                            obj.DniNavigation = new Persona() { Dni = obj.Dni, Tipo = "cli" };
                        }
                        else
                        {
                            obj.DniNavigation.Dni = obj.Dni;
                            obj.DniNavigation.Tipo = "cli";
                        }


                        if (personaRepository.Exists(obj.DniNavigation.Dni))
                        {
                            personaRepository.Update(obj.DniNavigation);
                        }
                        else
                        {
                            personaRepository.Insert(obj.DniNavigation);
                        }

                        //Si este cliente tiene representante legal, actualiza, si no tiene, verificar que no exista ya ese cliente para registrarlo
                    }
                    #endregion
                    return conductorRepository.Update(obj);
                }
                else
                {
                    throw new Exception("No existe un conductor con ese DNI");
                }
                /*Conductor current = listaConductores.FirstOrDefault(x => x.Dni == obj.Dni);

                if(!(current is null))
                {

                    if (!String.IsNullOrWhiteSpace(obj.Brevete)) current.Brevete = obj.Brevete;

                    if (!String.IsNullOrWhiteSpace(obj.Direccion)) current.Direccion = obj.Direccion;

                    if (!String.IsNullOrWhiteSpace(obj.FechaInicio.ToString())) current.FechaInicio = obj.FechaInicio;

                    if (!String.IsNullOrWhiteSpace(obj.GradoInstruccion)) current.GradoInstruccion = obj.GradoInstruccion;

                    if (!String.IsNullOrWhiteSpace(obj.LugarNac)) current.LugarNac = obj.LugarNac;

                    if (!String.IsNullOrWhiteSpace(obj.Personalidad)) current.Personalidad = obj.Personalidad;

                    /*
                    if(obj.DniNavigation is null)
                    {
                        if (current.DniNavigation is null)
                            current.DniNavigation = new Persona() { Dni = current.Dni, Tipo = "con" };
                    }
                    else
                    {
                        if (current.DniNavigation is null)
                        {
                            current.DniNavigation = obj.DniNavigation;
                        }
                        else
                        {
                            //MAPEO
                            if (!String.IsNullOrWhiteSpace(obj.DniNavigation.Nombre))
                            {
                                current.DniNavigation.Nombre = obj.DniNavigation.Nombre;
                            }

                            if (!String.IsNullOrWhiteSpace(obj.DniNavigation.Apellido))
                            {
                                current.DniNavigation.Apellido = obj.DniNavigation.Apellido;
                            }

                            if (!String.IsNullOrWhiteSpace(obj.DniNavigation.FechaNac.ToString()))
                            {
                                current.DniNavigation.FechaNac = obj.DniNavigation.FechaNac;
                            }

                            if (!String.IsNullOrWhiteSpace(obj.DniNavigation.Nacionalidad))
                            {
                                current.DniNavigation.Nacionalidad = obj.DniNavigation.Nacionalidad;
                            }

                        }
                    }
                    


                    return conductorRepository.Update(current);
                }
                else
                {
                    return false;
                }*/
            }
            else
            {
                throw new Exception("El DNI esta vacio");
            }
        }

        public bool Eliminar(string DNI)
        {
            if (!String.IsNullOrWhiteSpace(DNI))
            {
                if (conductorRepository.Exists(DNI))
                {
                    var result = conductorRepository.Delete(DNI);
                    return result;
                }
                else
                {
                    throw new Exception("No existe un conductor con ese DNI");
                }
            }
            else
            {
                throw new Exception("El DNI esta vacio");
            }
        }



    }
}
