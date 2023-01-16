using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace Dal;




sealed internal class DalXml : IDal
{

    private DalXml() { }
    public static IDal Instance { get; } = new DalXml();

    public IProduct Product { get; } = new Dal.DXProduct();
    public IOrder Order { get; } = new Dal.DXOrder();
    public IOrderItem OrderItem { get; } = new Dal.DXOrderItem();

}

