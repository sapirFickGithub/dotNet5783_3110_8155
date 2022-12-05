using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DO;
/// <summary>
/// struct that represent identification information about the order
/// </summary>
public struct Order
{
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerMail { get; set; }
    public string CustamerAddress { get; set; }
    public DateTime? DateOfOrder { get; set; }
    public DateTime? DateOfShipping { get; set; }
    public DateTime? DateOfDelivery { get; set; }

    /// <summary>
    /// make a string with all the fields in the struct.
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
Order ID={ID}: {CustomerName}, 
  Customer Mail- {CustomerMail}
  Custamer address- {CustamerAddress}
  Date of order: {DateOfShipping}
  Date of delivery: {DateOfDelivery}
";
}
