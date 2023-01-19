using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Order
    {
       

        public int idOfOrder { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerMail { get; set; }
        public string? CustomerAddress { get; set; }
        public DateTime? DateOfOrder { get; set; }//תאריך הזמנה
        public DateTime? DateOfShipping { get; set; }//תאריך שילוח
        public DateTime? DateOfDelivery { get; set; }//תאריך אספקה
        public Enum.OrderStatus? Status { get; set; }
        
        public List<OrderItem?>? Items { get; set; }
        public double TotalPrice { get; set; }
      

        public override string ToString() {
            string print = $@"
Order ID={idOfOrder}: {CustomerName}.
   Customer Mail- {CustomerMail}.
   Custamer address- {CustomerAddress}.
   Date of order: {DateOfShipping}.
   Date of delivery: {DateOfDelivery}.
   Status: {Status}.";
   
            foreach (var item in Items)
            {
                print = print + "\n" + $@"TotalPrice: {TotalPrice}";
            }
            return print;
        }
    }
}