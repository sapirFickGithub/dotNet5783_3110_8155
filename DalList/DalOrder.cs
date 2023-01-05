using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
namespace Dal;
using DO;
using System.Runtime.CompilerServices;
using System.Transactions;
using static Dal.DataSource;


internal class DalOrder : IOrder
{
    public int Add(Order order)
    {
        order.idOfOrder = _Config.get_OrderID;
        listOrder.Add(order);
        return order.idOfOrder;
    }
    

    public IEnumerable<Order?> getAllByParam(Func<Order?, bool>? param)
    {
        if (param == null)
        {
            return listOrder;
        }
        var list = from item in listOrder where param(item) select item;
        return list.AsEnumerable();
    }


    public void delete(int id)
    {
        listOrder.Remove(getOneByParam(x=> id ==x?.idOfOrder));
    }


    public void update(Order newOrder)
    {
        int index = search(newOrder.idOfOrder);
        if (index == -1)
        {
            throw new notExist();
        }
            listOrder[index] = newOrder;
    }


    public int search(int id)//help function, get id and check if the id exist in the product and send the index of the list
    {
        return listOrder.FindIndex(item => id == item?.idOfOrder);
    }


    public void print()
    {
        listOrder.ForEach(item => Console.WriteLine(item));
    }


    public int length() { return listOrder.Count(); }


    public Order? getOneByParam(Func<Order?, bool>? param)
    {
        return listOrder.FirstOrDefault(param);
        throw new notExist();

    }
}