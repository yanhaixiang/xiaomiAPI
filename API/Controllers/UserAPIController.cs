﻿using System;
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
        
        [HttpPost]
        public int Create(User user)
        {
            return GetBll<User>.CreateDal().Create(user);
        }

        [HttpGet]
        public List<User> GetUsers()
        {
            return GetBll<User>.CreateDal().Show();
        }
        [HttpDelete]
        public int Del(int id)
        {
            return GetBll<User>.CreateDal().Del(id);
        }
        [HttpPut]
        public int Upt(User user)
        {
            return GetBll<User>.CreateDal().Upt(user);
        }
    }
}
