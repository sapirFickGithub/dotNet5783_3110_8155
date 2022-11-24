using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;


namespace BlApi
{
     public interface ICart
    {
      
      public BO.Cart add (Cart cart, int id);
        public BO.Cart updet(Cart cart, int id,int amount);
        public bool approvment(Cart cart);
    }
}
