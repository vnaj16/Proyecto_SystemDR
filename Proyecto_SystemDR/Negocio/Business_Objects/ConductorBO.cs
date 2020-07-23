using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Interfaces;
using Entidades;
using Negocio.Core;

namespace Negocio.Business_Objects
{
    public class ConductorBO
    {
        private IConductorRepository conductorRepository;
        private List<Conductor> listaConductores;

        public ConductorBO(IConductorRepository conductorRepository)
        {
            this.conductorRepository = conductorRepository;
            GetAll();
        }

        public List<Conductor> GetAll()
        {
            if (listaConductores != null && listaConductores.Count != 0)
            {
                return listaConductores;
            }
            else
            {
                listaConductores = conductorRepository.GetAll().ToList();

                return listaConductores;
            }
        }

        public bool Registrar(Conductor obj)
        {
            if (!String.IsNullOrWhiteSpace(obj.Dni))
            {
                if(!listaConductores.Exists(x=>x.Dni == obj.Dni))
                {
                    if (obj.DniNavigation is null)
                    {
                        obj.DniNavigation = new Persona() { Dni = obj.Dni, Tipo = "con" };
                    }
                    else
                    {
                        obj.DniNavigation.Dni = obj.Dni;
                        obj.DniNavigation.Tipo = "con";
                    }

                    //Primero verifico si se agrego de manera correcta a la DB, luego lo agrego a la Lista in Memory
                    var Result = conductorRepository.Insert(obj);

                    if (Result) listaConductores.Add(obj);

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

        public bool Actualizar(Conductor obj)
        {
            if (!String.IsNullOrWhiteSpace(obj.Dni))
            {
                Conductor current = listaConductores.FirstOrDefault(x => x.Dni == obj.Dni);

                if(!(current is null))
                {

                    if (!String.IsNullOrWhiteSpace(obj.Brevete)) current.Brevete = obj.Brevete;

                    if (!String.IsNullOrWhiteSpace(obj.Direccion)) current.Direccion = obj.Direccion;

                    if (!String.IsNullOrWhiteSpace(obj.FechaInicio.ToString())) current.FechaInicio = obj.FechaInicio;

                    if (!String.IsNullOrWhiteSpace(obj.GradoInstruccion)) current.GradoInstruccion = obj.GradoInstruccion;

                    if (!String.IsNullOrWhiteSpace(obj.LugarNac)) current.LugarNac = obj.LugarNac;

                    if (!String.IsNullOrWhiteSpace(obj.Personalidad)) current.Personalidad = obj.Personalidad;

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
                }
            }
            else
            {
                return false;
            }
        }

        public bool Eliminar(string DNI)
        {
            if (!String.IsNullOrWhiteSpace(DNI))
            {
                if(listaConductores.Exists(x=>x.Dni == DNI))
                {
                    var result = conductorRepository.Delete(DNI);

                    if (result) listaConductores.Remove(listaConductores.FirstOrDefault(x => x.Dni == DNI));

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
