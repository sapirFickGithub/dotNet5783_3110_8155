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
        public IEnumerable<OrderItem> getLisr(int id);
        public Order get(int id);
        public Order updatDelivery(int id);
        public Order updatShipping(int id);
        public OrderTracking tracking(int id);
    }
}
