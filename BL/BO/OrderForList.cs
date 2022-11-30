using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderForList
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public Enum.OrderStatus Status { get; set; }
        public int AmountOfItem { get; set; }
        public double TotalPrice { get; set; }

        public override string ToString() => $@"
Order for list ID={ID}: {CustomerName}..
   Status: {Status}.
   Amount of items: {AmountOfItem}.
   Total price: {TotalPrice}.
";
    }
}
