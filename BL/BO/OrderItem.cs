using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderItem
    {
        public int ID { get; set; }//num of order
        public int IdOfProduct { get; set; }
        public string? NameOfProduct { get; set; }//name of product
        public double PriceOfProduct { get; set; }
        public int amount { get; set; }
        public double totalPrice { get; set; }
        public Enum.Category ProductCategory { get; internal set; }

        public override string ToString() => $@"
 Order item ID: {ID}
    ID of item: {IdOfProduct}
    Price: {PriceOfProduct}.
    Amount: {amount}.
    Total price: {totalPrice}.
";
    }
}
