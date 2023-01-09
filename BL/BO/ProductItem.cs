using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ProductItem
    {
        public int idOfProduct { get; set; }
        public string? Name { get; set; }
        public Enum.Category? ProductCategory { get; set; }
        public double Price { get; set; }
        public bool InStock { get; set; }
        
        public int Amount { get; set; }

        public override string ToString() => $@"
    Product ID={idOfProduct}: {Name}.
    category - {ProductCategory}.
    Price: {Price}.
    Amount: {Amount}.
    Amount in stock: {InStock}.
";
    }
}
