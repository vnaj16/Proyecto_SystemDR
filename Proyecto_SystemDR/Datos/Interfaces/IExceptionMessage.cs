using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Interfaces
{
    //Los metodos serian keyisnull, exist, doesnotexist
    public interface IExceptionMessage
    {
        string KeyIsNull();

        string AlreadyExists(string ID);

        string DoesNotExist(string ID);
    }
}
