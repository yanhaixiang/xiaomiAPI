using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Common;
using DAL;

namespace BLL
{
    public class AutoFacData<T> where T:class,new()
    {
        public IDAL<T> AutoFac()
        {
            try
            {
                ContainerBuilder builder = new ContainerBuilder();
                builder.RegisterGeneric(typeof(DapperHelper<>)).As(typeof(IDAL<>));
                using (var container=builder.Build())
                {
                    var manager = container.Resolve<IDAL<T>>();
                    return manager;
                }
            }
            catch (Exception e)
            {
                return default(IDAL<T>);
            }
        }
    }
}
