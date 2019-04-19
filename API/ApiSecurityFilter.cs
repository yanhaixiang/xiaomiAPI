using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Security;

namespace API
{
    public class ApiSecurityFilter : ActionFilterAttribute
    {
        //请求有效性验证
        //合法请求为 带有 时间戳+随机数+数据(get/post)+数字签名(token)
        //数字签名=时间戳+随机数+私钥+数据 进行md5加密后的字符串
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string staffid = "#9793932i82`/";
            string timestamp = string.Empty, nonce = string.Empty, singture = string.Empty;
            //消息头中的关键数据
            if (actionContext.Request.Headers.Contains("timestamp"))
            {
                timestamp = actionContext.Request.Headers.GetValues("timestamp").FirstOrDefault();
            }
            if (actionContext.Request.Headers.Contains("nonce"))
            {
                nonce = actionContext.Request.Headers.GetValues("nonce").FirstOrDefault();
            }
            if (actionContext.Request.Headers.Contains("singture"))
            {
                singture = actionContext.Request.Headers.GetValues("singture").FirstOrDefault();
            }
            if (string.IsNullOrEmpty(timestamp) || string.IsNullOrEmpty(nonce) || string.IsNullOrEmpty(singture))
            {
                throw new Exception("必要参数缺失");
            }

            var method = actionContext.Request.Method.Method;
            IDictionary<string, string> sortedParams = null;
            switch (method.ToUpper())
            {
                case "POST":
                case "DELETE":
                case "PUT":
                    Stream stream = HttpContext.Current.Request.InputStream;
                    StreamReader reader = new StreamReader(stream);
                    sortedParams = new SortedDictionary<string, string>(new JsonSerializer().Deserialize<Dictionary<string, string>>(new JsonTextReader(reader)));
                    break;
                case "GET":
                    IDictionary<string, string> paramters = new Dictionary<string, string>();
                    foreach (string item in HttpContext.Current.Request.QueryString)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            paramters.Add(item, HttpContext.Current.Request.QueryString[item]);
                        }
                    }
                    sortedParams = new SortedDictionary<string, string>(paramters);
                    break;

                default:
                    break;
            }
            var data = string.Empty;//请求参数
            StringBuilder query = new StringBuilder();
            if (sortedParams != null)
            {
                foreach (var sort in sortedParams.OrderBy(o => o.Key))
                {
                    if (!string.IsNullOrEmpty(sort.Key))
                    {
                        query.Append(sort.Key).Append(sort.Value);
                    }
                }
                data = query.ToString().Replace(" ", "");
            }

            //生产签名并和客户端传递的签名对比

            var md5 = FormsAuthentication.HashPasswordForStoringInConfigFile(timestamp + nonce + staffid + data, "MD5").ToLower();

            if (!md5.Equals(singture.ToLower()))
            {
                throw new Exception("无权访问");
            }

        }

    }
}