﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ProductItem
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public Category ProductCategory { get; set; }
        public double Price { get; set; }
        public int InStock { get; set; }
        public int Amount { get; set; }

        public override string ToString() => $@"
    Product ID={ID}: {Name}.
    category - {ProductCategory}.
    Price: {Price}.
    Amount: {Amount}.
    Amount in stock: {InStock}.
";
    }
}