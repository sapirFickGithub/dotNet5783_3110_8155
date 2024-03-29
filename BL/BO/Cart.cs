﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BlImplementation;
namespace BO
{
    public class Cart
    {
        public string? CustomerName { get; set; }
        public string? CustomerMail { get; set; }
        public string? CustomerAddress { get; set; }
        public List<OrderItem?>? itemList { get; set; } = new List<OrderItem?>();
        public double TotalPrice { get; set; } = 0;




        public override string ToString()
        {
            string print = $@"
Cart:
   Customer name: {CustomerName}.
   Customer Mail- {CustomerMail}.
   Custamer address- {CustomerAddress}.";
            print += itemList?.Aggregate("", (acc, item) => acc + "\n" + item?.NameOfProduct + "\n" + item?.ToString());
            print += "\n" + $@"TotalPrice: {TotalPrice}";
            return print;
        }

       
    }
}




