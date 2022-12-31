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
            IEnumerable<DO.Product?> products = dal.Product.getAll(param) ?? throw new Exception("NULL");
            var productsList = from product in products
                               select (new BO.ProductForList
                               {
                                   idOfProduct = (int)(product?.idOfProduct),
                                   Name = product?.Name,
                                   Price = (double)(product?.Price),
                                   ProductCategory = (BO.Enum.Category)product?.ProductCategory

                               });
            return productsList.AsEnumerable();
        }

        public BO.Product GetProduct(int idOfProduct)
        {
            BO.Product product = new BO.Product();
            if (idOfProduct > 999999 || idOfProduct < 100000)
            {
                DO.Product Dproduct = dal.Product.getByParam(x => idOfProduct == x?.idOfProduct);
                BO.Product temp = new() { idOfProduct = Dproduct.idOfProduct, Name = Dproduct.Name, Price = Dproduct.Price, InStock = Dproduct.InStock };
                return temp;
            }
            else
                throw new notExist();
        }
        public BO.ProductItem GetDetails(int idOfProduct, BO.Cart cart)
        {
            if (idOfProduct > 999999 || idOfProduct < 100000)
                throw new incorrectData();
            DO.Product Dproduct = dal.Product.getByParam(x => idOfProduct == x?.idOfProduct);
            BO.Product temp = new()
            {
                idOfProduct = Dproduct.idOfProduct,
                Name = Dproduct.Name,
                Price = Dproduct.Price,
                InStock = Dproduct.InStock
            };
            var productsList = from product in cart.itemList
                               where idOfProduct == product.idOfProduct
                               select (new BO.ProductItem
                               {
                                   idOfProduct = product.idOfProduct,
                                   Name = product.NameOfProduct,
                                   Price = product.PriceOfProduct,
                                   ProductCategory = (BO.Enum.Category)product.ProductCategory,
                                   Amount = product.amount,
                                   InStock = temp.InStock
                               });
            var p = (List<BO.ProductItem>)(productsList);
            return p[0];

        }
        public void addProduct(int idOfProduct, string name, BO.Enum.Category productCategory, double price, int inStock)
        {
            if ((idOfProduct > 999999 || idOfProduct < 100000) && name != null && price > 0 && inStock > 0)
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

            if (idOfProduct > 999999 || idOfProduct < 100000)
                throw new incorrectData();
    
            List<DO.OrderItem> orderItems = (List<DO.OrderItem>)dal.OrderItem.getAll();   
        ///option 1:  --with LINQ  but not so effectiv
        ///    var orderItemTemp =from thisOrderItem in orderItems 
        ///                       where idOfProduct == thisOrderItem.idOfItem  
        ///                      select thisOrderItem;
        ///orderItemTemp?? throw new existInOrders();
            ///option2:
            foreach (var thisOrderItem in orderItems)
                if (idOfProduct == thisOrderItem.idOfItem)
                {
                    throw new existInOrders();
                }
            dal.Product.delete(idOfProduct);
        }
        public void update(BO.Product product)
        {

            if ((product.idOfProduct > 999999 || product.idOfProduct < 100000) && product.Name != null && product.Price > 0 && product.InStock > 0)
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
        //    List<DO.Product?> productList = Dal.Product.getAll(x => category == x?.Category);
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
