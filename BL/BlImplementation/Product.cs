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
                BO.ProductForList temp = new() { ID = item.ID, Name = item.Name, Price = item.Price, ProductCategory = (BO.Enum.Category)item.ProductCategory };
                productForList.Add(temp);
            }
            return productForList.AsEnumerable();
        }

        public BO.Product GetProduct(int id)
        {
            BO.Product product = new BO.Product();
            if (id > 0)
            {
                DO.Product Dproduct = Dal.Product.get(id);
                BO.Product temp = new() { ID = Dproduct.ID, Name = Dproduct.Name, Price = Dproduct.Price, InStock = Dproduct.InStock };
                return temp;
            }
            else
                throw new notExist();
        }
        public BO.ProductItem GetDetails(int id, BO.Cart cart)
        {
            if (id > 0)
            {
                DO.Product Dproduct = Dal.Product.get(id);
                BO.Product temp = new() { ID = Dproduct.ID, Name = Dproduct.Name, Price = Dproduct.Price, InStock = Dproduct.InStock };
                foreach (var item in cart.itemList)
                {
                    if (id == item.ID)
                    {
                        BO.ProductItem productItem = new()
                        {
                            ID = item.ID,
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
        public void addProduct(int id, string name, BO.Enum.Category productCategory, double price, int inStock)
        {
            if (id > 0 && name != null && price > 0 && inStock > 0)
            {
                DO.Product Dproduct = new DO.Product
                {
                    ID = id,
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
        public void delete(int id)
        {
            IEnumerable<DO.Order> order = Dal.Order.getAll();
            foreach (var item in order)
            {
                if (id == item.ID)
                {
                    throw new existInOrders();
                }
                if (id < 0)
                {
                    throw new notExist();
                }
            }
            Dal.Product.delete(id);
        }
        public void update(BO.Product product)
        {

            if (product.ID > 0 && product.Name != null && product.Price > 0 && product.InStock > 0)
            {
                DO.Product Dproduct = new DO.Product
                {
                    ID = product.ID,
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
