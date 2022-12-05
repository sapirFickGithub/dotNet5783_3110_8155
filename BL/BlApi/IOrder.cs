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
        public IEnumerable<BO.OrderForList> getListOfOrder();
        public Order GetOrder(int id);
        public Order updateDliveryOrder(int numOfOrder);
        public BO.Order UpdateSupplyDelivery(int numOfOrder);
        public BO.OrderTracking orderTracking(int numOfOrder);


    }
}
