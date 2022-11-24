using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

using BO;
using System.Runtime.CompilerServices;


namespace BlImplementation
{
    internal class Cart : ICart
    {
        public BO.Cart add(BO.Cart cart, int id)
        {
            foreach(var OrderItem in Item)
            {

            }
            return cart;
        }
        public BO.Cart updet(BO.Cart cart, int id, int amount)
        {

            return cart;
        }
        public bool approvment(BO.Cart cart)
        {
            return false;
        }
    }
}
