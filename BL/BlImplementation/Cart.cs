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
        private DalApi.IDal Dal = new Dal.DalList();
        public BO.Cart add(BO.Cart cart, int id)
        {
            DO.Product product = Dal.Product.get(id);

            foreach (var item in cart.itemList)
            {
                if (item.IdOfItem == id)
                {
                    if (product.InStock - item.amount >= 0)
                    {
                        item.amount++;
                        item.totalPrice += product.Price;
                        return cart;
                    }
                    else throw new outOfStock();
                }
            }
            if (product.InStock > 0)
            {
                OrderItem newItem = new OrderItem
                {
                    NameOfProduct = product.Name,
                    IdOfItem = product.ID,
                    PriceOfProduct = product.Price,
                    totalPrice = product.Price,
                    amount = 1
                };
                cart.itemList.Add(newItem);
                cart.TotalPrice += product.Price;
                return cart;
            }
            else throw new outOfStock();

        }
        public BO.Cart updet(BO.Cart cart, int id, int amount)
        {
            DO.Product product = Dal.Product.get(id);

            foreach (var item in cart.itemList)
            {
                if (item.IdOfItem == id)
                {
                    if (amount == 0)
                    {
                        BO.OrderItem a = item;
                        cart.itemList.Remove(a);
                        return cart;
                    }
                    if (item.amount < amount)
                        if (product.InStock - item.amount >= 0)
                        {
                            cart.TotalPrice += (amount - item.amount) * product.Price;// so that we dont count the price that was befor.
                            item.amount = amount;
                            item.totalPrice = amount * product.Price;

                            return cart;
                        }
                        else throw new outOfStock();

                }
            }

            return cart;
        }
        public bool approvment(BO.Cart cart)
        {
            //data tast
            if ((!cart.CustomerMail.Contains('@')) || (cart.CustomerName == "")|| (cart.CustomerAddress == ""))
                return false;

            DO.Order newOrder = new DO.Order();

            int id = Dal.Order.Add(newOrder);
            if (id < 0)
                throw new incorrectData();
            newOrder.DateCreateDelivery = DateTime.Now;
            newOrder.DateOfOrder = DateTime.MinValue;
            newOrder.DateOfDelivery = DateTime.MinValue;

            DO.Product product;

            foreach (var item in cart.itemList)
            {
                product = Dal.Product.get(item.ID);
                if (product.InStock-item.amount<0)
                {
                    return false;
                }
                DO.OrderItem newOrderItem = new DO.OrderItem()
                {
                    IdOfItem = item.IdOfItem,
                    NumOfOrder = id,
                    Price = item.PriceOfProduct,
                    amount = item.amount,
                };

            }
            return true;
        }
    }
}