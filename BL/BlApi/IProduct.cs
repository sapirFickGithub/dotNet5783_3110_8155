using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi
{
    public interface IProduct
    {
        public IEnumerable<ProductForList> getListOfProduct();
        public Product GetProduct(int id);
        public ProductItem GetDetails(int id, BO.Cart cart);
        public void addProduct(int id, string name, Category productCategory ,double price,int inStock);
        public void delete(int id);
        public void update(Product product);
    }
}
