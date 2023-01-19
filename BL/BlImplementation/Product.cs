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

        public IEnumerable<BO.ProductItem> getListOfProductItem(Func<DO.Product?, bool>? param = null)
        {
            IEnumerable<DO.Product?> products = dal.Product.getAllByParam(param) ?? throw new Exception("NULL");
            var productsItemList = from product in products
                                   select (doToBoProductItem(product));
            return productsItemList;
        }

        public BO.ProductItem doToBoProductItem(DO.Product? product)
        {
            try
            {
                return new BO.ProductItem
                {
                    idOfProduct = (int)(product?.idOfProduct),
                    Name = product?.Name,
                    Price = (double)(product?.Price),
                    InStock = ((int)(product?.InStock) > 0),
                    Amount = 0,
                    ProductCategory = (BO.Enum.Category)product?.ProductCategory
                };
            }
            catch(DO.notExist ex)
            {
                throw new BO.BlMissingEntityException("not exist ",ex);
            }
        }

        public IEnumerable<BO.ProductForList> getListOfProduct(Func<DO.Product?, bool>? param)
        {
            IEnumerable<DO.Product?> products = dal.Product.getAllByParam(param) ?? throw new Exception("NULL");
            var productsList = from product in products
                               select (doToBoProductForList(product));
            return productsList;
        }

        private ProductForList doToBoProductForList(DO.Product? product)
        {
            try
            {
                return new BO.ProductForList
                {
                    idOfProduct = (int)(product?.idOfProduct),
                    Name = product?.Name,
                    Price = (double)(product?.Price),
                    ProductCategory = (BO.Enum.Category)product?.ProductCategory,
                };
            }
            catch (DO.notExist ex)
            {
                throw new BO.BlMissingEntityException("the product for list doesn't exsit", ex);
            }

        }

        public BO.Product GetProduct(int idOfProduct)
        {
            BO.Product product = new BO.Product();
            if (idOfProduct > 999999 || idOfProduct < 100000)
            {
                throw new incorrectData();
            }
            DO.Product Dproduct = dal.Product.getOneByParam(x => idOfProduct == x?.idOfProduct) ?? throw new BO.notExist();

            BO.Product? tempProduct = new()
            {
                idOfProduct = Dproduct.idOfProduct,
                Name = Dproduct.Name,
                Price = Dproduct.Price,
                InStock = Dproduct.InStock,
                ProductCategory = (BO.Enum.Category?)Dproduct.ProductCategory

            };
            return tempProduct;

        }

        public BO.ProductItem GetProductItem(int idOfProduct)
        {
            try
            {
                var item = (DO.Product)(dal.Product.getOneByParam(x => idOfProduct == x?.idOfProduct) ?? throw new BO.notExist()); ;
                BO.ProductItem productItem = new()
                {
                    idOfProduct = item.idOfProduct,
                    Name = item.Name,
                    Price = item.Price,
                    ProductCategory = (BO.Enum.Category)item.ProductCategory,
                    Amount = 0,
                    InStock = (item.InStock > 0)
                };

                return productItem;
            }
            catch (DO.notExist ex)
            {
                throw new BO.BlMissingEntityException("the product doesn't exist ", ex);
            }
        }



        public BO.ProductItem GetDetails(int idOfProduct, BO.Cart cart)
        {
            if ((idOfProduct > 999999 || idOfProduct < 100000))
            {
                throw new BO.incorrectData();
            }
            try
            {
                DO.Product Dproduct = (DO.Product)dal.Product.getOneByParam(x => idOfProduct == x?.idOfProduct);
                BO.Product tempProduct = new()
                {
                    idOfProduct = Dproduct.idOfProduct,
                    Name = Dproduct.Name,
                    Price = Dproduct.Price,
                    InStock = Dproduct.InStock
                };



                var item = cart?.itemList.First(x => x?.idOfProduct == idOfProduct) ?? throw new BO.notExist();

                BO.ProductItem productItem = new()
                {
                    idOfProduct = item.idOfProduct,
                    Name = item.NameOfProduct,
                    Price = item.PriceOfProduct,
                    ProductCategory = (BO.Enum.Category)item.ProductCategory,
                    Amount = item.amount,
                    InStock = (tempProduct.InStock > 0)
                };
                return productItem;
            }
            catch (DO.notExist ex)
            {
                throw new BO.BlMissingEntityException("the product doesn't exist ", ex);
            }

        }
        public int addProduct(string name, BO.Enum.Category productCategory, double price, int inStock)
        {
            if (name == null && price < 0 && inStock <= 0)
            { throw new incorrectData(); }
            DO.Product Dproduct = new DO.Product
            {
                Name = name,
                ProductCategory = (DO.Category)productCategory,
                Price = price,
                InStock = inStock
            };
            return dal.Product.Add(Dproduct);

        }
        public void delete(int idOfProduct)
        {
            if (idOfProduct > 999999 || idOfProduct < 100000)
            {
                throw new BO.incorrectData();
            }
            IEnumerable<DO.OrderItem?> orderItem = dal?.OrderItem.getAllByParam();

            if (orderItem.Any(thisOrderItem => idOfProduct == thisOrderItem?.idProduct))
            {
                throw new existInOrders();
            }
            dal.Product.delete(idOfProduct);
        }
        public void Update(BO.Product product)
        {
            //  try
            {
                if ((product.idOfProduct > 999999 || product.idOfProduct < 100000) && product.Name == null && product.Price < 0 && product.InStock <= 0)
                { throw new incorrectData(); }

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
            //  catch()
        }

        public ProductForList? GetProductForList(int productId)
        {
            return doToBoProductForList(dal.Product.getOneByParam(p => p?.idOfProduct == productId));
        }
    }
}
