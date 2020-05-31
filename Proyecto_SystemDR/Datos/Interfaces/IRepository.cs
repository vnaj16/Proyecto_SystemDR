using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Interfaces
{
    public interface IRepository<T>
    {
        bool Insert(T obj);
        bool Update(T obj);
        bool Delete(string ID);
        IEnumerable<T> GetAll();
    }
}
