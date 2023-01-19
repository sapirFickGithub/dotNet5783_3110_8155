using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal class DXOrder : IOrder
{
    const string orderPath = "Order";
    static XElement config = XmlTools.LoadConfig();

    public int search(int id)//help function, get id and check if the id exist in the product and send the index of the list
    {
        List<DO.Order?> listOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);
        return listOrder.FindIndex(item => id == item?.idOfOrder);
    }
    public int length()
    {
        List<DO.Order?> listOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);
        return listOrder.Count();
    }
    public void print()
    {
        List<DO.Order?> listOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);
        listOrder.ForEach(item => Console.WriteLine(item));
    }

    public int Add(Order order)
    {
        List<DO.Order?> listOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);
        order.idOfOrder = int.Parse(config.Element("idOfOrder")!.Value) + 1;
        XmlTools.SaveConfigXElement("idOfOrder", order.idOfOrder);
        listOrder.Add(order);
        XmlTools.SaveListToXMLSerializer(listOrder, orderPath);
        return order.idOfOrder;
    }

  

    public void update(Order order)
    {
        List<DO.Order?> listOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);
        Order? isNULL = getOneByParam(
             a => a?.idOfOrder == order.idOfOrder
                             );
        int I = listOrder.IndexOf(isNULL);//DataSource.searchOrder(order.ID);
        if (I != -1) // if the ID exist update the details else throw an Error
        {
            listOrder[I] = order;
            XmlTools.SaveListToXMLSerializer(listOrder, orderPath);
        }
        else
        {
            throw new Exception("object doesn't exist - Update");
        }
    }

    public void delete(int idOrder)
    {
        List<DO.Order?> listOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);
        listOrder.Remove(getOneByParam(x => idOrder == x?.idOfOrder));
        XmlTools.SaveListToXMLSerializer(listOrder, orderPath);
    }

    public IEnumerable<Order?> getAllByParam(Func<Order?, bool>? func = null)
    {
        List<DO.Order?> listOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);
        List<Order?> finalResult = new List<Order?>();
        if (func != null)
        {
            finalResult = (List<Order?>)(from Order in listOrder
                                         where func(Order)
                                         select Order);
           
            return finalResult;
        }
        else
        {
            finalResult = listOrder;
            return finalResult;
        }
    }




    public Order? getOneByParam(Func<Order?, bool>? func)
    {
        List<DO.Order?> listOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);

        if (func != null)
        {
            var order = listOrder.Find(x => func(x));
            if (order != null)
            {
                return (Order)order;
            }
        }
        throw new notExist();
      
    }
}
