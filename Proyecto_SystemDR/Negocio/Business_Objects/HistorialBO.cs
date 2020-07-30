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
    public class HistorialBO
    {
        private IHistorialRepository historialRepository;
        //private List<Historial> listaHistoriales;

        public HistorialBO(IHistorialRepository historialRepository)
        {
            this.historialRepository = historialRepository;
            //GetAll();
        }

        public List<Historial> GetAll() //READ
        {
            /*if (listaHistoriales != null && listaHistoriales.Count != 0)
            {
                return listaHistoriales;
            }
            else
            {
                listaHistoriales = historialRepository.GetAll().ToList();

                /*foreach (Cliente x in listaDB)
                {
                    ClienteDTO obj = new ClienteDTO();
                    MyMapper.Map(x, obj);
                    listaClienteDTO.Add(obj);
                }

                return listaHistoriales;
            }*/

            return historialRepository.GetAll().ToList();
        }

        public bool Registrar(Historial obj)
        {
            if (historialRepository.Exists(obj.Id.ToString()))//EVALUO SI YA EXISTE
            {
                if (!String.IsNullOrEmpty(obj.DniConductor))
                {
                    var Result = historialRepository.Insert(obj);

                    return Result;
                }
                else
                {
                    throw new Exception("No existe conductor asignado");
                }
            }
            else
            {
                throw new Exception("Este historial ya existe");
            }
        }

        public bool Eliminar(int ID)
        {
            if (ID > 0)//EVALUO CAMPOS OBLIGATORIOS
            {
                if (historialRepository.Exists(ID.ToString()))
                {
                    var result = historialRepository.Delete(ID.ToString());

                    return result;
                }
                else
                {
                    throw new Exception("No existe un historial con ese ID");
                }
            }
            else
            {
                throw new Exception("No hay ID asignado");
            }
        }

        public bool Actualizar(Historial obj)
        {
            if (obj.Id > 0)//EVALUO CAMPOS OBLIGATORIOS
            {
                if (historialRepository.Exists(obj.Id.ToString()))
                {
                    return historialRepository.Update(obj);
                }
                else
                {
                    throw new Exception("No existe un historial con ese ID");
                }
            }
            else
            {
                throw new Exception("No hay ID asignado");
            }
        }

    }
}
