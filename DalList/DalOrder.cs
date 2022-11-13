using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DO;
using System.Runtime.CompilerServices;
using static Dal.DataSource;

public class DalOrder
{
    public int Add(Order order)
    {
        if (_Config._OrderIndex ==100)
        {
            throw new Exception("OVERLOAD! no more space in arrayOrder");
        }
        if (search(order))
        {
            throw new Exception("DUPLICATION! this Order already exist");
        }
        order.ID = _Config.get_OrderID;
        arrayOrder[_Config._OrderIndex++] = order;
        return order.ID;
    }
    public Order get(int id)
    {
        int i;
        for ( i = 0; i < arrayOrder.Length; i++)
        {
            if (id== arrayOrder[i].ID)
            {
                return arrayOrder[i];
            }
        }
        throw new Exception("ORDER NOT FOUND!");
    }
    public Order[] get()
    {
        return arrayOrder;
    }
    public void delete(int id)
    {
        if
        Order[] Order = new Order[100];
        int i;
        for (i = 0; i < arrayOrder.Length; i++)
        {
            if (id == arrayOrder[i].ID)
            {
                break;
            }
        }
        for (int j = 0; j < i - 1; j++)
        {
            Order[j] = arrayOrder[j];
        }
        for (int j = i + 1; j < arrayOrder.Length; j++)
        {
            Order[j] = arrayOrder[j];
        }
        arrayOrder = Order;
    }
    public void update(Order newOrder)
    {
        if (search(newOrder))
        {
            for (int i = 0; i < arrayOrder.Length; i++)
            {
                if (newOrder.ID == arrayOrder[i].ID)
                    arrayOrder[i] = newOrder;
            }
        }
    }
    public bool search(Order find)//help function
    {
        int i;
        for (i = 0; i < arrayOrder.Length; i++)
        {
            if (find.ID == arrayOrder[i].ID)
            {
                return true;
            }
        }
        return false;
    }

}
