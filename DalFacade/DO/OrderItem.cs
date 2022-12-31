using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DO;

/// <summary>
///  order's details. like ID,identification number of the order ,final price...
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// ID of the specific orderItem
    /// </summary>
    public int ID { get; set; } 


    /// <summary>
    /// ID of order 
    /// </summary>
    public int idOfOrder { get; set; }


    /// <summary>
    /// id of product
    /// </summary>
    public int idProduct { get; set; } 


    /// <summary>
    /// the price per unit
    /// </summary>
    public double Price { get; set; }


    /// <summary>
    /// the amount of per unit
    /// </summary>
    public int amount { get; set; }

    /// <summary>
    /// make a string with all the fields in the struct.
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
    ID of order item: {ID}
    ID of orde: {idOfOrder}, 
    ID of product: {idProduct}
    Price per unit: {Price}
    Amount of items: {amount}
";
}
