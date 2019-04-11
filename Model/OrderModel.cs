using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int OrderNumber { get; set; }
        public int UserId { get; set; }
        public int Freight { get; set; }
        public int OrderState { get; set; }
        public float TurePrice { get; set; }
        public DateTime PaymentTime { get; set; }
        public DateTime ShipmentTime { get; set; }
        public DateTime HarvestTime { get; set; }
    }
}
