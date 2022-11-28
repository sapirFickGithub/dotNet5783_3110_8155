using BO;
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
            double _price = 0;
            int _amount = 0;
            if (id>0)
            {
                
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
                        order.Items.Add(new()
                        {
                            ID = item.ID,
                            NameOfProduct = Dal.Product.get(id),
                        })
                    }
                }
                order.TotalPrice = _price;
                order.Items = _amount;
                





            }
        }

    }
}
