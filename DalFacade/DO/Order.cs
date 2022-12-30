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
public struct  Order 
{
    /// <summary>
    /// ID of order
    /// </summary>
    public int idOfOrder { get; set; }


    /// <summary>
    /// the name of the customer
    /// </summary>
    public string? CustomerName { get; set; }


    /// <summary>
    /// the mail addres of the customer
    /// </summary>
    public string? CustomerMail { get; set; }


    /// <summary>
    /// the address of the delivery
    /// </summary>
    public string? CustamerAddress { get; set; }


    /// <summary>
    ///order been received and take care of תאריך יצירת משלוח 
    /// </summary>
    public DateTime? DateOfOrder { get; set; }


    /// <summary>
    /// the order has been ordered and shipped out to the customer תאריך משלוח 
    /// </summary>
    public DateTime? DateOfShipping { get; set; }


    /// <summary>
    ///   the order being ordered shipped and delivered to the customer תאריך מסירה 
    /// </summary>
    public DateTime? DateOfDelivery { get; set; }


    /// <summary>
    /// make a string with all the fields in the struct.
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
    ID of order: {idOfOrder}
    Name of customer: {CustomerName}
    Customer's Mail- {CustomerMail}
    Custamer's address- {CustamerAddress}
    Date of order: {DateOfOrder}
    Date of shipping: {DateOfShipping}
    Date of delivery: {DateOfDelivery}";
}

