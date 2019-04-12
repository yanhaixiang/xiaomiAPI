using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Common
{
    public interface IDAL<T>
    {
        int Create(T t);
        List<T> Show();
        int Del(int id);
        int Upt(T t);
    }
}
