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


internal class DalOrder:IOrder
{
    public int Add(Order order)
    {
        order.ID = _Config.get_OrderID;
        if (search(order))
        {
            throw new duplication();
        }
        //listOrder[listOrder.Count()] = order;
        listOrder.Add(order);
        return order.ID;
    }
    public Order get(int id)
    {
        int i;
        for (i = 0; i < listOrder.Count(); i++)
        {
            if (id == listOrder[i].ID)
            {
                return listOrder[i];
            }
        }
        throw new notFound();
    }
    public IEnumerable<Order> getAll()
    {
        return listOrder;
    }
    public void delete(int id)
    {
        if (0 > search(id))
        {
            throw new notFound();
        }
        List<Order> Order = new List<Order>();
        int i;
        for (i = 0; i < listOrder.Count(); i++)
        {
            if (id == listOrder[i].ID)
            {
                Order p = new Order();
                listOrder[i] = p;
            }
        }
    }
    public void update(Order newOrder)
    {
        if (search(newOrder))
        {
            for (int i = 0; i < listOrder.Count(); i++)
            {
                if (newOrder.ID == listOrder[i].ID)
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
        int count = 0;
        int i;
        for (i = 0; i < listOrder.Count(); i++)
        {
            if (find.ID == listOrder[i].ID)
            {
                count++;
                if (count == 0)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public int search(int id)//help function for delete, get id and check if the id exist in the product
    {
        int i;
        for (i = 0; i <listOrder.Count(); i++)
        {
            if (id == listOrder[i].ID)
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
}