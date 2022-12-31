using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi
{
    public interface IOrder
    {
        /// <summary>
        /// giv a list of all the orders.
        /// </summary>
        /// <IEnumerable<BO.OrderForList>></returns>
        public IEnumerable<BO.OrderForList> getListOfOrder();
        /// <summary>
        /// get id of order and give order object with the details of the order.
        /// </summary>
        /// <param name="id"></param>
        /// <Order></returns>
        public Order GetOrder(int numOfOrder);
        /// <summary>
        /// Update the time of dlivery order.
        /// </summary>
        /// <param name="numOfOrder"></param>
        /// <Order></returns>
        public Order updateDliveryOrder(int numOfOrder);
        /// <summary>
        /// Update the time of supply delivery.
        /// </summary>
        /// <param name="numOfOrder"></param>
        /// <Order></returns>
        public BO.Order UpdateSupplyDelivery(int numOfOrder);
        /// <summary>
        /// Gives tracking status of the order.
        /// </summary>
        /// <param name="numOfOrder"></param>
        /// <OrderTracking></returns>
        public BO.OrderTracking orderTracking(int numOfOrder);
        /// <summary>
        /// func to admin only - updete exist order
        /// </summary>
        /// <param name="idOrder"></param>
        /// <param name="idProduct"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool updateAdmin(int idOrder, int idProduct, int amount);
    }
}
