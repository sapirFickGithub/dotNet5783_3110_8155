using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DO;
public class DalOrder
{
    static int i = 0;
    private void _add(Order orderAdd)
    {
        DataSource.arrayOrder[i] = orderAdd;
        i++;
    }
}
