using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DO;


/// <summary>
/// struct that represent identification information about the product
/// </summary>
public struct Product
{
    public int idOfProduct { get; set; }
    public string? Name { get; set; }
    public Category ProductCategory { get; set; }
    public double Price { get; set; }
    public int InStock { get; set; }

    /// <summary>
    /// make a string with all the fields in the struct.
    /// </summary>
    /// <returns></returns>
    /// 
    public override string ToString() => $@"
    Product ID={idOfProduct}: {Name}, 
    category - {ProductCategory}
    Price: {Price}
    Amount in stock: {InStock}";
}
