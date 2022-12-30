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
        //orderItem.ID = _Config.get_OrderItemID;למה לא מוסיפים מספר מזהה לפריט מוצר 
        //if (search(orderItem))לא בטוח שצריך מכיוון שמדובר ל מספר רץ
        //{
        //    throw new duplication();
        //}
        listOrderItem.Add(orderItem);
        //listOrderItem[listOrderItem.Count()] = orderItem;
        return orderItem.ID;
    }
    public OrderItem get(int id)
    {
        int i, length = listOrderItem.Count();
        for (i = 0; i < length; i++)
        {
            if (id == listOrderItem[i]?.ID)
            {
                return (OrderItem)listOrderItem[i - 1];
            }
        }
        throw new notFound();
    }
    public IEnumerable<OrderItem?> getAll(Func<OrderItem?, bool>? param)
    {

        if (param == null)//לחזור לראות אם זה תקין
        {
            return listOrderItem;
        }
        var list = from item in listOrderItem where param(item) select item;
        //List<OrderItem?> list = new List<OrderItem?>();
        //foreach (var item in listOrderItem)
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
        listOrderItem.Remove(getByParam(x =>id == x?.ID));
    }

    public void update(OrderItem newOrderItem)
    {
        if (search(newOrderItem))
        {

            for (int i = 0; i < listOrderItem.Count(); i++)
            {
                if (newOrderItem.ID == listOrderItem[i]?.ID)
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
        if (search(find.ID) != -1)
            return true;
        //int i;

        //for (i = 0; i < listOrderItem.Count(); i++)
        //{
        //    if (find.ID == listOrderItem[i]?.ID)
        //    {
                
        //            return true;
                
        //    }
        //}
        return false;
    }
    public int search(int id)//help function for delete, get id and check if the id exist in the product
    {
        int i;
     //   listOrderItem.FindIndex(x =>)
        for (i = 0; i < listOrderItem.Count(); i++)
        {
            if (id == listOrderItem[i]?.ID)
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
    public IEnumerable<OrderItem?> getAllItemOrder(int id)
    {
        List<OrderItem> list = new List<OrderItem>();
        //listOrderItem.orEach(x=> )
        foreach (OrderItem item in listOrderItem)
        {
            if (item.idOfItem == id)
                list.Add(item);
            if (item.amount == 0)
                throw new notFound();
        }
        return (IEnumerable<OrderItem?>)list.AsEnumerable();
    }
    public OrderItem getByParam(Func<OrderItem?, bool>? param)
    {
        foreach (var item in listOrderItem)
        {
            if (param(item))
            {
                OrderItem? orderItem = new OrderItem();
                orderItem = item;
                return (OrderItem)orderItem;
            }
        }
        throw new Exception("NOT EXIST!");

    }
}
