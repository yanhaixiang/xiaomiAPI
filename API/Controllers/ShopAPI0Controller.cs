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
    public class ShopAPI0Controller : ApiController
    {
        [HttpGet]
        public List<Shop> GetShops()
        {
            return GetBll<Shop>.CreateDal().Show();
        }
        [HttpPost]
        public int Create(Shop shop)
        {
            return GetBll<Shop>.CreateDal().Create(shop);
        }
        [HttpDelete]
        public int Del(int id)
        {
            return GetBll<Shop>.CreateDal().Del(id);
        }
        [HttpPut]
        public int Upt(Shop shop)
        {
            return GetBll<Shop>.CreateDal().Upt(shop);
        }
    }
}
