using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderItem
    {
        public int idOfOrder { get; set; }//id of order
        public int idOfProduct { get; set; }//id of product
        public string? NameOfProduct { get; set; }//name of product
        public double PriceOfProduct { get; set; }
        public int amount { get; set; }
        public double totalPrice { get; set; }
        public Enum.Category? ProductCategory { get; internal set; }

        public override string ToString() => $@"
 Order item ID: {idOfOrder}
    ID of item: {idOfProduct}
    Price: {PriceOfProduct}.
    Amount: {amount}.
    Total price: {totalPrice}.
";
    }
}
