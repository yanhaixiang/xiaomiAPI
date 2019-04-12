using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Common;

namespace DAL
{
    public class UserDal:IUser<User>
    {
        public int Create(User user)
        {
            return DapperHelper<User>.Create(user);
        }

        public int Del(int id)
        {
            return DapperHelper<User>.Delete(id);
        }

        /// <summary>
        /// 这个方法需要实现修改的反填查询和登陆调用
        /// </summary>
        /// <returns></returns>
        public  IEnumerable<User> GetUsers()
        {
            return DapperHelper<User>.Show();
        }

        public int Upt(User user)
        {
            return DapperHelper<User>.Update(user);
        }
    }
}
