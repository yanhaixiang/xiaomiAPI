using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public int OrderNumber { get; set; }
        public int UserId { get; set; }
        public int Freight { get; set; }
        public int OrderState { get; set; }
        public float TurePrice { get; set; }
        public string PaymentTime { get; set; }
        public string ShipmentTime { get; set; }
        public string HarvestTime { get; set; }
    }
}
