using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    internal class User
    {
        public string? Email { get; set; }

        public string? Adress { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public bool IsAdmin { get; set; }

        public List<DO.Order?>? MyOrders { get; set; }

        public BO.Cart UserCart { get; set; }


    }
}
