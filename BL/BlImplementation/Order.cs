using BO;
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

        DalApi.IDal? dal = DalApi.Factory.Get();
        public IEnumerable<BO.OrderForList> getListOfOrder()
        {
            IEnumerable<DO.OrderItem> orderItem = (IEnumerable<DO.OrderItem>)(dal.OrderItem?.getAllByParam());
            IEnumerable<DO.Order> order = (IEnumerable<DO.Order>)dal.Order.getAllByParam();
            List<BO.OrderForList> orderForList = new List<BO.OrderForList>();
            BO.OrderForList temp;

            foreach (var item in order)
            {
                int _items = 0;
                double _totalPrice = 0;
                foreach (var ite in orderItem)
                {
                    if (ite.idOfOrder == item.idOfOrder)
                        _items += ite.amount;
                    _totalPrice += ite.Price;
                }
                temp = new() { idOfOrder = item.idOfOrder, CustomerName = item.CustomerName, AmountOfItem = _items, TotalPrice = _totalPrice };
                var orderTemp = dal.Order.getOneByParam(x => item.idOfOrder == x?.idOfOrder);

                if (orderTemp.DateOfOrder == null)
                {
                    temp.Status = (BO.Enum.OrderStatus.SHIPPED);
                }
                else if (orderTemp.DateOfShipping != null && orderTemp.DateOfOrder == null)
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
            if (numOfOrder > 999999 || numOfOrder < 100000)
                throw new incorrectData();

            DO.Order Dorder = dal.Order.getByParam(x => numOfOrder == x?.idOfOrder);
                IEnumerable<DO.OrderItem?> orderItems = dal.OrderItem.getAll(x => numOfOrder == (int)(x?.ID));
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
                if (Dorder.DateOfOrder == null)
                {
                    order.Status = (BO.Enum.OrderStatus.SHIPPED);
                }
                else if (Dorder.DateOfShipping != null && Dorder.DateOfOrder == null)
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
                    order.Items.Add(new() { });
                    order.Items[i].idOfOrder = (int)item?.ID;
                    order.Items[i].idOfProduct = (int)item?.idProduct;
                    order.Items[i].NameOfProduct = dal.Product.getOneByParam(x => (int)item?.idProduct == x?.idOfProduct).Name;
                    order.Items[i].amount = dal.OrderItem.getOneByParam(x => (int)item?.ID == x?.ID).amount;
                    order.Items[i].PriceOfProduct = (double)item?.Price;
                    order.Items[i].totalPrice = dal.OrderItem.getOneByParam(x => (int)item?.ID == x?.ID).amount * (double)item?.Price;
                    order.TotalPrice += dal.OrderItem.getOneByParam(x => (int)item?.ID == x?.ID).amount * (double)item?.Price;
                    i++;
                }
                return order;
            }
            else
                throw new notExist();
        }
        public BO.Order updateDliveryOrder(int numOfOrder)
        {
            DO.Order Dorder = dal.Order.getByParam(x => numOfOrder == x?.idOfOrder);
            if (Dorder.DateOfDelivery == null && Dorder.idOfOrder > 0)
            {
                Dorder.DateOfDelivery = DateTime.Now;
                BO.Order order = GetOrder(numOfOrder);
                order.DateOfDelivery = DateTime.Now;
                dal.Order.update(Dorder);
                return order;
            }
            else
            {
                throw new incorrectData();
            }
        }
        public BO.Order UpdateSupplyDelivery(int numOfOrder)
        {
            DO.Order Dorder = dal.Order.getByParam(x => numOfOrder == x?.idOfOrder);
            if (Dorder.DateOfDelivery != null && Dorder.idOfOrder > 0 && Dorder.DateOfShipping == null)
            {
                Dorder.DateOfShipping = DateTime.Now;
                BO.Order order = GetOrder(numOfOrder);
                order.DateOfShipping = DateTime.Now;
                dal.Order.update(Dorder);
                return order;
            }
            else
            {
                throw new incorrectData();
            }
        }
        public BO.OrderTracking orderTracking(int numOfOrder)
        {
            DO.Order Dorder = dal.Order.getByParam(x => numOfOrder == x?.idOfOrder); //אם ההזמנה לא קיימת תיזרק שגיאה
            Tuple<DateTime, BO.Enum.OrderStatus> a;

            if (numOfOrder > 999999|| numOfOrder<100000)
                                throw new incorrectData();

            BO.OrderTracking orderTracking = new() { idOfOrder = numOfOrder };
            orderTracking.Track = new List<Tuple<DateTime, BO.Enum.OrderStatus>>();
            if (numOfOrder > 999999 || numOfOrder < 100000)
            {
                if (Dorder.DateOfShipping == null)
                {
                    orderTracking.Status = (BO.Enum.OrderStatus.ORDERED);
                    a = new Tuple<DateTime, BO.Enum.OrderStatus>((DateTime)Dorder.DateOfOrder, (BO.Enum.OrderStatus)orderTracking.Status);

                }
                else if (Dorder.DateOfOrder != null && Dorder.DateOfDelivery == null)
                {
                    orderTracking.Status = (BO.Enum.OrderStatus.SHIPPED);
                    a = new Tuple<DateTime, BO.Enum.OrderStatus>((DateTime)Dorder.DateOfShipping, (BO.Enum.OrderStatus)orderTracking.Status);
                }
                else
                {
                    orderTracking.Status = (BO.Enum.OrderStatus.DLIVERY);
                    a = new Tuple<DateTime, BO.Enum.OrderStatus>((DateTime)Dorder.DateOfDelivery, (BO.Enum.OrderStatus)orderTracking.Status);
                }


                orderTracking.Track.Add((a));
            }
            return orderTracking;


        }

        public bool updateAdmin(int idOrder, int idProduct, int amount)
        {
            List<DO.OrderItem> orderItems = (List<DO.OrderItem>)(dal?.OrderItem?.getAllByParam(x => (idOrder == x?.idOfOrder) && (idProduct == x?.idProduct)));
            if (orderItems[0].amount == amount)
                return true;
            var Dproduct = dal.Product.getOneByParam(x => idProduct == x?.idOfProduct);
            if (Dproduct.InStock - amount < 0)
                return false;
            var orderItem = orderItems[0];
            orderItem.amount = amount;
            dal.OrderItem.update(orderItem);
            return true;
        }

    }

}

