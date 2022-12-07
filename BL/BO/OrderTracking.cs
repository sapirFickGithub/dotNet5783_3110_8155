using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderTracking
    {
        public int idOfOrder { get; set; }
        public Enum.OrderStatus Status { get; set; }
       public List<Tuple<DateTime, Enum.OrderStatus>> Track { get; set; }
        public override string ToString() => $@"
Order ID={idOfOrder}.
   Status: {Status}.
";
    }
}
