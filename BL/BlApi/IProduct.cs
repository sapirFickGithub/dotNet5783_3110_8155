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
        public IEnumerable<BO.ProductForList> getListOfProduct();
        public BO.Product GetProduct(int id);
        public BO.ProductItem GetDetails(int id, BO.Cart cart);
        public void addProduct(int id, string name, BO.Enum.Category productCategory, double price, int inStock);
        public void delete(int id);
        public void update(BO.Product product);
    }
}
