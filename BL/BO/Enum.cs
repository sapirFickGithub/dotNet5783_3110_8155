using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public class Enum
{
    //Categories of cars.=Category.
    public enum Category {Familly, Race, Jeep, Sport ,Gears, Collectors, All}


    public enum OrderStatus { DLIVERY, SHIPPED, ORDERED }
    // ORDERED - order been received and take care of
    // SHIPPED- the order has been ordered and shipped out to the customer
    // DLIVERY - the order being ordered shipped and delivered to the customer


};
