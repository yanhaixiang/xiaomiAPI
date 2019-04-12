using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common;
using Model;
using BLL;

namespace API.Controllers
{
    public class UserAPIController : ApiController
    {
        //public IUser<User> GetUser { get; set; }
        UserBLL bll = new UserBLL();
        [HttpPost]
        public int Create(User user)
        {
            return bll.Create(user);
            //return GetUser.Create(user);
        }

        [HttpGet]
        public List<User> GetUsers()
        {
            return bll.GetUsers().ToList();
            //return GetUser.GetUsers().ToList();
        }
    }
}
