using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public struct Cart
    {
        public string? CustomerName { get; set; }
        public string? CustomerMail { get; set; }
        public string? CustomerAddress { get; set; }
        public List<OrderItem> itemList { get; set; }
        public double TotalPrice { get; set; }


        public override string ToString() => $@"
Cart:
   Customer name: {CustomerName}.
   Customer Mail- {CustomerMail}.
   Custamer address- {CustomerAddress}.
   Item: {itemList}.
   Total price: {TotalPrice}.
";
    }
}
