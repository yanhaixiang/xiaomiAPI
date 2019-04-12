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
using Common;

namespace DAL
{
    public class DapperHelper<T>:IDAL<T> where T:new()
    { 
        static IDbConnection conn = new MySqlConnection(ConfigurationSettings.AppSettings["ConnString"]);
        /// <summary>
        /// 数据的添加
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public  int Create(T t)
        {
            Type type = typeof(T);
            //获取属性
            PropertyInfo[] pros = type.GetProperties();
            //实例化字符串进行拼接
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into `" + type.Name + "`(");
            //遍历属性值
            foreach (var item in pros)
            {
                //判断是不是主键带id 比如studentid
                if (!(type.Name.ToString().ToLower() + "id").Equals(item.Name.ToString().ToLower()))
                {
                    if (item.GetValue(t, null).ToString() != null)
                    {
                        sb.Append(item.Name.ToString() + ",");
                    }
                }
            }

            //insert into student(studentName,studentSex,
            //截取最后的,
            sb.Replace(sb.ToString(), sb.ToString().Substring(0, sb.ToString().LastIndexOf(',')));
            //insert into student(studentName,studentSex
            sb.Append(") values(");
            //insert into student(studentName,studentSex) values(
            //遍历属性值
            foreach (var item in pros)
            {
                //判断是不是主键带id 比如studentid
                if (!(type.Name.ToString().ToLower() + "id").Equals(item.Name.ToString().ToLower()))
                {
                    if (item.GetValue(t, null).ToString() != null)
                    {
                        sb.Append("@" + item.Name.ToString() + ",");
                    }

                }
            }
            //insert into student(studentName,studentSex) values(@studentName,@studentSex,
            //截取最后的,
            sb.Replace(sb.ToString(), sb.ToString().Substring(0, sb.ToString().LastIndexOf(',')));
            //insert into student(studentName,studentSex) values(@studentName,@studentSex
            sb.Append(");");
            //insert into student(studentName,studentSex) values(@studentName,@studentSex)
            int i = 0;
            try
            {
                conn.Open();
                i = conn.Execute(sb.ToString(), t);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return i;
        }
        /// <summary>
        /// 数据显示
        /// </summary>
        /// <returns>获取到表中所有的数据</returns>
        public  List<T> Show()

        {
            Type type = typeof(T);
            StringBuilder str = new StringBuilder("select * from `" + type.Name.ToLower() + "`;");
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
        /// <summary>
        /// 根据Id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  int Del(int id)
        {
            Type type = typeof(T);
            //获取model中属性
            PropertyInfo[] pros = type.GetProperties();
            //实例化字符串进行拼接
            StringBuilder sb = new StringBuilder();
            sb.Append("delete from `" + type.Name.ToString() + "` where " + type.Name.ToString() + "Id=@id;");
            int i = 0;
            try
            {
                conn.Open();
                i = conn.Execute(sb.ToString(), id);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return i;
        }
        /// <summary>
        /// 修改使用方法
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public  int Upt(T t)
        {
            Type type = typeof(T);
            //获取属性
            PropertyInfo[] pros = type.GetProperties();
            //实例化字符串进行拼接
            StringBuilder sb = new StringBuilder();
            sb.Append("update `" + type.Name + "` set ");
            //update student set 
            //遍历属性值
            foreach (var item in pros)
            {
                //判断是不是主键带id 比如studentid
                if (!(type.Name.ToString().ToLower() + "id").Equals(item.Name.ToString().ToLower()))
                {
                    if (item.GetValue(t, null).ToString() != null)
                    {
                        sb.Append(item.Name.ToString() + "=@" + item.Name.ToString() + ",");
                    }
                }
            }
            //update student set studentName=@studentName,studentSex=@studentSex, 
            //截取最后的,
            sb.Replace(sb.ToString(), sb.ToString().Substring(0, sb.ToString().LastIndexOf(',')));
            //update student set studentName=@studentName,studentSex=@studentSex
            //添加id判断
            sb.Append(" where " + type.Name.ToString() + "Id=@" + type.Name.ToString() + "Id;");
            //update student set studentName=@studentName,studentSex=@studentSex where studentId=@studentId;
            int i = 0;
            try
            {
                conn.Open();
                i = conn.Execute(sb.ToString(), t);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return i;
        }
    }
}
