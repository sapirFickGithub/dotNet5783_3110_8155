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
        public Product requstedProduct (int id);
        public void add(int id, string name, Category productCategory ,double price,int inStock);
        public void delete(int id);
        public void updat(Product product);
        public ProductForList allProduct();

    }
}
