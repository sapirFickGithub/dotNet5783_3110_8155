using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DalApi
{
    public interface IOrderItem : ICrud<OrderItem>
    {
        public int search(int id);
        public bool search(OrderItem find);
        public void print(int index);
        public int length();
    }
}
