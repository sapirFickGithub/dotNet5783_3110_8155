using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
namespace Dal;
using DO;
using System.Runtime.CompilerServices;
using static Dal.DataSource;


internal class DalOrder : IOrder
{
    public int Add(Order order)
    {
        order.idOfOrder = _Config.get_OrderID;
        if (search(order))
        {
            throw new duplication();
        }
        //listOrder[listOrder.Count()] = order;
        listOrder.Add(order);
        return order.idOfOrder;
    }
    public Order get(int id)
    {
        int i;
        for (i = 0; i < listOrder.Count(); i++)
        {
            if (id == listOrder[i]?.idOfOrder)
            {
                return (Order)listOrder[i];
            }
        }
        throw new notFound();
    }
    public IEnumerable<Order?> getAll(Func<Order?, bool>? param)
    {

        if (param == null)
        {
            return listOrder;
        }
        //List<Order?> list = new List<Order?>();
       var  list = from item in listOrder where param(item) select item;
        //foreach (var item in listOrder)
        //{
        //    if (param(item))
        //    {
        //        list.Add(item);
        //    }
        //}
        return list.AsEnumerable();
    }
    public void delete(int id)
    {

        listOrder.Remove(get(id));

    }
    public void update(Order newOrder)
    {
        if (search(newOrder))
        {
            for (int i = 0; i < listOrder.Count(); i++)
            {
                if (newOrder.idOfOrder == listOrder[i]?.idOfOrder)
                    listOrder[i] = newOrder;
            }
        }
        else
        {
            throw new notFound();
        }
    }
    public bool search(Order find)//help function
    {

        int i;
        for (i = 0; i < listOrder.Count(); i++)
        {
            if (find.idOfOrder == listOrder[i]?.idOfOrder)
            {
                return true;
            }
        }
        return false;
    }
    public int search(int id)//help function for delete, get id and check if the id exist in the product
    {
        int i;
        for (i = 0; i < listOrder.Count(); i++)
        {
            if (id == listOrder[i]?.idOfOrder)
            {
                return i;
            }
        }
        return -1;
    }
    public void print(int index)
    {
        Console.WriteLine(listOrder[index]);
    }
    public int length() { return listOrder.Count(); }
    public Order getByParam(Func<Order?, bool>? param)
    {
        foreach (var item in listOrder)
        {
            if (param(item))
            {
                Order? order = new Order();
                order = item;
                return (Order)order;
            }
        }
        throw new Exception("NOT EXIST!");

    }
}