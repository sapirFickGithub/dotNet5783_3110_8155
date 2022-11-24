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
        public int IdOfItem { get; set; }
        public double Price { get; set; }
        public int amount { get; set; }
        public double totalPrice { get; set; }
        public override string ToString() => $@"
 Order item ID={ID}: {NumOfOrder}, 
    ID of item - {IdOfItem}
    Price: {Price}s
    Amount: {amount}
    Total price: {totalPrice}
";
    }
}
