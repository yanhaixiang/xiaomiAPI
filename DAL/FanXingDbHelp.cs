using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;
using Dapper;
using System.Configuration;

namespace DAL
{
    public class FanXingDbHelp<T>where T:class,new()
    {
        static IDbConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["ConnString"]);
        /// <summary>
        /// 数据显示
        /// </summary>
        /// <returns></returns>
        public static List<T> Show()
        {
            Type type = typeof(T);
            StringBuilder str = new StringBuilder("select * from " + type.Name.ToLower());
            conn.Open();
            List<T> list = new List<T>();
            try
            {
                list = conn.Query<T>(str.ToString()).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return list;
        }

        public static int Add(T t)
        {
            Type type = typeof(T);
            PropertyInfo[] prop = type.GetProperties();
            StringBuilder str =new StringBuilder($"insert into {type.Name.ToLower()} values(");
            foreach (var item in prop)
            {
                str=str.Append($"'{item.GetValue(t, null).ToString()}',");
            }
            
            str = str.ToString().Substring(0,str.Length-1);
            str += ")";
            return db.ExecuteNonQuery(str);
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int Del(T t)
        {
            Type type = typeof(T);
            PropertyInfo[] prop = type.GetProperties();
            string str = $"delete from {type.Name.ToLower() + "s"} where 1=1";
            foreach (var item in prop)
            {
                //只把id给赋值删除
                if (item.GetValue(t,null).ToString() != "")
                {
                    str += $" and {item.Name}='{item.GetValue(t, null).ToString()}'";
                }
            }
            return db.ExecuteNonQuery(str);
        }

        public static int Upt(T t)
        {
            Type type= typeof(T);
            var prop = type.GetProperties();
            string str = $"update {type.Name.ToLower() + "s"} set ";
            string str1 = "";
            foreach (var item in prop)
            {
                if (!item.Name.ToString().Contains("no"))
                {
                    str += $"{item.Name}='{item.GetValue(t, null).ToString()}',";
                }
                else
                {
                    str1 += $" where {item.Name}='{item.GetValue(t, null).ToString()}'";
                }
                
            }
            str = str.Substring(0, str.Length - 1);
            str = str + str1;
            return db.ExecuteNonQuery(str);
        }
    }
}
