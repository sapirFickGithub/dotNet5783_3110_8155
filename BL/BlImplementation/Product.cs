using BlApi;
using BO;
using DO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlImplementation
{
    internal class Product : BlApi.IProduct
    {
        private DalApi.IDal Dal = new Dal.DalList();
        public IEnumerable<BO.ProductForList> getListOfProduct()
        {
            IEnumerable<DO.Product> products = Dal.Product.getAll();
            List<BO.ProductForList> productForList = new List<BO.ProductForList>();

            foreach (var item in products)
            {
                BO.ProductForList temp = new() { idOfProduct = item.idOfProduct, Name = item.Name, Price = item.Price, ProductCategory = (BO.Enum.Category)item.ProductCategory };
                productForList.Add(temp);
            }
            return productForList.AsEnumerable();
        }

        public BO.Product GetProduct(int idOfProduct)
        {
            BO.Product product = new BO.Product();
            if (idOfProduct > 0)
            {
                DO.Product Dproduct = Dal.Product.get(idOfProduct);
                BO.Product temp = new() { idOfProduct = Dproduct.idOfProduct, Name = Dproduct.Name, Price = Dproduct.Price, InStock = Dproduct.InStock };
                return temp;
            }
            else
                throw new notExist();
        }
        public BO.ProductItem GetDetails(int idOfProduct, BO.Cart cart)
        {
            if (idOfProduct > 0)
            {
                DO.Product Dproduct = Dal.Product.get(idOfProduct);
                BO.Product temp = new() { idOfProduct = Dproduct.idOfProduct, Name = Dproduct.Name, Price = Dproduct.Price, InStock = Dproduct.InStock };
                foreach (var item in cart.itemList)
                {
                    if (idOfProduct == item.IdOfProduct)
                    {
                        BO.ProductItem productItem = new()
                        {
                            idOfProduct = item.IdOfProduct,
                            Name = item.NameOfProduct,
                            Price = item.PriceOfProduct,
                            ProductCategory = (BO.Enum.Category)item.ProductCategory,
                            Amount = item.amount,
                            InStock = temp.InStock
                        };
                        return productItem;
                    }
                }
            }
            throw new notExist();
        }
        public void addProduct(int idOfProduct, string name, BO.Enum.Category productCategory, double price, int inStock)
        {
            if (idOfProduct > 0 && name != null && price > 0 && inStock > 0)
            {
                DO.Product Dproduct = new DO.Product
                {
                    idOfProduct = idOfProduct,
                    Name = name,
                    ProductCategory = (DO.Category)productCategory,
                    Price = price,
                    InStock = inStock
                };
                Dal.Product.Add(Dproduct);
            }
            else
                throw new incorrectData();
        }
        public void delete(int idOfProduct)
        {

            IEnumerable<DO.Order> orders = Dal.Order.getAll();
            foreach (var thisOrder in orders)
            {
                List<DO.OrderItem> orderItem = (List<DO.OrderItem>)Dal.OrderItem.getAllItemOrder(thisOrder.idOfOrder);
                foreach (var thisOrderItem in orderItem)
                    if (idOfProduct == thisOrderItem.idOfItem)
                {
                    throw new existInOrders();
                }
            }

            if (idOfProduct < 0)
                {
                    throw new notExist();
                }
            Dal.Product.delete(idOfProduct);
        }
        public void update(BO.Product product)
        {

            if (product.idOfProduct > 0 && product.Name != null && product.Price > 0 && product.InStock > 0)
            {
                DO.Product Dproduct = new DO.Product
                {
                    idOfProduct = product.idOfProduct,
                    Name = product.Name,
                    ProductCategory = (DO.Category)product.ProductCategory,
                    Price = product.Price,
                    InStock = product.InStock
                };
                Dal.Product.update(Dproduct);
            }
            else
                throw new incorrectData();
        }
    }
}
