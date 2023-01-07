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
        DalApi.IDal? dal = DalApi.Factory.Get();
        public BO.Cart add(BO.Cart cart, int idOfProduct)
        {
            DO.Product product = dal?.Product.getOneByParam(x => idOfProduct == x?.idOfProduct) ?? throw new BO.notExist();

            var item = cart.itemList.FirstOrDefault(i => i.idOfProduct == idOfProduct);
            if (item != null)
            {
                if (product.InStock - item.amount >= 0)
                {
                    item.amount++;
                    item.totalPrice += product.Price;
                    return cart;
                }
                else
                {
                    throw new outOfStock();
                }
            }

            //foreach (var item in cart.itemList)
            //{
            //    if (item.idOfProduct == idOfProduct)
            //    {//item alredy in cart- amount++
            //        if (product.InStock - item.amount >= 0)
            //        {
            //            item.amount++;
            //            item.totalPrice += product.Price;
            //            return cart;
            //        }
            //        else throw new outOfStock();
            //    }
            //}

            if (product.InStock > 0)// add the item to cart 
            {
                BO.OrderItem newItem = new BO.OrderItem
                {
                    NameOfProduct = product.Name,
                    idOfProduct = product.idOfProduct,
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
        public BO.Cart updete(BO.Cart cart, int idOfProduct, int amount)
        {
            DO.Product product = dal.Product.getOneByParam(x => idOfProduct == x?.idOfProduct) ?? throw new BO.notExist();
            var item = cart.itemList.Where(i => i.idOfProduct == idOfProduct).FirstOrDefault();
            if (item == null)
                throw new BO.notExist();
            if (amount == 0)
            {
                cart.itemList.Remove(item);
                return cart;
            }
            if (product.InStock - amount >= 0)
            {
                cart.TotalPrice += (amount - item.amount) * product.Price;
                item.amount = amount;
                item.totalPrice = amount * product.Price;

                return cart;
            }
            else
            {
                throw new outOfStock();
            }
        }
        public bool approvment(BO.Cart cart)
        {
            //data tast
            if ((!(cart.CustomerMail.Contains('@') || cart.CustomerMail == "")) && (cart.CustomerName == "") && (cart.CustomerAddress == ""))
                throw new incorrectData();//incorect address

            DO.Order newOrder = new DO.Order();

            int idOfOrder = dal.Order.Add(newOrder);//return the id of the new order
            if (idOfOrder < 0)
                throw new incorrectData();
            newOrder.DateOfOrder = DateTime.Now;
            newOrder.DateOfShipping = null;
            newOrder.DateOfDelivery = null;

            if (cart.itemList.All(item =>
            {
                var product = dal.Product.getOneByParam(x => item.idOfProduct == x?.idOfProduct) ?? throw new notExist();
                return product.InStock - item.amount >= 0;
            }))
            {
                cart.itemList.ForEach(item =>
                {
                    var product = dal.Product.getOneByParam(x => item.idOfProduct == x?.idOfProduct) ?? throw new notExist();
                    DO.OrderItem newOrderItem = new DO.OrderItem()
                    {
                        idProduct = product.idOfProduct,
                        idOfOrder = item.idOfOrder,
                        Price = item.PriceOfProduct,
                        amount = item.amount,
                    };

                    dal.OrderItem.Add(newOrderItem);
                });

                Console.WriteLine("your order number is : " + idOfOrder);
                return true;
            }
            else
            {
                return false;
            }


            //foreach (var item in cart.itemList)
            //{
            //    //check if the product is in stock
            //    DO.Product product = dal.Product.getOneByParam(x => item.idOfProduct == x?.idOfProduct) ?? throw new notExist();

            //    if (product.InStock - item.amount < 0)
            //        return false;
            //    //if the produt in stock so add to order
            //    DO.OrderItem newOrderItem = new DO.OrderItem()
            //    {
            //        idProduct = product.idOfProduct,
            //        idOfOrder = item.idOfOrder,
            //        Price = item.PriceOfProduct,
            //        amount = item.amount,
            //    };

            //    dal.OrderItem.Add(newOrderItem);
            //}

            ////all the details are true
            //Console.WriteLine("your order number is : " + idOfOrder);
            //return true;
        }
    }
}