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
                var persona = obj.Persona;
                //Persona persona;
                ////Primero jalo a ver si es cliente, luego si es proveedor, o sino por ultimo si es conductor
                //if (TransporteDR.ClienteBO.GetAll().Exists(x => x.DNI == obj.DNI))
                //{

                //}
                //else if(TransporteDR.ProveedorBO.GetAll().Exists(x => x.DNI == obj.DNI))
                //{
                //    persona = TransporteDR.ProveedorBO.GetAll().Find(x => x.DNI == obj.DNI).Persona;
                //}
                //else if(TransporteDR.ConductorBO.GetAll().Exists(x => x.DNI == obj.DNI)){
                //    persona = TransporteDR.ConductorBO.GetAll().Find(x => x.DNI == obj.DNI).Persona;
                //}
                //else
                //{
                //    throw new Exception("No existe esa persona");
                //}

                if (persona.Telefono.ToList().Exists(x => x.Numero == obj.Numero))
                {
                    return false;
                }

                var result = telefonoRepository.Insert(obj);

                if (result) persona.Telefono?.Add(obj);

                return result;
            }
            else
            {
                return false;
            }
        }

        public bool Eliminar(Telefono obj)
        {
            if (!String.IsNullOrWhiteSpace(obj.Numero))
            {
                //Primero jalo a ver si es cliente, luego si es proveedor, o sino por ultimo si es conductor
                var persona = obj.Persona;


                if (persona.Telefono.ToList().Exists(x => x.Numero == obj.Numero))
                {
                    //int y = persona.Telefono.ToList().RemoveAll(x => x.Numero == Numero);

                    var result = telefonoRepository.Delete(obj.Numero);

                    if (result) persona.Telefono.Remove(persona.Telefono.FirstOrDefault(x => x.Numero == obj.Numero));

                    return result;
                }


                return false;
            }
            else
            {
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
