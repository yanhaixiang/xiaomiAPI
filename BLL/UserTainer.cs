using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using DAL;
using Model;
using Common;

namespace BLL
{
    public class UserTainer
    {
        public static ContainerBuilder _containerBuilder;
        public static IContainer _container;
        /// <summary>
        /// 构造函数
        /// </summary>
        static UserTainer()
        {
            //实现单例
            if (_containerBuilder == null)
            {
                _containerBuilder = new ContainerBuilder();
                _containerBuilder.RegisterType<UserDal>().As<IUser<User>>();
                _container = _containerBuilder.Build();
            }
        }
        /// <summary>
        /// 使用这个方法实现查询方法
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<User> GetUsers()
        {
            using (_container.BeginLifetimeScope())
            {
                return _container.Resolve<IUser<User>>().GetUsers();
            }
        }

        /// <summary>
        /// 注册使用
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int Create(User user)
        {
            using (_container.BeginLifetimeScope())
            {
                return _container.Resolve<IUser<User>>().Create(user);
            }
        }

        /// <summary>
        /// 删除用户使用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int Del(int id)
        {
            using (_container.BeginLifetimeScope())
            {
                return _container.Resolve<IUser<User>>().Del(id);
            }
        }

        /// <summary>
        /// 修改使用
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int Upt(User user)
        {
            using (_container.BeginLifetimeScope())
            {
                return _container.Resolve<IUser<User>>().Upt(user);
            }
        }
    }
}
