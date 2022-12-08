﻿using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    internal class Order : BlApi.IOrder
    {
        int _items = 0;
        double _totalPrice=0;
        private DalApi.IDal Dal = new Dal.DalList();
        public IEnumerable<BO.OrderForList> getListOfOrder()
        {
            IEnumerable<DO.OrderItem> orderItem = Dal.OrderItem.getAll();
            IEnumerable<DO.Order> order = Dal.Order.getAll();
            List<BO.OrderForList> orderForList = new List<BO.OrderForList>();
            BO.OrderForList temp;
            foreach (var item in orderItem)
            {
                _items += item.amount;
                _totalPrice += item.Price;
            }
            foreach (var item in order)
            {
                temp = new() { idOfOrder = item.idOfOrder, CustomerName = item.CustomerName, AmountOfItem = _items, TotalPrice = _totalPrice};
                
                if(Dal.Order.get(item.idOfOrder).DateOfOrder == DateTime.MinValue)
                {
                    temp.Status = (BO.Enum.OrderStatus.SHIPPED);
                }
                else if (Dal.Order.get(item.idOfOrder).DateOfShipping != DateTime.MinValue && Dal.Order.get(item.idOfOrder).DateOfOrder == DateTime.MinValue)
                {
                    temp.Status = (BO.Enum.OrderStatus.ORDERED);
                }
                else
                {
                    temp.Status = (BO.Enum.OrderStatus.DLIVERY);
                }
                orderForList.Add(temp);
            }
            return orderForList.AsEnumerable();
        }
        public BO.Order GetOrder(int numOfOrder)
        {
            if (numOfOrder > 0)
            {
                DO.Order Dorder = Dal.Order.get(numOfOrder);
                IEnumerable<DO.OrderItem> orderItems = Dal.OrderItem.getAll();
                BO.Order? order = new()
                {
                    idOfOrder = numOfOrder,
                    CustomerName = Dorder.CustomerName,
                    CustomerAddress = Dorder.CustamerAddress,
                    CustomerMail = Dorder.CustomerMail,
                    DateOfShipping = Dorder.DateOfShipping,
                    DateOfOrder = Dorder.DateOfOrder,
                    DateOfDelivery = Dorder.DateOfDelivery
                };
                if (Dal.Order.get(numOfOrder).DateOfOrder == DateTime.MinValue)
                {
                    order.Status = (BO.Enum.OrderStatus.SHIPPED);
                }
                else if (Dal.Order.get(numOfOrder).DateOfShipping != DateTime.MinValue && Dal.Order.get(numOfOrder).DateOfOrder == DateTime.MinValue)
                {
                    order.Status = (BO.Enum.OrderStatus.ORDERED);
                }
                else
                {
                    order.Status = (BO.Enum.OrderStatus.DLIVERY);
                }
                int i = 0;
                order.Items = new List<BO.OrderItem>();
                foreach (var item in orderItems)
                {
                    if (numOfOrder == item.ID)
                    {
                        order.Items.Add(new(){ });
                        order.Items[i].idOfOrder = item.ID;
                        order.Items[i].IdOfProduct = item.idOfItem;
                        order.Items[i].NameOfProduct = Dal.Product.get(item.idOfItem).Name;
                        order.Items[i].amount = Dal.OrderItem.get(item.ID).amount;
                        order.Items[i].PriceOfProduct = item.Price;
                        order.Items[i].totalPrice = Dal.OrderItem.get(item.ID).amount * item.Price;
                        order.TotalPrice += Dal.OrderItem.get(item.ID).amount * item.Price;
                        i++;
                    }
                    
                }
                return order;
            }
            else
                throw new notExist();
        }
        public BO.Order updateDliveryOrder(int numOfOrder)
        {
            DO.Order Dorder = Dal.Order.get(numOfOrder);
            if (Dorder.DateOfDelivery == DateTime.MinValue && Dorder.idOfOrder > 0)
            {
                Dorder.DateOfDelivery = DateTime.Now;
                BO.Order order = GetOrder(numOfOrder);
                order.DateOfDelivery = DateTime.Now;
                Dal.Order.update(Dorder);
                return order;
            }
            else
            {
                throw new incorrectData();
            }
        }
        public BO.Order UpdateSupplyDelivery(int numOfOrder)
        {
            DO.Order Dorder = Dal.Order.get(numOfOrder);
            if (Dorder.DateOfDelivery != DateTime.MinValue && Dorder.idOfOrder > 0 && Dorder.DateOfShipping == DateTime.MinValue)
            {
                Dorder.DateOfShipping = DateTime.Now;
                BO.Order order = GetOrder(numOfOrder);
                order.DateOfShipping = DateTime.Now;
                Dal.Order.update(Dorder);
                return order;
            }
            else
            {
                throw new incorrectData();
            }
        }
        public BO.OrderTracking orderTracking(int numOfOrder)
        {
            DO.Order Dorder = Dal.Order.get(numOfOrder); //אם ההזמנה לא קיימת תיזרק שגיאה
            Tuple<DateTime, BO.Enum.OrderStatus> a;

            if (numOfOrder > 0)
            {
                BO.OrderTracking orderTracking = new() { idOfOrder = numOfOrder };
                orderTracking.Track = new List<Tuple<DateTime, BO.Enum.OrderStatus>>();
                if (numOfOrder > 0)
                {
                    if (Dorder.DateOfShipping == DateTime.MinValue)
                    {
                        orderTracking.Status = (BO.Enum.OrderStatus.ORDERED);
                        a = new Tuple<DateTime, BO.Enum.OrderStatus>((DateTime)Dorder.DateOfOrder, orderTracking.Status);

                    }
                    else if (Dal.Order.get(numOfOrder).DateOfOrder!= DateTime.MinValue && Dal.Order.get(numOfOrder).DateOfDelivery == DateTime.MinValue)
                    {
                        orderTracking.Status = (BO.Enum.OrderStatus.SHIPPED);
                        a = new Tuple<DateTime, BO.Enum.OrderStatus>((DateTime)Dorder.DateOfShipping, orderTracking.Status);
                    }
                    else
                    {
                        orderTracking.Status = (BO.Enum.OrderStatus.DLIVERY);
                        a = new Tuple<DateTime, BO.Enum.OrderStatus>((DateTime)Dorder.DateOfDelivery, orderTracking.Status);
                    }
                    orderTracking.Track.Add(a);
                }
                return orderTracking;
            }
            else
                throw new incorrectData();
        }


    }

}
  
