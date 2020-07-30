using Datos.Interfaces;
using Entidades;
using Negocio.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Business_Objects
{
    public class TelefonoBO
    {
        private ITelefonoRepository telefonoRepository;

        public TelefonoBO(ITelefonoRepository telefonoRepository)
        {
            this.telefonoRepository = telefonoRepository;
        }

        public bool Registrar(Telefono obj)
        {
            if (!String.IsNullOrWhiteSpace(obj.Numero) && !String.IsNullOrWhiteSpace(obj.Dni))
            {
                if (obj.DniNavigation is null)
                {
                    throw new Exception("No hay una persona asignada a este telefono");
                }

                var persona = obj.DniNavigation;

                if (!telefonoRepository.Exists(obj.Numero))
                {
                    var result = telefonoRepository.Insert(obj);

                    if(persona.Telefono is null)
                    {
                        persona.Telefono = new List<Telefono>();
                    }

                    if (result && !(persona.Telefono.ToList().Exists(X=>X.Numero == obj.Numero))) {
                        persona.Telefono.Add(obj);
                    }

                    return result;
                }
                else
                {
                    throw new Exception("Ya existe ese telefono");
                }
            }
            else
            {
                throw new Exception("Asegurese que el campo Numero no este vacio.\n Si el problema persiste, cierre y abra la ventana de nuevo");
            }
        }

        public bool Eliminar(Telefono obj)
        {
            if (!String.IsNullOrWhiteSpace(obj.Numero))
            {
                if (obj.DniNavigation is null)
                {
                    throw new Exception("No hay una persona asignada a este telefono");
                }

                var persona = obj.DniNavigation;


                if (telefonoRepository.Exists(obj.Numero))
                {
                    var result = telefonoRepository.Delete(obj.Numero);

                    if (result) persona.Telefono.Remove(persona.Telefono.FirstOrDefault(x => x.Numero == obj.Numero));

                    return result;
                }
                else
                {
                    throw new Exception("Ya no existe ese telefono en la Base de Datos");
                }
            }
            else
            {
                throw new Exception("Asegurese que el campo Numero no este vacio.\n Si el problema persiste, cierre y abra la ventana de nuevo");
            }
        }

        public bool Actualizar(Telefono obj)
        {
            if (!String.IsNullOrWhiteSpace(obj.Numero) && !String.IsNullOrWhiteSpace(obj.Dni) && !String.IsNullOrWhiteSpace(obj.NumeroAntiguo))
            {
                if (obj.DniNavigation is null)
                {
                    throw new Exception("No hay una persona asignada a este telefono");
                }

                var persona = obj.DniNavigation;

                /*//Primero jalo a ver si es cliente, luego si es proveedor, o sino por ultimo si es conductor
                var persona = TransporteDR.ClienteBO.GetAll().FirstOrDefault(x => x.DniRl == obj.Dni).DniRlNavigation;

                if (persona is null)
                {
                    //persona = TransporteDR.ClienteBO.GetAll().Find(x => x.DNI == obj.DNI).Persona;
                }

                if (persona is null)
                {
                    //persona = TransporteDR.ClienteBO.GetAll().Find(x => x.DNI == obj.DNI).Persona;
                }*/

                if (telefonoRepository.Exists(obj.NumeroAntiguo) && !telefonoRepository.Exists(obj.Numero))
                {
                    var result = telefonoRepository.Update(obj);

                    if (result)
                    {
                        persona.Telefono.FirstOrDefault(x => x.Numero == obj.Numero).Numero = obj.Numero;
                    }


                    return result;
                }
                else if (telefonoRepository.Exists(obj.Numero))
                {
                    throw new Exception("Ya existe ese telefono en la Base de Datos");
                }
                else
                {
                    throw new Exception("Ya no existe ese telefono en la Base de Datos");
                }
            }
            else
            {
                throw new Exception("Asegurese que el campo Numero no este vacio.\n Si el problema persiste, cierre y abra la ventana de nuevo");
            }
        }

    }
}
