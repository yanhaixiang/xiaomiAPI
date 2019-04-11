using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Data;
//using System.Data.SqlClient;
using System.Reflection;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DAL
{
    public class DapperHelper<T>
    {
        static IDbConnection conn = new MySqlConnection(ConfigurationSettings.AppSettings["ConnString"]);
        public int Create(T t)
        {
            Type type = typeof(T);
            PropertyInfo[] pros = type.GetProperties();
            //实例化字符串进行拼接
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into " + type.Name + " values(@");
            string sql = "";
            foreach (var item in pros)
            {
                //判断是否为主键
                if (item.GetCustomAttribute(typeof(KeyAttribute), true) == null)
                {
                    sb.Append(item.Name + ",@");
                }
                sql = sb.ToString().Substring(0, sb.Length - 2) + ")";
            }
            return conn.Execute(sql, t);
        }
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
        public List<T> GetAll()
        {
            using (IDbConnection conn = new SqlConnection(conStr))
            {
                Type type = typeof(T);
                string sql = "select *from " + type.Name;
                return conn.Query<T>(sql).ToList();
            }
        }
        public IEnumerable<dynamic> GetAll(string procName)
        {
            using (IDbConnection conn = new SqlConnection(conStr))
            {
                return conn.Query(procName, commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete(int id)
        {
            using (IDbConnection conn = new SqlConnection(conStr))
            {
                Type type = typeof(T);
                PropertyInfo[] pros = type.GetProperties();
                string sql = "delete from " + type.Name + " where ";
                foreach (var item in pros)
                {
                    if (item.GetCustomAttribute(typeof(KeyAttribute), true) != null)
                    {
                        sql += item.Name + " = @Id";
                    }
                }
                return conn.Execute(sql, new { Id = id });
            }
        }
        public int Update(T t)
        {
            using (IDbConnection conn = new SqlConnection(conStr))
            {
                Type type = typeof(T);
                var prop = type.GetProperties();
                StringBuilder sb = new StringBuilder();
                sb.Append("update " + type.Name + " set ");
                StringBuilder sb2 = new StringBuilder();
                sb2.Append(" where ");
                string str = "";
                foreach (var item in prop)
                {
                    if (item.GetCustomAttribute(typeof(KeyAttribute), true) != null)
                    {
                        sb2.Append(item.Name + "=@" + item.Name);
                    }
                    else
                    {
                        if (item.GetValue(t) != null)
                        {
                            sb.Append(item.Name + "=@" + item.Name + ",");
                        }

                    }
                }
                str = sb.ToString().Substring(0, sb.Length - 1) + sb2.ToString();
                return conn.Execute(str, t);
            }

        }
    }
}
