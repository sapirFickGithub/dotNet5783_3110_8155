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
    /// <summary>
    /// ID number of the product
    /// </summary>
    public int idOfProduct { get; set; }


    /// <summary>
    /// name of product
    /// </summary>
    public string? Name { get; set; }


    /// <summary>
    /// the category of the product
    /// </summary>
    public Category? ProductCategory { get; set; }


    /// <summary>
    /// the price of the product
    /// </summary>
    public double Price { get; set; }


    /// <summary>
    /// who much products in the stock
    /// </summary>
    public int InStock { get; set; }


    /// <summary>
    /// make a string with all the fields in the struct.
    /// </summary>
    /// <returns></returns>
    /// 
    public override string ToString() => $@"
    ID of product: {idOfProduct}
    Name of product: {Name} 
    Category: {ProductCategory}
    Price: {Price}
    Product's amount in stock: {InStock}";
}
