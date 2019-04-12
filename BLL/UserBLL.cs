using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Common;
using Model;
using Autofac;

namespace BLL
{
    public class UserBLL : IUser<User>
    { 
        /// <summary>
        /// 注册使用
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Create(User t)
        {
            return UserTainer.Create(t);
        }

        /// <summary>
        /// 删除用户使用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Del(int id)
        {
            return UserTainer.Del(id);
        }
        /// <summary>
        /// 获取数据,在修改中使用linq反填登陆使用
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetUsers()
        {

            return UserTainer.GetUsers();
        }

        /// <summary>
        /// 修改使用
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Upt(User user)
        {
            return UserTainer.Upt(user);
        }

    }
}
