using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderTracking
    {
        public int ID { get; set; }
        public OrderSatus Status { get; set; }

        public override string ToString() => $@"
Order ID={ID}.
   Status: {Status}.
";
    }
}
