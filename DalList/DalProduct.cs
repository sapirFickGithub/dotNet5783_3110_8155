using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

public class DalProduct
{
    static int i = 0;
    private void _add(Product addProduct)
    {
        DataSource.arrayProduct[i] = addProduct;
        i++;
    }
}
