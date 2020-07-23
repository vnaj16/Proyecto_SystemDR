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
        private List<UnidadVehicular> listaVehiculos;

        public UnidadVehicularBO(IUnidadVehicularRepository unidadVehicularRepository)
        {
            this.unidadVehicularRepository = unidadVehicularRepository;
            GetAll();
        }

        public List<UnidadVehicular> GetAll() //READ
        {
            if (listaVehiculos != null && listaVehiculos.Count != 0)
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
                }*/

                return listaVehiculos;
            }
        }

        public bool Registrar(UnidadVehicular obj)
        {
            if (!String.IsNullOrWhiteSpace(obj.Placa))//EVALUO CAMPOS OBLIGATORIOS
            {
                if (!listaVehiculos.Exists(x => x.Placa == obj.Placa))//EVALUO SI YA EXISTE
                {
                    //Primero verifico si se agrego de manera correcta a la DB, luego lo agrego a la Lista in Memory
                    var Result = unidadVehicularRepository.Insert(obj);

                    if (Result) listaVehiculos.Add(obj);

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


        public bool Actualizar(UnidadVehicular obj)
        {
            if (!String.IsNullOrWhiteSpace(obj.Placa))//EVALUO CAMPOS OBLIGATORIOS
            {
                UnidadVehicular current = listaVehiculos.FirstOrDefault(x => x.Placa == obj.Placa);
                if (!(current is null))//EVALUO SI YA EXISTE
                {

                    if (!String.IsNullOrWhiteSpace(obj.Marca))
                    {
                        current.Marca = obj.Marca;
                    }

                    if (!String.IsNullOrWhiteSpace(obj.SerieChasis))
                    {
                        current.SerieChasis = obj.SerieChasis;
                    }


                    if (!String.IsNullOrWhiteSpace(current.YFabricacion.ToString()))
                    {
                        current.YFabricacion = obj.YFabricacion;
                    }

                    //Lo agrego a la lista en memoria, luego a la DB
                    return unidadVehicularRepository.Update(current);
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

        public bool Eliminar(string Placa)
        {
            if (!String.IsNullOrWhiteSpace(Placa))//EVALUO CAMPOS OBLIGATORIOS
            {
                if (listaVehiculos.Exists(x => x.Placa == Placa))
                {
                    var result = unidadVehicularRepository.Delete(Placa);

                    if (result) listaVehiculos.Remove(listaVehiculos.FirstOrDefault(x => x.Placa == Placa));

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
