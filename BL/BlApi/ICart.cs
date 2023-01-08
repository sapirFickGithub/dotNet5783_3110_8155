using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;


namespace BlApi
{
    public interface ICart
    {
        /// <summary>
        /// Adds a product to the shopping cart.
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public BO.Cart add(Cart cart, int id);
        /// <summary>
        /// Update a product in the shopping cart.
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <returns><Cart>
        public BO.Cart updete(Cart cart, int id, int amount);
        /// <summary>
        /// approve the cart and crate a order if all the details are right.
        /// </summary>
        /// <param name="cart"></param>
        /// <returns><bool>
        public int approvment(Cart cart);
    }
}