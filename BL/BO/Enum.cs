using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public enum Category { FAMILLY, RACE, JEEP, SPORT }

/// ordered - order been received and take care of
///Shipped - the order has been ordered and shipped out to the customer
///Delivered - the order being ordered shipped and delivered to the customer

public enum OrderStatus { DLIVERY, SHIPPED, ORDERED }
