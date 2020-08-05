using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.Helpers;
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
                    throw new Exception(ExceptionMessageManager.ExceptionMessageConductor.AlreadyExists(obj.Dni));
                }
            }
            else
            {
                throw new Exception(ExceptionMessageManager.ExceptionMessageConductor.KeyIsNull());
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
                    throw new Exception(ExceptionMessageManager.ExceptionMessageConductor.DoesNotExist(obj.Dni));
                }
            }
            else
            {
                throw new Exception(ExceptionMessageManager.ExceptionMessageConductor.KeyIsNull());
            }
        }

        public bool Eliminar(string DNI)
        {
            if (!String.IsNullOrWhiteSpace(DNI))
            {
                if (conductorRepository.Exists(DNI))
                {
                    var result = conductorRepository.Delete(DNI);

                    bool resultp = true;
                    PersonaRepository personaRepository = new PersonaRepository();

                    resultp = personaRepository.Delete(DNI);

                    return result && resultp;
                }
                else
                {
                    throw new Exception(ExceptionMessageManager.ExceptionMessageConductor.DoesNotExist(DNI));
                }
            }
            else
            {
                throw new Exception(ExceptionMessageManager.ExceptionMessageConductor.KeyIsNull());
            }
        }



    }
}
