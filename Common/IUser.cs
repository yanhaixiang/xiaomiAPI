using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Common
{
    public interface IUser<T>
    {
        int Create(T t);
        IEnumerable<T> GetUsers();
        int Del(int id);
        int Upt(T t);
    }
}
