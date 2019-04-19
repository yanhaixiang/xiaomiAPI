using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace API.Controllers
{
    public class PhoneController : ApiController
    {
        [HttpPost]
        public string Post(PostData post)
        {
            //获取sessionKey
            var code = post.code;
            HttpClient http = new HttpClient();
            string result = string.Empty;
            HttpResponseMessage response = http.GetAsync($"https://api.weixin.qq.com/sns/jscode2session?appid=wx8a2f89f41f89befa&secret=5ed3edb1c5bead797c3b0620a914d100&js_code={code}&grant_type=authorization_code").Result;
            if(response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsStringAsync().Result;
            }
            if (string.IsNullOrEmpty(result))
            {
                throw new Exception("未经授权的操作");
            }
            var apiResult = JsonConvert.DeserializeObject<dynamic>(result);
            string sessionKey = apiResult.session_key;
            return Mi.Jie(post.iv, sessionKey, post.encryptedData);
        }
    }
    public class PostData
    {
        public string iv { get; set; }
        public string code { get; set; }
        public string encryptedData { get; set; }
    }
}
