using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    const string orderItemPath = "OrderItem";
    static XElement config = XmlTools.LoadConfig();

    public int Create(OrderItem orderItem1)
    {
        List<DO.OrderItem?> listOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

        orderItem1.OrderItemID = int.Parse(config.Element("OrderItemID")!.Value) + 1;
        XmlTools.SaveConfigXElement("OrderItemID", orderItem1.OrderItemID);
        listOrderItem.Add(orderItem1);

        XmlTools.SaveListToXMLSerializer(listOrderItem, orderItemPath);

        return orderItem1.OrderItemID;
    }

    public OrderItem Read(OrderItem orderItem)
    {
        List<DO.OrderItem?> listOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

        OrderItem? isNULL = ReadObject(a => a?.OrderItemID == orderItem.OrderItemID);

        int I = listOrderItem.IndexOf(isNULL);

        if (I != -1) // if the ID exist return the details else throw an Error
        {
            return (OrderItem)listOrderItem[I];
        }
        else
        {
            throw new IDWhoException("Delete range Error ");
        }
    }

    public void Update(OrderItem orderItem)
    {
        List<DO.OrderItem?> listOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

        OrderItem? isNULL = ReadObject(a => a?.OrderItemID == orderItem.OrderItemID);

        int I = listOrderItem.IndexOf(isNULL);
        if (I != -1) // if the ID exist update the details else throw an Error
        {
            listOrderItem[I] = orderItem;
            XmlTools.SaveListToXMLSerializer(listOrderItem, orderItemPath);
        }
        else
        {
            throw new IDWhoException("object doesn't exist - Update");
        }
    }

    public void Delete(OrderItem orderItem)
    {
        List<DO.OrderItem?> listOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

        OrderItem? isNULL = ReadObject(a => a?.OrderItemID == orderItem.OrderItemID);

        if (isNULL?.OrderItemID != -1) // if the ID exist delete the details else throw an Error
        {
            listOrderItem.Remove(orderItem);
            XmlTools.SaveListToXMLSerializer(listOrderItem, orderItemPath);
        }
        else
        {
            throw new IndexOutOfRangeException("Delete range Error");
        }
    }

    public IEnumerable<OrderItem?> ReadAll(Func<OrderItem?, bool>? func = null)
    {
        List<DO.OrderItem?> listOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

        if (func != null)
        {
            var finalResult = from item in listOrderItem
                              where func(item)
                              select item;

            return finalResult;

        }
        else
        {
            List<OrderItem?> finalResult = new List<OrderItem?>();
            finalResult = listOrderItem;
            return finalResult;
        }
    }

    public OrderItem ReadObject(Func<OrderItem?, bool>? func)
    {
        List<DO.OrderItem?> listOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

        if (func != null)
        {
            var orderItem = listOrderItem.Find(x => func(x));
            if (orderItem != null)
            {
                return (OrderItem)orderItem;
            }
        }

        throw new notExist();
    }

}
