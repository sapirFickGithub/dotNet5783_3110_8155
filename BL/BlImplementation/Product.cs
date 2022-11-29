using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    public class Product : BlApi.IProduct
    {
        public void add(int id, string name, BO.Category productCategory, double price, int inStock)
        {
            throw new NotImplementedException();
        }

        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public BO.Product requstedProduct(int id)
        {
            throw new NotImplementedException();
        }


        public void updat(BO.Product product)
        {
            throw new NotImplementedException();
        }
    }
}
