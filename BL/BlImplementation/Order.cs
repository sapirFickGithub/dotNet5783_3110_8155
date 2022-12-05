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
        int _items = 0;
        double _totalPrice=0;
        private DalApi.IDal Dal = new Dal.DalList();
        public IEnumerable<BO.OrderForList> getListOfOrder()
        {
            IEnumerable<DO.OrderItem> orderItem = Dal.OrderItem.getAll();
            IEnumerable<DO.Order> order = Dal.Order.getAll();
            IEnumerable<BO.OrderForList> orderForList = new List<BO.OrderForList>();
            BO.OrderForList temp;
            foreach (var item in orderItem)
            {
                _items += item.amount;
                _totalPrice += item.Price;
            }
            foreach (var item in order)
            {
                temp = new() { ID = item.ID, CustomerName = item.CustomerName, AmountOfItem = _items, TotalPrice = _totalPrice};
                
                if(Dal.Order.get(item.ID).DateCreateDelivery == DateTime.MinValue)
                {
                    temp.Status = (BO.Enum.OrderStatus.SHIPPED);
                }
                else if (Dal.Order.get(item.ID).DateOfOrder != DateTime.MinValue && Dal.Order.get(item.ID).DateCreateDelivery == DateTime.MinValue)
                {
                    temp.Status = (BO.Enum.OrderStatus.ORDERED);
                }
                else
                {
                    temp.Status = (BO.Enum.OrderStatus.DLIVERY);
                }
                orderForList.Append(temp);
            }
            return orderForList;
        }
        public BO.Order GetOrder(int id)
        {
            double _price = 0,_totalPrice =0;
            if (id > 0)
            {
                DO.OrderItem amount = Dal.OrderItem.get(id);
                DO.Order Dorder = Dal.Order.get(id);
                IEnumerable<DO.OrderItem> orderItem = Dal.OrderItem.getAll();
                BO.Order order = new()
                {
                    ID = id,
                    CustomerName = Dorder.CustomerName,
                    CustomerAddress = Dorder.CustamerAddress,
                    CustomerMail = Dorder.CustomerMail,
                    DateOfOrder = Dorder.DateOfOrder,
                    DateCreateDelivery = Dorder.DateCreateDelivery,
                    DateOfDelivery = Dorder.DateOfDelivery
                };
                if (Dal.Order.get(id).DateCreateDelivery == DateTime.MinValue)
                {
                    order.Status = (BO.Enum.OrderStatus.SHIPPED);
                }
                else if (Dal.Order.get(id).DateOfOrder != DateTime.MinValue && Dal.Order.get(id).DateCreateDelivery == DateTime.MinValue)
                {
                    order.Status = (BO.Enum.OrderStatus.ORDERED);
                }
                else
                {
                    order.Status = (BO.Enum.OrderStatus.DLIVERY);
                }
                foreach (var item in orderItem)
                {
                    if (id == item.NumOfOrder)
                    {
                        _price += item.Price;
                        _totalPrice = Dal.OrderItem.get(id).amount * item.Price;
                        order.Items.Add(new()
                        {
                            ID = item.ID,
                            NameOfProduct = Dal.Product.get(id).Name,
                            amount = Dal.OrderItem.get(id).amount,
                            PriceOfProduct = item.Price,
                            totalPrice = _totalPrice
                        });
                    }
                }
                order.TotalPrice = _price;
                return order;
            }
            else
                throw new notExist();
        }
        public BO.Order updateDliveryOrder(int numOfOrder)
        {
            DO.Order Dorder = Dal.Order.get(numOfOrder);
            if (Dorder.DateOfDelivery == DateTime.MinValue && Dorder.ID>0)
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
            if (Dorder.DateOfDelivery != DateTime.MinValue && Dorder.ID > 0 && Dorder.DateOfOrder == DateTime.MinValue)
            {
                Dorder.DateOfOrder = DateTime.Now;
                BO.Order order = GetOrder(numOfOrder);
                order.DateOfOrder = DateTime.Now;
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
           

            if (numOfOrder > 0)
            {
                BO.OrderTracking orderTracking = new() { ID = numOfOrder };
                orderTracking.Track = new List<Tuple<DateTime, BO.Enum.OrderStatus>>();
                if (numOfOrder > 0)
                {
                    if (Dorder.DateCreateDelivery == DateTime.MinValue)
                    {
                        orderTracking.Status = (BO.Enum.OrderStatus.SHIPPED);
                        Tuple<DateTime, BO.Enum.OrderStatus> a =  new Tuple<DateTime, BO.Enum.OrderStatus>((DateTime)Dorder.DateOfDelivery, orderTracking.Status);
                       


                    }
                    else if (Dal.Order.get(numOfOrder).DateOfOrder!= DateTime.MinValue && Dal.Order.get(numOfOrder).DateCreateDelivery == DateTime.MinValue)
                    {
                        order.Status = (BO.Enum.OrderStatus.ORDERED);
                        Tuple<DateTime, BO.Enum.OrderStatus> a = new Tuple<DateTime, BO.Enum.OrderStatus>((DateTime)Dorder.DateOfOrder, orderTracking.Status);
                    }
                    else
                    {
                        order.Status = (BO.Enum.OrderStatus.DLIVERY);
                    }
                }
            }
        }


    }

}
  
