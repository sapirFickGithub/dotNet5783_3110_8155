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
    public int ID { get; set; } 
    public int idOfOrder { get; set; }
    public int idOfItem { get; set; } 
    public double Price { get; set; }
    public int amount { get; set; }

    /// <summary>
    /// make a string with all the fields in the struct.
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
 Order item ID={ID}: {idOfOrder}, 
    ID of item - {idOfItem}
    Price: {Price}
    Amount: {amount}
";
}
