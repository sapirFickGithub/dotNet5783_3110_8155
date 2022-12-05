using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi
{    public interface IOrder : ICrud<Order>
    {
        public int search(int id);
        public bool search(Order find);
        public void print(int index);
        public int length();
    }
}
