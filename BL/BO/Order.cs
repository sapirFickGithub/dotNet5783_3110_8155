using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Order
    {
        public int ID { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerMail { get; set; }
        public string? CustomerAddress { get; set; }
        public DateTime? DateCreateDelivery { get; set; }
        public DateTime? DateOfOrder { get; set; }
        public DateTime? DateOfDelivery { get; set; }
        public OrderSatus Status { get; set; }//
        public DateTime? PaymentDate { get; set; }
        public OrderItem Items { get; set; }
        public double TotalPrice { get; set; }


       public override string ToString() => $@"
Order ID={ID}: {CustomerName}.
   Customer Mail- {CustomerMail}.
   Custamer address- {CustomerAddress}.
   Date of order: {DateOfOrder}.
   Date of delivery: {DateOfDelivery}.
   Status: {Status}.
   Date of payment: {PaymentDate}.
   Items: {Items}.
   Total price: {TotalPrice}.
";
    }
}