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
                if (item.ID == id)
                {//item alredy in cart- amount++
                    if (product.InStock - item.amount >= 0)
                    {
                        item.amount++;
                        item.totalPrice += product.Price;
                        return cart;
                    }
                    else throw new outOfStock();
                }
            }
            if (product.InStock > 0)// add the item to cart 
            {
                OrderItem newItem = new OrderItem
                {
                    NameOfProduct = product.Name,
                    ID = product.ID,
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
        public BO.Cart updete(BO.Cart cart, int id, int amount)
        {
            DO.Product product = Dal.Product.get(id);

            foreach (var item in cart.itemList)
            {
                if (item.ID == id)
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
            if (!cart.CustomerMail.Contains('@'))
                throw new incorrectData();//incorect email
            if (cart.CustomerName == "")
                throw new incorrectData();//incorect name
            if (cart.CustomerAddress == "")
                throw new incorrectData();//incorect address

            DO.Order newOrder = new DO.Order();

            int id = Dal.Order.Add(newOrder);
            if (id < 0)
                throw new incorrectData();
            newOrder.DateOfOrder = DateTime.Now;
            newOrder.DateOfShipping = DateTime.MinValue;
            newOrder.DateOfDelivery = DateTime.MinValue;

            foreach (var item in cart.itemList)
            {

                DO.OrderItem newOrderItem = new DO.OrderItem()
                {
                    IdOfItem = item.ID,
                    NumOfOrder = id,
                    Price = item.PriceOfProduct,
                    amount = item.amount,
                };
            }
            return false;
        }
    }
}