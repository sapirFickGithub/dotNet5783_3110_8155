using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderItem
    {
        public int ID { get; set; }
        public int NumOfOrder { get; set; }
        public int IdOfProduct { get; set; }
        public string? NameOfProduct { get; set; }
        public double PriceOfProduct { get; set; }
        public int amount { get; set; }
        public double totalPrice { get; set; }
        public override string ToString() => $@"
 Order item ID={ID}: {NumOfOrder}, 
    ID of item - {IdOfProduct}
    Price: {PriceOfProduct}.
    Amount: {amount}.
    Total price: {totalPrice}.
";
    }
}
