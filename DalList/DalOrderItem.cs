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
        orderItem.ID = _Config.get_OrderItemID;//למה לא מוסיפים מספר מזהה לפריט מוצר 
        listOrderItem.Add(orderItem);
        return orderItem.ID;
    }
  
    public IEnumerable<OrderItem?> getAllByParam(Func<OrderItem?, bool>? param)
    {

        if (param == null)//לחזור לראות אם זה תקין
        {
            return listOrderItem;
        }
        var list = from item in listOrderItem where param(item) select item;
        
        return list.AsEnumerable();
    }


    public void delete(int id)
    {
        listOrderItem.Remove(getOneByParam(x =>id == x?.ID));
    }

    public void update(OrderItem newOrderItem)
    {
        int index = search(newOrderItem.ID);
        if (index == -1)
        {
            throw new notExist();
        }
        listOrderItem[index] = newOrderItem;
    }
 
    public int search(int id)//help function for delete, get id and check if the id exist in the product
    {
        int counter = 0;
        foreach (var item in listOrderItem)
        {
            if (id == item?.ID)
            {
                return counter;
            }
            counter++;
        }
        return -1;
    }


    public void print()
    {
        foreach (var item in listOrderItem)
        {
            Console.WriteLine(item);
        }
    }


    public int length()
    { return listOrderItem.Count(); }


    public OrderItem? getOneByParam(Func<OrderItem?, bool>? param)
    {
        foreach (var item in listOrderItem)
        {
            if (param(item))
            {
                return item;
            }
        }
        throw new notExist();
    }
}
