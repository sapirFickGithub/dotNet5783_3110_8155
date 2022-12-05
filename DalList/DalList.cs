using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
namespace Dal
{
    sealed public class DalList : IDal
    {
        public IProduct Product => new DalProduct();
        public IOrder Order => new DalOrder();
        public IOrderItem OrderItem => new DalOrderItem();

    }
}

