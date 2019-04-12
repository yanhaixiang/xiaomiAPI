using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;
using BLL;

namespace API.Controllers
{
    public class AddressAPIController : ApiController
    {
        [HttpGet]
        public List<Address> GetAddresss()
        {
            return GetBll<Address>.CreateDal().Show();
        }
        [HttpPost]
        public int Create(Address address)
        {
            return GetBll<Address>.CreateDal().Create(address);
        }
        [HttpDelete]
        public int Del(int id)
        {
            return GetBll<Address>.CreateDal().Del(id);
        }
        [HttpPut]
        public int Upt(Address address)
        {
            return GetBll<Address>.CreateDal().Upt(address);
        }
    }
}
