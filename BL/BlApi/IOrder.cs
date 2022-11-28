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
        public IEnumerable<BO.OrderForList> getListOfProduct();
        public Order GetOrder(int id);
    }
}
