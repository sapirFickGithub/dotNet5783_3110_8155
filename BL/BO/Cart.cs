using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BlImplementation;
namespace BO
{
    public class Cart
    {
        public string? CustomerName { get; set; }
        public string? CustomerMail { get; set; }
        public string? CustomerAddress { get; set; }
        public List<OrderItem?>? itemList { get; set; } = new List<OrderItem?>();
        public double TotalPrice { get; set; }



        public override string ToString()
        {
            string print = $@"
Cart:
   Customer name: {CustomerName}.
   Customer Mail- {CustomerMail}.
   Custamer address- {CustomerAddress}.";
            foreach (var item in itemList)
            {
                print = print + "\n" + item.NameOfProduct + "\n" + item.ToString();
            }
            print = print + "\n" + $@"TotalPrice: {TotalPrice}";
            return print;
        }
    }
}
  
            

	
