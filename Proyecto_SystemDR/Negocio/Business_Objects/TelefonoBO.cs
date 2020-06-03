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
            if (!String.IsNullOrWhiteSpace(obj.Numero) && !String.IsNullOrWhiteSpace(obj.DNI))
            {
                //Primero jalo a ver si es cliente, luego si es proveedor, o sino por ultimo si es conductor
                var persona = TransporteDR.ClienteBO.GetAll().FirstOrDefault(x => x.DNI == obj.DNI).Persona;

                if(persona is null)
                {
                    //persona = TransporteDR.ClienteBO.GetAll().Find(x => x.DNI == obj.DNI).Persona;
                }

                if(persona is null)
                {
                    //persona = TransporteDR.ClienteBO.GetAll().Find(x => x.DNI == obj.DNI).Persona;
                }

                if (persona.Telefono.ToList().Exists(x => x.Numero == obj.Numero))
                {
                    return false;
                }

                persona.Telefono?.Add(obj);

                return telefonoRepository.Insert(obj);
            }
            else
            {
                return false;
            }
        }

        public bool Eliminar(string Numero, string DNI)
        {
            if (!String.IsNullOrWhiteSpace(Numero))
            {
                //Primero jalo a ver si es cliente, luego si es proveedor, o sino por ultimo si es conductor
                var persona = TransporteDR.ClienteBO.GetAll().FirstOrDefault(x => x.DNI ==DNI).Persona;

                if (persona is null)
                {
                    //persona = TransporteDR.ClienteBO.GetAll().Find(x => x.DNI == obj.DNI).Persona;
                }

                if (persona is null)
                {
                    //persona = TransporteDR.ClienteBO.GetAll().Find(x => x.DNI == obj.DNI).Persona;
                }


                if (persona.Telefono.ToList().Exists(x => x.Numero == Numero))
                {
                    //int y = persona.Telefono.ToList().RemoveAll(x => x.Numero == Numero);

                    bool p = persona.Telefono.Remove(persona.Telefono.FirstOrDefault(x => x.Numero == Numero));

                    return telefonoRepository.Delete(Numero);
                }


                return false;
            }
            else{
                return false;
            }
        }
    
        public bool Actualizar(Telefono obj)
        {
            if (!String.IsNullOrWhiteSpace(obj.Numero) && !String.IsNullOrWhiteSpace(obj.DNI) && !String.IsNullOrWhiteSpace(obj.NumeroAntiguo))
            {
                //Primero jalo a ver si es cliente, luego si es proveedor, o sino por ultimo si es conductor
                var persona = TransporteDR.ClienteBO.GetAll().FirstOrDefault(x => x.DNI == obj.DNI).Persona;

                if (persona is null)
                {
                    //persona = TransporteDR.ClienteBO.GetAll().Find(x => x.DNI == obj.DNI).Persona;
                }

                if (persona is null)
                {
                    //persona = TransporteDR.ClienteBO.GetAll().Find(x => x.DNI == obj.DNI).Persona;
                }

                if (persona.Telefono.ToList().Exists(x => x.Numero == obj.NumeroAntiguo))
                {
                    persona.Telefono.FirstOrDefault(x => x.Numero == obj.NumeroAntiguo).Numero = obj.Numero;

                    return telefonoRepository.Update(obj);
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
