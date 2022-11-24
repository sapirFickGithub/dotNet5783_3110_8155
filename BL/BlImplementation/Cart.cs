using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace BlImplementation
{
    internal class Cart : ICart
    {
        public Cart add(Cart cart, int id)
        {

            return cart;
        }
        public Cart updet(Cart cart, int id, int amount)
        {

            return cart;
        }
        public bool approvment(Cart cart)
        {
            return false;
        }
    }
}
