using BlApi;
using BO;
using DO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlImplementation
{
    internal class Product : BlApi.IProduct
    {
        DalApi.IDal? dal = DalApi.Factory.Get();

        public IEnumerable<BO.ProductForList> getListOfProduct(Func<DO.Product?, bool>? param)
        {
            IEnumerable<DO.Product?> products = dal.Product.getAllByParam(param)?? throw new Exception("NULL");
            List<BO.ProductForList?> productForList = new List<BO.ProductForList?>();

            foreach (var item in products)
            {
                BO.ProductForList temp = new() 
                { 
                    idOfProduct = (int)(item?.idOfProduct),
                    Name = item?.Name,
                    Price = (double)(item?.Price),
                    ProductCategory = (BO.Enum.Category)item?.ProductCategory
                                 
                };

                productForList.Add(temp);
            }
            return productForList.AsEnumerable();
        }

        public BO.Product GetProduct(int idOfProduct)
        {
            BO.Product product = new BO.Product();
            if (idOfProduct > 0)
            {
                DO.Product? Dproduct = dal.Product.getOneByParam(x => idOfProduct == x?.idOfProduct);
                BO.Product temp = new() { idOfProduct = Dproduct.idOfProduct, Name = Dproduct.Name, Price = Dproduct.Price, InStock = Dproduct.InStock };
                return temp;
            }
            else
                throw new BO.notExist();
        }
        public BO.ProductItem GetDetails(int idOfProduct, BO.Cart cart)
        {
            if (idOfProduct > 0)
            {
                DO.Product Dproduct = dal.Product.getOneByParam(x => idOfProduct == x?.idOfProduct);
                BO.Product temp = new()
                { idOfProduct = Dproduct.idOfProduct,
                    Name = Dproduct.Name, 
                    Price = Dproduct.Price,
                    InStock = Dproduct.InStock };
                foreach (var item in cart.itemList)
                {
                    if (idOfProduct == item.idOfProduct)
                    {
                        BO.ProductItem productItem = new()
                        {
                            idOfProduct = item.idOfProduct,
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
            throw new BO.notExist();
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
                dal.Product.Add(Dproduct);
            }
            else
                throw new incorrectData();
        }
        public void delete(int idOfProduct)
        {


            List<DO.OrderItem> orderItem = (List<DO.OrderItem>)dal.OrderItem.getAllByParam();
            foreach (var thisOrderItem in orderItem)
                if (idOfProduct == thisOrderItem.idProduct)
                {
                    throw new existInOrders();
                }

            if (idOfProduct < 0)
            {
                throw new BO.notExist();
            }
            dal.Product.delete(idOfProduct);
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
                dal.Product.update(Dproduct);
            }
            else
                throw new incorrectData();
        }
        //public IEnumerable<BO.ProductForList> getByCategory(BO.Enum.Category category)
        //{
        //    List<DO.Product?> productList = Dal.Product.getAllByParam(x => category == x?.Category);
        //    List<BO.ProductForList?> productForList = new List<BO.ProductForList?>();

        //    foreach (var item in productList)
        //    {
        //        BO.ProductForList temp = new()
        //        {
        //            idOfProduct = (int)(item?.idOfProduct),
        //            Name = item?.Name,
        //            Price = (double)(item?.Price),
        //            ProductCategory = (BO.Enum.Category)item?.ProductCategory
        //        };
        //        productForList.Add(temp);
        //    }
        //    return productForList.AsEnumerable();
        //}
    }
}
