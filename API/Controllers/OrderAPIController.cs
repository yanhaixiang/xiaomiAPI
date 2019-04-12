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
    public class OrderAPIController : ApiController
    {
        public List<Order> GetOrders()
        {
            return GetBll<Order>.CreateDal().Show();
        }
    }
}
