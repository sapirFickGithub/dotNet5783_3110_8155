﻿using System;
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
        public DateTime? DateCreateDelivery { get; set; }//תאריך הזמנה
        public DateTime? DateOfOrder { get; set; }//תאריך שילוח
        public DateTime? DateOfDelivery { get; set; }//תאריך אספקה
        public OrderStatus Status { get; set; }
        public DateTime? PaymentDate { get; set; }
        public List<OrderItem> Items { get; set; }
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