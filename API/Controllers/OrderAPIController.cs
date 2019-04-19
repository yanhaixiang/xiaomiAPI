using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;
using Newtonsoft.Json;
using BLL;

namespace API.Controllers
{
    [ApiSecurityFilter]
    public class OrderAPIController : ApiController
    {
        [HttpGet]
        public List<Order> GetOrders()
        {
            return GetBll<Order>.CreateDal().Show();
        }
        [HttpPost]
        public int Create(Order order)
        {
            return GetBll<Order>.CreateDal().Create(order);
        }
        [HttpDelete]
        public int Del(int id)
        {
            return GetBll<Order>.CreateDal().Del(id);
        }
        [HttpPut]
        public int Upt(Order order)
        {
            return GetBll<Order>.CreateDal().Upt(order);
        }

        [HttpGet]
        public List<Order> GetOrders(int TJ)
        {
            return GetOrders().Where(m => m.OrderState == TJ).ToList();
        }

    }
}
