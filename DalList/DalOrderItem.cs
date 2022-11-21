using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dal.DataSource;

namespace Dal;

public class DalOrderItem
{
    public int Add(OrderItem orderItem)
    {
        orderItem.ID = _Config.get_OrderItemID;
        if (_Config._OrderItemIndex == 200)
        {
            throw new overload();
        }
        if (search(orderItem))
        {
            throw new duplication();
        }

        arrayOrderItem[_Config._OrderItemIndex++] = orderItem;
        return orderItem.ID;
    }
    public OrderItem get(int id)
    {
        int i;
        for (i = 0; i < arrayOrderItem.Length; i++)
        {
            if (id == arrayOrderItem[i].ID)
            {
                return arrayOrderItem[i];
            }
        }
        throw new notFound();
    }
    public OrderItem[] get()
    {
        return arrayOrderItem;
    }
    public void delete(int id)
    {
        if (0 > search(id))
        {
            throw new notFound();
        }
        OrderItem[] OrderItem = new OrderItem[200];
        int i;
        for (i = 0; i < arrayOrderItem.Length; i++)
        {
            if (id == arrayOrderItem[i].ID)
            {
                break;
            }
        }
        for (int j = 0; j < i - 1; j++)
        {
            OrderItem[j] = arrayOrderItem[j];
        }
        for (int j = i + 1; j < arrayOrderItem.Length; j++)
        {
            OrderItem[j] = arrayOrderItem[j];
        }
        arrayOrderItem = OrderItem;
    }
    public void update(OrderItem newOrderItem)
    {
        if (search(newOrderItem))
        {
            for (int i = 0; i < arrayOrderItem.Length; i++)
            {
                if (newOrderItem.ID == arrayOrderItem[i].ID)
                    arrayOrderItem[i] = newOrderItem;
            }
        }
        else
        {
            throw new notFound();
        }
    }
    public bool search(OrderItem find)//help function
    {
        int i;
        for (i = 0; i < arrayOrderItem.Length; i++)
        {
            if (find.ID == arrayOrderItem[i].ID)
            {
                return true;
            }
        }
        return false;
    }
    public int search(int id)//help function for delete, get id and check if the id exist in the product
    {
        int i;
        for (i = 0; i < arrayOrderItem.Length; i++)
        {
            if (id == arrayOrderItem[i].ID)
            {
                return i;
            }
        }
        return -1;
    }


    public void print(int index)
    {
        Console.WriteLine(arrayOrderItem[index]);
    }

    public int length() { return arrayOrderItem.Length; }
}
