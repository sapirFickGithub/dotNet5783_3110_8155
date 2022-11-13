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
        if (_Config._OrderItemIndex == 200)
        {
            throw new Exception("OVERLOAD! no more space in arrayOrderItem");
        }
        if (search(orderItem))
        {
            throw new Exception("DUPLICATION! this OrderItem already exist");
        }
        orderItem.ID = _Config.get_OrderItemID;
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
        throw new Exception("ORDER-ITEM NOT FOUND!");
    }
    public OrderItem[] get()
    {
        return arrayOrderItem;
    }
    public void delete(int id)
    {
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
    }
    public bool search(OrderItem find)//help function
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

