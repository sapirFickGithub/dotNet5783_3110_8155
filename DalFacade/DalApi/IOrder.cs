using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi
{
    public interface IOrder : ICrud<Order>
    {
        /// <summary>
        /// receive an order ID number,
        ///and searches for the order and returns the index in the array of orders.
        /// </summary>
        /// <param name="id"></param>
        /// <int></returns>
        public int search(int idOfOrder);
        /// <summary>
        /// receive an order ID number,
        ///and looks for the order and returns true if the order exists.
        /// </summary>
        /// <param name="find"></param>
        /// <returns></returns>
        public bool search(Order order);
     /// <summary>
     /// Prints the order instead of the index.
     /// </summary>
     /// <param name="index"></param>
        public void print(int index);
      /// <summary>
      /// Returns how many orders there are - the length of the array.
      /// </summary>
      /// <int></returns>
        public int length();
    }
}
