using BlApi;
using BO;
using DalApi;
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
        public IEnumerable<BO.OrderForList?> GetAllOrderForList()
        {
            IEnumerable<DO.OrderItem?> orderItem = dal.OrderItem?.getAllByParam() ?? throw new BO.notExist();
            var order = dal.Order.getAllByParam() ?? throw new BO.notExist();
            var orderForList = from item in order select (doToBoOrderForList(item, orderItem));
            return orderForList;

        }
        public OrderForList? GetOneOrderForList(int orderId)//get one order in orderForList var.
        {
            IEnumerable<DO.OrderItem?> orderItem = dal.OrderItem?.getAllByParam() ?? throw new BO.notExist();
            return doToBoOrderForList(dal.Order.getOneByParam(p => p?.idOfOrder == orderId), orderItem);
        }

        /// <summary>
        /// conversion DO order to BO order for list
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        private OrderForList doToBoOrderForList(DO.Order? order, IEnumerable<DO.OrderItem?> orderItem)
        {
            var OrderItem = from item in orderItem where (item?.idOfOrder == order?.idOfOrder) select item;
            double _totalPrice = OrderItem.Sum((item => item?.Price ?? 0));

            BO.OrderForList? OrderForList = new()
            {
                idOfOrder = (int)order?.idOfOrder,
                CustomerName = order?.CustomerName,
                AmountOfItem = OrderItem.Count(),
                TotalPrice = _totalPrice
            };
            var orderTemp = dal.Order.getOneByParam(x => order?.idOfOrder == x?.idOfOrder);

            if (orderTemp?.DateOfOrder == null )
            {
                OrderForList.Status = (BO.Enum.OrderStatus.ORDERED);
            }
            
           
            else if (orderTemp?.DateOfOrder != null && orderTemp?.DateOfShipping == null)
            {
                OrderForList.Status = (BO.Enum.OrderStatus.SHIPPED);
            }
            else if (orderTemp?.DateOfDelivery != null)
            {
                OrderForList.Status = (BO.Enum.OrderStatus.DLIVERY);
            }
            return OrderForList;
        }

        public BO.Order GetOrder(int numOfOrder)
        {
            if (numOfOrder > 999999 || numOfOrder < 100000)
                throw new incorrectData();

            DO.Order Dorder = (DO.Order)dal.Order.getOneByParam(x => numOfOrder == x?.idOfOrder);
            IEnumerable<DO.OrderItem?> orderItems = dal.OrderItem.getAllByParam(x => numOfOrder == (int)(x?.ID));/// return null??

            /// the
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
            if (Dorder.DateOfOrder != null && Dorder.DateOfShipping == null)
            {
                order.Status = (BO.Enum.OrderStatus.SHIPPED);
            }
            else if (Dorder.DateOfOrder == null)
            {
                order.Status = (BO.Enum.OrderStatus.ORDERED);
            }
            else
            {
                order.Status = (BO.Enum.OrderStatus.DLIVERY);
            }
            //if (Dorder.DateOfOrder == null)
            //{
            //    order.Status = (BO.Enum.OrderStatus.SHIPPED);
            //}
            //else if (Dorder.DateOfShipping != null && Dorder.DateOfOrder == null)
            //{
            //    order.Status = (BO.Enum.OrderStatus.ORDERED);
            //}
            //else
            //{
            //    order.Status = (BO.Enum.OrderStatus.DLIVERY);
            //}
            
            order.Items = new List<BO.OrderItem>();


            order.Items.AddRange(orderItems.Select((item, index) => new BO.OrderItem
            {
                idOfOrder = (int)item?.ID,
                idOfProduct = (int)item?.idProduct,
                NameOfProduct = dal.Product.getOneByParam(x => (int)item?.idProduct == x?.idOfProduct)?.Name,
                amount = (int)dal.OrderItem.getOneByParam(x => (int)item?.ID == x?.ID)?.amount,
                PriceOfProduct = (double)item?.Price,
                totalPrice = (int)(dal.OrderItem.getOneByParam(x => (int)item?.ID == x?.ID)?.amount) * (double)item?.Price
            }));
            order.TotalPrice = order.Items.Sum(item => item.totalPrice);


            return order;
        }

        public BO.Order? updateDliveryOrder(int numOfOrder)
        {
            if (numOfOrder > 999999 || numOfOrder < 100000)
                throw new incorrectData();

            DO.Order Dorder = dal.Order.getOneByParam(x => numOfOrder == x?.idOfOrder) ?? throw new BO.notExist();
            if (Dorder.DateOfDelivery == null)
                throw new incorrectData();

            Dorder.DateOfDelivery = DateTime.Now;
            BO.Order order = GetOrder(numOfOrder);
            order.DateOfDelivery = DateTime.Now;
            dal.Order.update(Dorder);
            return order;
        }

        public BO.Order? UpdateSupplyDelivery(int numOfOrder)
        {
            DO.Order Dorder = dal.Order.getOneByParam(x => numOfOrder == x?.idOfOrder) ?? throw new BO.notExist();
            if (Dorder.DateOfDelivery != null && (Dorder.idOfOrder <= 999999 && numOfOrder >= 100000) && Dorder.DateOfShipping == null)
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




        public OrderTracking? orderTracking(int orderID)
        {

            DO.Order trackedOrder = dal.Order.getOneByParam(x => orderID == x?.idOfOrder) ?? throw new BO.notExist();


            BO.OrderTracking tracking = new() { idOfOrder = orderID };
            tracking.Track = new() { };

            tracking.Status = (BO.Enum.OrderStatus.ORDERED);
            tracking.Track.Add(new Tuple<DateTime, BO.Enum.OrderStatus>((DateTime)trackedOrder.DateOfOrder, (BO.Enum.OrderStatus)tracking.Status));


            if (trackedOrder.DateOfShipping != null)
            { // if the value of the shiping is define

                tracking.Status = (BO.Enum.OrderStatus.SHIPPED);
                tracking.Track.Add(new Tuple<DateTime, BO.Enum.OrderStatus>((DateTime)trackedOrder.DateOfShipping, (BO.Enum.OrderStatus)tracking.Status));

                if (trackedOrder.DateOfDelivery != null)// and if the value of the deliverying is define
                {
                    tracking.Status = (BO.Enum.OrderStatus.DLIVERY);
                    tracking.Track.Add(new Tuple<DateTime, BO.Enum.OrderStatus>((DateTime)trackedOrder.DateOfDelivery, (BO.Enum.OrderStatus)tracking.Status));
                }
                else
                {
                    tracking.Track.Add(null);
                }
            }
            else
            {
                tracking.Track.Add(null);
                tracking.Track.Add(null);
            }


            return tracking;

        }



        public bool updateAdmin(int idOrder, int idProduct, int amount)
        {

            List<DO.OrderItem> orderItems = (List<DO.OrderItem>)(dal?.OrderItem?.getAllByParam(x => (idOrder == x?.idOfOrder) && (idProduct == x?.idProduct)));

            if (orderItems[0].amount == amount)
                return true;
            var Dproduct = dal.Product.getOneByParam(x => idProduct == x?.idOfProduct) ?? throw new BO.notExist();
            if (Dproduct.InStock - amount < 0)
                return false;

            var orderItem = orderItems[0];
            orderItem.amount = amount;
            dal.OrderItem.update(orderItem);
            return true;
        }



    }

}

