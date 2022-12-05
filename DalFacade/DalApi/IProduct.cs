using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi
{
    public interface IProduct : ICrud<Product> 
    {
        public int search(int id);
        public bool search(Product find);
        public void print(int index);
        public int length();
    }
}

