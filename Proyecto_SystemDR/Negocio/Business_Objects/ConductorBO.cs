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
            if (!String.IsNullOrWhiteSpace(obj.DNI))
            {
                if(!listaConductores.Exists(x=>x.DNI == obj.DNI))
                {
                    if (obj.Persona is null)
                    {
                        obj.Persona = new Persona() { DNI = obj.DNI, Tipo = "con" };
                    }
                    else
                    {
                        obj.Persona.DNI = obj.DNI;
                        obj.Persona.Tipo = "con";
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
            if (!String.IsNullOrWhiteSpace(obj.DNI))
            {
                Conductor current = listaConductores.FirstOrDefault(x => x.DNI == obj.DNI);

                if(!(current is null))
                {

                    if (!String.IsNullOrWhiteSpace(obj.Brevete)) current.Brevete = obj.Brevete;

                    if (!String.IsNullOrWhiteSpace(obj.Direccion)) current.Direccion = obj.Direccion;

                    if (!String.IsNullOrWhiteSpace(obj.Fecha_Inicio.ToString())) current.Fecha_Inicio = obj.Fecha_Inicio;

                    if (!String.IsNullOrWhiteSpace(obj.Grado_Instruccion)) current.Grado_Instruccion = obj.Grado_Instruccion;

                    if (!String.IsNullOrWhiteSpace(obj.Lugar_Nac)) current.Lugar_Nac = obj.Lugar_Nac;

                    if (!String.IsNullOrWhiteSpace(obj.Personalidad)) current.Personalidad = obj.Personalidad;

                    if(obj.Persona is null)
                    {
                        if (current.Persona is null)
                            current.Persona = new Persona() { DNI = current.DNI, Tipo = "con" };
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
                if(listaConductores.Exists(x=>x.DNI == DNI))
                {
                    var result = conductorRepository.Delete(DNI);

                    if (result) listaConductores.Remove(listaConductores.FirstOrDefault(x => x.DNI == DNI));

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
