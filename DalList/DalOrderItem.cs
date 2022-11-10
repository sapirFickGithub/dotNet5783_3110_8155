using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

public class DalOrderItem
{
    static int i = 0;
    private void _add(OrderItem orderItemAdd)
    {
        DataSource.arrayOrderItem[i] = orderItemAdd;
        i++;
    }
}
