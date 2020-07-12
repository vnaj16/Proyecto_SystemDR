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
        private List<Historial> listaHistoriales;

        public HistorialBO(IHistorialRepository historialRepository)
        {
            this.historialRepository = historialRepository;
            GetAll();
        }

        public List<Historial> GetAll() //READ
        {
            if (listaHistoriales != null && listaHistoriales.Count != 0)
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
                }*/

                return listaHistoriales;
            }
        }

        public bool Registrar(Historial obj)
        {
            if (obj.ID>=0)//EVALUO CAMPOS OBLIGATORIOS
            {
                if (!listaHistoriales.Exists(x => x.ID == obj.ID))//EVALUO SI YA EXISTE
                {
                    try
                    {
                        //Primero verifico si se agrego de manera correcta a la DB, luego lo agrego a la Lista in Memory
                        var Result = historialRepository.Insert(obj);


                        if (Result) listaHistoriales.Add(obj);

                        return Result;
                    }
                    catch(Exception ex) { throw; }
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

        public bool Eliminar(int ID)
        {
            if (ID !=0)//EVALUO CAMPOS OBLIGATORIOS
            {
                if (listaHistoriales.Exists(x => x.ID == ID))
                {
                    var result = historialRepository.Delete(ID.ToString());

                    if (result) listaHistoriales.Remove(listaHistoriales.FirstOrDefault(x => x.ID == ID));

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

        public bool Actualizar(Historial obj)
        {
            if (obj.ID !=0)//EVALUO CAMPOS OBLIGATORIOS
            {
                var current = listaHistoriales.FirstOrDefault(x => x.ID == obj.ID);
                if (!(current is null))//EVALUO SI YA EXISTE
                {

                    if (!String.IsNullOrWhiteSpace(obj.Descripcion))
                    {
                        current.Descripcion = obj.Descripcion;
                    }

                    if (!String.IsNullOrWhiteSpace(obj.Eventualidad))
                    {
                        current.Eventualidad = obj.Eventualidad;
                    }

                    if (String.IsNullOrWhiteSpace(current.Fecha.ToString()))
                    {
                        current.Fecha = obj.Fecha;
                    }

                    if (String.IsNullOrWhiteSpace(current.Lugar))
                    {
                        current.Lugar = obj.Lugar;
                    }


                    //Lo agrego a la lista en memoria, luego a la DB
                    return historialRepository.Update(current);
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
