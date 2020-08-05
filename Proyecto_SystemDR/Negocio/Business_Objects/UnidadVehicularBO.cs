using Datos.Helpers;
using Datos.Interfaces;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Business_Objects
{
    public class UnidadVehicularBO
    {
        private IUnidadVehicularRepository unidadVehicularRepository;
        //private List<UnidadVehicular> listaVehiculos;

        public UnidadVehicularBO(IUnidadVehicularRepository unidadVehicularRepository)
        {
            this.unidadVehicularRepository = unidadVehicularRepository;
            //GetAll();
        }

        public List<UnidadVehicular> GetAll() //READ
        {
            /*if (listaVehiculos != null && listaVehiculos.Count != 0)
            {
                return listaVehiculos;
            }
            else
            {
                listaVehiculos = unidadVehicularRepository.GetAll().ToList();

                /*foreach (Cliente x in listaDB)
                {
                    ClienteDTO obj = new ClienteDTO();
                    MyMapper.Map(x, obj);
                    listaClienteDTO.Add(obj);
                }

                return listaVehiculos;
            }*/

            return unidadVehicularRepository.GetAll().ToList();
        }

        public bool Registrar(UnidadVehicular obj)
        {
            if (!String.IsNullOrWhiteSpace(obj.Placa))//EVALUO CAMPOS OBLIGATORIOS
            {
                if (!unidadVehicularRepository.Exists(obj.Placa))//EVALUO SI YA EXISTE
                {
                    //Primero verifico si se agrego de manera correcta a la DB, luego lo agrego a la Lista in Memory
                    var Result = unidadVehicularRepository.Insert(obj);

                    return Result;
                }
                else
                {
                    throw new Exception(ExceptionMessageManager.ExceptionMessageVehiculo.AlreadyExists(obj.Placa));
                }
            }
            else
            {
                throw new Exception(ExceptionMessageManager.ExceptionMessageVehiculo.KeyIsNull());
            }
        }


        public bool Actualizar(UnidadVehicular obj)
        {
            if (!String.IsNullOrWhiteSpace(obj.Placa))//EVALUO CAMPOS OBLIGATORIOS
            {
                if (unidadVehicularRepository.Exists(obj.Placa))//EVALUO SI YA EXISTE
                {
                    return unidadVehicularRepository.Update(obj);
                }
                else
                {
                    throw new Exception(ExceptionMessageManager.ExceptionMessageVehiculo.DoesNotExist(obj.Placa));
                }
            }
            else
            {
                throw new Exception(ExceptionMessageManager.ExceptionMessageVehiculo.KeyIsNull());
            }
        }

        public bool Eliminar(string Placa)
        {
            if (!String.IsNullOrWhiteSpace(Placa))//EVALUO CAMPOS OBLIGATORIOS
            {
                if (unidadVehicularRepository.Exists(Placa))
                {
                    var result = unidadVehicularRepository.Delete(Placa);

                    return result;
                }
                else
                {
                    throw new Exception(ExceptionMessageManager.ExceptionMessageVehiculo.DoesNotExist(Placa));
                }
            }
            else
            {
                throw new Exception(ExceptionMessageManager.ExceptionMessageVehiculo.KeyIsNull());
            }
        }

    }
}
