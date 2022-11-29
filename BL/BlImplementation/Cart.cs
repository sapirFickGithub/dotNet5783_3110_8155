using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using System.Runtime.CompilerServices;


namespace BlImplementation
{
    internal class Cart : ICart
    {
        private DalApi.IDal dal = new Dal.DalList();
        public BO.Cart add(BO.Cart cart, int id)
        {
            
            BO.Product product = new BO.Product();
            // product = dal.(int id);
           product.


            foreach (var i in cart.Item)
            {
                if (i.IdOfProduct == id)
                {

                    i.amount++;
                    i.totalPrice+=product.Price;
                    cart.TotalPrice+=product.Price;
                    return cart;
                }
            }
            BO.OrderItem newOrderItem = new BO.OrderItem()
            {
                IdOfProduct = id,
                NameOfProduct = product.Name,
                PriceOfProduct = product.Price,
                amount = 1,
                totalPrice = product.Price,
            };
            cart.Item.Add(newOrderItem);
            cart.TotalPrice += product.Price;
            return cart;
        }
        public BO.Cart updetAmount(BO.Cart cart, int id, int amount)
        {
            foreach (var i in cart.Item)
            {
                if (i.ID == id)
                {
                    Product product = requstedProduct(id);
                    if (product.InStock >= amount)
                        i.Amount = amount;
                    else

                }
            }
            throw new notFound();

        }
        public bool approvment(BO.Cart cart)
        {
            return false;
        }
    }
}
