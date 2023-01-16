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

    public int search(int id)//help function, get id and check if the id exist in the product and send the index of the list
    {
        List<DO.OrderItem?> listOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);
        return listOrderItem.FindIndex(item => id == item?.idOfOrder);
    }
    public int length()
    {
        List<DO.OrderItem?> listOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);
        return listOrderItem.Count();
    }
    public void print()
    {
        List<DO.OrderItem?> listOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);
        listOrderItem.ForEach(item => Console.WriteLine(item));
    }
    public int Add(OrderItem orderItem1)
    {
        List<DO.OrderItem?> listOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

        orderItem1.ID = int.Parse(config.Element("OrderItemID")!.Value) + 1;
        XmlTools.SaveConfigXElement("OrderItemID", orderItem1.ID);
        listOrderItem.Add(orderItem1);

        XmlTools.SaveListToXMLSerializer(listOrderItem, orderItemPath);

        return orderItem1.ID;
    }

 

    public void update(OrderItem orderItem)
    {
        List<DO.OrderItem?> listOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);

        OrderItem? isNULL = getOneByParam(a => a?.ID == orderItem.ID);

        int I = listOrderItem.IndexOf(isNULL);
        if (I != -1) // if the ID exist update the details else throw an Error
        {
            listOrderItem[I] = orderItem;
            XmlTools.SaveListToXMLSerializer(listOrderItem, orderItemPath);
        }
        else
        {
            throw new Exception("object doesn't exist - Update");
        }
    }

    public void delete(int IDorderItem)
    {

        List<DO.OrderItem?> listOrderItem = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(orderItemPath);
        listOrderItem.Remove(getOneByParam(x => IDorderItem == x?.ID));
        XmlTools.SaveListToXMLSerializer(listOrderItem, orderItemPath);
        
    }

    public IEnumerable<OrderItem?> getAllByParam(Func<OrderItem?, bool>? func = null)
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

    public OrderItem? getOneByParam(Func<OrderItem?, bool>? func)
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
