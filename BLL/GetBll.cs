using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace BLL
{
    public class GetBll<T> where T:class,new()
    {
        static IDAL<T> dal = null;
        /// <summary>
        /// 单例使用
        /// </summary>
        static GetBll()
        {
            if (dal == null)
            {
                //创建DAL对象
                dal = new AutoFacData<T>().AutoFac();
            }
        }

        /// <summary>
        /// 返回dal的实例
        /// </summary>
        /// <returns></returns>
        public static IDAL<T> CreateDal()
        {
            return dal;
        }
    }
}
