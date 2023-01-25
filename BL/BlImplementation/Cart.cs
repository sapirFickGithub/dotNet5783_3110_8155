using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using BlApi;
using BO;
using System.Runtime.CompilerServices;


namespace BlImplementation
{
    internal class Cart : ICart
    {
        DalApi.IDal? dal = DalApi.Factory.Get();
        public BO.Cart? add(BO.Cart? cart, int idOfProduct)
        {
            DO.Product product = dal?.Product.getOneByParam(x => idOfProduct == x?.idOfProduct) ?? throw new BO.notExist();

            BO.OrderItem? item = cart?.itemList?.FirstOrDefault(i => i?.idOfProduct == idOfProduct);//item  = null
            if (item != null)
            {
                if (product.InStock - item.amount >= 0)
                {
                    item.amount++;
                    item.totalPrice += product.Price;
                    cart.TotalPrice += product.Price;
                    return cart;
                }
                else
                {
                    throw new outOfStock(product.idOfProduct);
                }
            }

           

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
                cart?.itemList?.Add(newItem);
                cart.TotalPrice += product.Price;
                return cart;
            }
            else throw new outOfStock(product.idOfProduct);


        }
        public BO.Cart? updete(BO.Cart? cart, int idOfProduct, int amount)
        {
            DO.Product product = dal?.Product.getOneByParam(x => idOfProduct == x?.idOfProduct) ?? throw new BO.notExist();
        
            var item = cart?.itemList?.Where(i => i.idOfProduct == idOfProduct).FirstOrDefault();
            if (item == null)
                throw new BO.notExist();
            if (amount == 0)
            {
                cart.TotalPrice -= product.Price;
                cart?.itemList?.Remove(item);
                return cart;
            }
            if (product.InStock - amount >= 0)
            {

                cart.TotalPrice += (amount - item.amount) * product.Price;
                item.amount = amount;
                item.totalPrice += amount * product.Price;

                return cart;
            }
            else
            {
                throw new outOfStock(product.idOfProduct);
            }
        }
        public int approvment(BO.Cart cart)
        {
            //data tast
            if ((!(cart.CustomerMail.Contains('@') || cart.CustomerMail == "")) && (cart.CustomerName == "") && (cart.CustomerAddress == ""))
                throw new incorrectData();//incorect address

            DO.Order newOrder = new DO.Order();

           //return the id of the new order
         
            newOrder.DateOfOrder = DateTime.Now;
            newOrder.DateOfShipping = null;
            newOrder.DateOfDelivery = null;
            newOrder.CustomerName = cart.CustomerName;
            newOrder.CustomerMail = cart.CustomerMail;
            newOrder.CustamerAddress = cart.CustomerAddress;
            int idOfOrder = dal.Order.Add(newOrder);

            if (cart.itemList.All(item =>
            {
                var product = dal.Product.getOneByParam(x => item.idOfProduct == x?.idOfProduct) ?? throw new notExist();
                if (product.InStock - item.amount < 0)
                {
                    throw new outOfStock(item.idOfProduct);

                }
                else return true;
                
            }))
            {
                cart.itemList.ForEach(item =>
                {
                    var product = dal.Product.getOneByParam(x => item.idOfProduct == x?.idOfProduct) ?? throw new notExist();
                    DO.OrderItem newOrderItem = new DO.OrderItem()
                    {
                        idProduct = product.idOfProduct,
                        idOfOrder = idOfOrder,
                        Price = item.PriceOfProduct,
                        amount = item.amount,
                                            
                    };

                    dal.OrderItem.Add(newOrderItem);
                });

                Console.WriteLine("your order number is : " + idOfOrder);
                //dal.Order.update(newOrder);
                return idOfOrder;

            }
            throw new incorrectData();
            


           
        }
    }
}