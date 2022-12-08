using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dal.DataSource;
using DalApi;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    public int Add(OrderItem orderItem)
    {
        //orderItem.ID = _Config.get_OrderItemID;
        if (search(orderItem))
        {
            throw new duplication();
        }
        listOrderItem.Add(orderItem);
        //listOrderItem[listOrderItem.Count()] = orderItem;
        return orderItem.ID;
    }
    public OrderItem get(int id)
    {
        int i, length = listOrderItem.Count();
        for (i = 0; i < length; i++)
        {
            if (id == listOrderItem[i].ID)
            {
                return listOrderItem[i - 1];
            }
        }
        throw new notFound();
    }
    public IEnumerable<OrderItem> getAll()
    {
        return listOrderItem;
    }

   
    public void delete(int id)
    {
        listOrderItem.Remove(get(id));
    }

    public void update(OrderItem newOrderItem)
    {
        if (search(newOrderItem))
        {
            for (int i = 0; i < listOrderItem.Count(); i++)
            {
                if (newOrderItem.ID == listOrderItem[i].ID)
                    listOrderItem[i] = newOrderItem;
            }
        }
        else
        {
            throw new notFound();
        }
    }
    public bool search(OrderItem find)//help function
    {
        int count = 0;
        int i;
        for (i = 0; i < listOrderItem.Count(); i++)
        {
            if (find.ID == listOrderItem[i].ID)
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
        for (i = 0; i < listOrderItem.Count(); i++)
        {
            if (id == listOrderItem[i].ID)
            {
                return i;
            }
        }
        return -1;
    }
    public void print(int index)
    {
        Console.WriteLine(listOrderItem[index]);
    }
    public int length()
    { return listOrderItem.Count(); }
    public IEnumerable<OrderItem> getAllItemOrder(int id)
    {
        List<OrderItem> list = new List<OrderItem>();
        foreach (OrderItem item in listOrderItem)
        {
            if (item.idOfItem == id)
                list.Add(item);
            if (item.amount == 0)
                throw new notFound();
        }
        return list;
    }
}
