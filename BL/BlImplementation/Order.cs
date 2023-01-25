using BlApi;
using BO;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

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
            orderForList = orderForList.GroupBy(x => x.idOfOrder).SelectMany(x => x).ToList();
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
            double _totalPrice = OrderItem.Sum((item => item?.Price*item?.amount ?? 0));
            int sumAmount = OrderItem.Sum(item => item?.amount ?? 0);
            BO.OrderForList? OrderForList = new()
            {
                idOfOrder = (int)order?.idOfOrder,
                CustomerName = order?.CustomerName,
                AmountOfItem = sumAmount,
                TotalPrice = _totalPrice
                
        };
           
            var orderTemp = dal.Order.getOneByParam(x => order?.idOfOrder == x?.idOfOrder);

            if (orderTemp?.DateOfOrder != null&& orderTemp?.DateOfShipping == null)
            {
                OrderForList.Status = (BO.Enum.OrderStatus.ORDERED);
            }
            
           
            else if (orderTemp?.DateOfOrder != null && orderTemp?.DateOfShipping != null && orderTemp?.DateOfDelivery == null)
            {
                OrderForList.Status = (BO.Enum.OrderStatus.SHIPPED);
            }
            else if (orderTemp?.DateOfDelivery != null)
            {
                OrderForList.Status = (BO.Enum.OrderStatus.DELIVERY);
            }
            
            return OrderForList;
        }

        public BO.Order GetOrder(int? numOfOrder)
        {
            if (numOfOrder > 999999 || numOfOrder < 100000)
                throw new incorrectData();

            DO.Order Dorder = (DO.Order)dal.Order.getOneByParam(x => numOfOrder == x?.idOfOrder);
            IEnumerable<DO.OrderItem?> orderItems = dal.OrderItem.getAllByParam(x => numOfOrder == (int)(x?.ID));/// return null??

            BO.Order order = new()
            {
                idOfOrder = (int)numOfOrder,
                CustomerName = Dorder.CustomerName,
                CustomerAddress = Dorder.CustamerAddress,
                CustomerMail = Dorder.CustomerMail,
                DateOfShipping = Dorder.DateOfShipping,
                DateOfOrder = Dorder.DateOfOrder,
                DateOfDelivery = Dorder.DateOfDelivery
            };

            if (Dorder.DateOfOrder != null && Dorder.DateOfShipping == null)
            {
                order.Status = (BO.Enum.OrderStatus.ORDERED);
            }


            else if (Dorder.DateOfOrder != null && Dorder.DateOfShipping != null && Dorder.DateOfDelivery == null)
            {
                order.Status = (BO.Enum.OrderStatus.SHIPPED);
            }
            else if (Dorder.DateOfDelivery != null)
            {
                order.Status = (BO.Enum.OrderStatus.DELIVERY);
            }
            


            order.Items = new List<BO.OrderItem>();

            List<DO.OrderItem?> item = (dal.OrderItem.getAllByParam(x => numOfOrder == x?.idOfOrder).ToList() ?? throw new BO.notExist());
      
            item.ForEach(item =>
                {
                    DO.Product? temp =  dal.Product.getOneByParam(x => (int)item?.idProduct == x?.idOfProduct);
                    BO.OrderItem newOrderItem = new BO.OrderItem()
                    {
                        idOfProduct = (int)item?.idProduct,
                        idOfOrder = (int)item?.idOfOrder,
                        NameOfProduct = temp?.Name,
                        PriceOfProduct = (int)item?.Price,
                        amount = (int)item?.amount,
                        ProductCategory = (BO.Enum.Category?)(temp?.ProductCategory),
                        totalPrice = (double)(temp?.Price * (int)item?.amount)
                    };
                    order.Items.Add(newOrderItem);
                });
           
           
            order.TotalPrice = order.Items.Sum(item => item.totalPrice);


            return order;
        }

        public BO.Order? updateDliveryOrder(int numOfOrder)
        {
            if (numOfOrder > 999999 || numOfOrder < 100000)
                throw new incorrectData();

            DO.Order Dorder = dal.Order.getOneByParam(x => numOfOrder == x?.idOfOrder) ?? throw new BO.notExist();
            if (Dorder.DateOfDelivery != null)
            {
                throw new Exception("the order is already delivered");
            }
            Dorder.DateOfDelivery = DateTime.Now;
            BO.Order order = GetOrder(numOfOrder);
            order.DateOfDelivery = DateTime.Now;
            order.Status = BO.Enum.OrderStatus.DELIVERY;
            dal.Order.update(Dorder);
            return order;
        }

        public BO.Order? UpdateSupplyDelivery(int numOfOrder)
        {
            DO.Order Dorder = dal.Order.getOneByParam(x => numOfOrder == x?.idOfOrder) ?? throw new BO.notExist();
            if (Dorder.DateOfShipping != null)
            {
                throw new Exception("the order is already shipping");

            }
            else if (Dorder.DateOfOrder != null && (Dorder.idOfOrder <= 999999 && numOfOrder >= 100000) && Dorder.DateOfShipping == null)
            {
                Dorder.DateOfShipping = DateTime.Now;
                BO.Order order = GetOrder(numOfOrder);
                order.DateOfShipping = DateTime.Now;
                order.Status = BO.Enum.OrderStatus.SHIPPED;
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
                    tracking.Status = (BO.Enum.OrderStatus.DELIVERY);
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



        public void updateAdmin(int idOrder, int idProduct, int amount)
        {

           DO.OrderItem orderItem = ((DO.OrderItem)dal.OrderItem.getOneByParam(x => (idOrder == x?.idOfOrder) && (idProduct == x?.idProduct)));

            //if (orderItem?.amount == amount)
            //    return true;
            if (amount == 0)
            {
                dal.OrderItem.delete(orderItem.ID);
            }
            var Dproduct = dal.Product.getOneByParam(x => idProduct == x?.idOfProduct) ?? throw new BO.notExist();
            if (amount > orderItem.amount)
            {
                if (Dproduct.InStock - amount < 0)
                    throw new BO.outOfStock(Dproduct.idOfProduct);
                else
                {
                    orderItem.amount = amount;
                    dal.OrderItem.update(orderItem);
                }

            }
            else if(amount!=0)
                {
                orderItem.amount = amount;
                dal.OrderItem.update(orderItem);

            }
        }

        public int? precedenceOrder()
        {
            var SOrdered = (dal?.Order.getAllByParam(x => x?.DateOfShipping == null));
             
             List<DO.Order?> SShipped = dal?.Order.getAllByParam(x => x?.DateOfShipping != null && x?.DateOfDelivery == null).ToList();
            if (SOrdered ==null&& SShipped ==null)
            {
                return null;
            }
            DO.Order? minSO =SOrdered?.MinBy(x => x?.DateOfOrder)??null;
            DO.Order? minSS =SShipped?.MinBy(x => x?.DateOfShipping)??null;
            if (minSO?.DateOfOrder< minSS?.DateOfShipping)
            {
                return minSO?.idOfOrder;

            }
            return minSS?.idOfOrder;

        }

    }

}

