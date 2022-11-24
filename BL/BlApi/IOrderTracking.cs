using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi
{
    public interface IOrderTracking
    {
        public Order updatDelivery(int id);
        public Order updatSupply(int id);
        public OrderTracking tracking(int id);
    }
}
